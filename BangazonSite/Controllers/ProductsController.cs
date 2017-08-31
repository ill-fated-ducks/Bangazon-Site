using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BangazonSite.Data;
using BangazonSite.Models;
using Bangazon.Models.ProductViewModels;

namespace BangazonSite.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductsController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Product.Include(p => p.ProductType);
            return View(await applicationDbContext.ToListAsync());
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
        // GET: ProductTypes/Type/5
        public async Task<IActionResult> Type([FromRoute]int? typeId)
        {

            // If no id was in the route, return 404
            if (typeId == null)
            {
                return NotFound();
            }

            /*
                Create instance of view model
             */
            ProductTypeDetailViewModel model = new ProductTypeDetailViewModel();

            /*
                Write LINQ statement to get requested product type
             */

            var productType = _context.ProductType.Single(t => t.ProductTypeId == typeId);

            // If product not found, return 404
            if (productType == null)
            {
                return NotFound();
            }

            /*
                Add corresponding products to the view model
             */
            model.Products = _context.Product.Where(p => p.ProductTypeId == typeId);

            // Add the product type to the view model
            model.ProductType = productType;

            return View(model);
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
        public IActionResult Create()
        {
            ViewData["ProductTypeId"] = new SelectList(_context.Set<ProductType>(), "ProductTypeId", "Type");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,Quantity,isActive,DateCreated,Description,ImagePath,LocalDeliveryCity,Title,Price,ProductTypeId")] Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["ProductTypeId"] = new SelectList(_context.Set<ProductType>(), "ProductTypeId", "Type", product.ProductTypeId);
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
            var product = await _context.Product.SingleOrDefaultAsync(m => m.ProductId == id);
            _context.Product.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool ProductExists(int id)
        {
            return _context.Product.Any(e => e.ProductId == id);
        }
    }
}
