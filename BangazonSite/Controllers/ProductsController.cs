using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BangazonSite.Data;
using BangazonSite.Models;
using Microsoft.AspNetCore.Identity;
using Bangazon.Models.ProductViewModels;
using Microsoft.AspNetCore.Authorization;

namespace BangazonSite.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ProductsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        // GET: UserProducts
        [Authorize]
        public async Task<IActionResult> Index()
        {
           
            //once it gets that info it will try to create order
            var user = await GetCurrentUserAsync();
            var userProducts = _context.Product.Where(m => m.User.Email == user.Email);
   
            return View(await userProducts.ToListAsync());
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .Include(p => p.ProductType)
                .SingleOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Search
        public async Task<IActionResult> Search(string searchString)
        {
            var products = from p in _context.Product
                         select p;

            if (!String.IsNullOrEmpty(searchString))
            {
                products = products.Where(g => g.Title.Contains(searchString) || g.Description.Contains(searchString) || g.LocalDeliveryCity.Contains(searchString));
            }

            return View(await products.ToListAsync());
        }

        // GET: Products/Create
        public async Task<IActionResult> Create()
        {
            var user = await GetCurrentUserAsync();
            ViewData["ProductTypeId"] = new SelectList(_context.Set<ProductType>(), "ProductTypeId", "Type");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,Quantity,Description,ImagePath,LocalDeliveryCity,Title,Price,ProductTypeId")] Product product)
        {
            ViewData["ProductTypeId"] = new SelectList(_context.Set<ProductType>(), "ProductTypeId", "Type", product.ProductTypeId);

            ModelState.Remove("User");

            if (ModelState.IsValid)
            {
                // Get current user
                var user = await GetCurrentUserAsync();
                product.User = user;
                product.IsActive = true;
                product.DateCreated = DateTime.Now;

                _context.Add(product);

                await _context.SaveChangesAsync();
                return RedirectToAction("Details", new { @id = product.ProductId });
            }
            return View(product);

        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product.SingleOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["ProductTypeId"] = new SelectList(_context.Set<ProductType>(), "ProductTypeId", "Type", product.ProductTypeId);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,Quantity,isActive,DateCreated,Description,ImagePath,LocalDeliveryCity,Title,Price,ProductTypeId")] Product product)
        {
            if (id != product.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.ProductId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            ViewData["ProductTypeId"] = new SelectList(_context.Set<ProductType>(), "ProductTypeId", "Type", product.ProductTypeId);
            return View(product);
        }
       

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .Include(p => p.ProductType)
                .SingleOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                //This method tries to delete product from the database, if product is added to the order it will throw exception
                var products = await _context.Product.SingleOrDefaultAsync(m => m.ProductId == id);
                _context.Product.Remove(products);

                await _context.SaveChangesAsync();

            }
            //If it throws exception it will catch it and tries to remove that product from other tables
            catch (Exception)
            {
                var ordered = _context.OrderProduct.Where(li => li.ProductId == id);

                // On an open order?
                var productOnOpenOrder = from product in _context.Product
                                         join op in _context.OrderProduct
                                         on product.ProductId equals op.ProductId
                                         join o in _context.Order
                                         on op.OrderId equals o.OrderId
                                         where o.PaymentTypeId == null
                                         select product;
                                         
  
                if (productOnOpenOrder != null)
                {
                    // Get all rows in OrderProduct with this product
                    var orderProduct = _context.OrderProduct.Where(li => li.ProductId == id);

                    // Delete all rows in OrderProduct
                    foreach (OrderProduct i in orderProduct)
                    {
                        _context.OrderProduct.Remove(i);
                    }

                    await _context.SaveChangesAsync();
                }
                else
                {
                    return RedirectToAction("Sold");
                }
            }
            return RedirectToAction("Index");
}

        private bool ProductExists(int id)
        {
            return _context.Product.Any(e => e.ProductId == id);
        }
    }
}
