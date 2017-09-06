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
using BangazonSite.Models.OrderViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace BangazonSite.Controllers
{
    public class OrdersController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public OrdersController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _context = context;
        }

        // This task retrieves the currently authenticated user
        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
        
        // GET: Orders
        [Authorize]
        public async Task<IActionResult> Index()
        {
            // Create new instance of the view model
            OrderListViewModel model = new OrderListViewModel();

            // Set the properties of the view model
            model.Orders = await _context.Order.Include(o => o.User).Include(o => o.PaymentType).ToListAsync();
            return View(model);
        }



        // GET: Orders/Details/5
        [Authorize]
        public async Task<IActionResult> Details([FromRoute]int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            OrderDetailViewModel model = new OrderDetailViewModel();

            model.Order = await _context.Order
                .Include(o => o.User)
                .Include(o => o.PaymentType)
                .Include(o => o.OrderProduct)
                .ThenInclude(o => o.Product)
                .SingleOrDefaultAsync(o => o.OrderId == id);

            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }

        // GET: Orders/Create
        [Authorize]
        public IActionResult Create()
        {
            ViewData["PaymentTypeId"] = new SelectList(_context.PaymentType, "PaymentTypeId", "AccountNumber");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderId,PaymentTypeId")] Order order)
        {
            if (ModelState.IsValid)
            {
                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["PaymentTypeId"] = new SelectList(_context.PaymentType, "PaymentTypeId", "AccountNumber", order.PaymentTypeId);
            return View(order);
        }
        // This method creates new instance of order
        public async Task<Order> CreateOrder()
        {
            var order = new Order();
            order.User = await GetCurrentUserAsync();
            _context.Add(order);
            await _context.SaveChangesAsync();
            return order;
        }

        // POST: Orders/BuyProduct/5
        [Authorize]
        public async Task<IActionResult> BuyProduct(int qty, int? id)
        {
            var product = await _context.Product.SingleOrDefaultAsync(p => p.ProductId == id);
            if (id == null)
            {
                return NotFound();
            }
            product.Quantity = product.Quantity - qty;
            _context.Update(product);

            //This method is expecting qty and product id
            if (id == null)
            {
                return NotFound();
            }
            //once it gets that info it will try to create order
            var user = await GetCurrentUserAsync();
            var order = await _context.Order.SingleOrDefaultAsync(m => m.User.Id == user.Id && m.PaymentType == null);
            if (order == null)
            {
                order = await CreateOrder();
            }
            //then it will try to add order to the join table in OrderProduct table
            for(var i = 0; i < qty; i++)
            {
                var orderProduct = new OrderProduct();
                orderProduct.Order = order;
                orderProduct.Product = await _context.Product.SingleOrDefaultAsync(p => p.ProductId == id);
                _context.Add(orderProduct);
            }
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Home");
        }
        // GET: Orders/Edit/5
        
        [Authorize]
        public async Task<IActionResult> Complete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order.SingleOrDefaultAsync(m => m.OrderId == id);
            CompleteOrderViewModel model = new CompleteOrderViewModel();
            model.Order = order;
            if (order == null)
            {
                return NotFound();
            }
            ViewData["PaymentTypeId"] = new SelectList(_context.PaymentType, "PaymentTypeId", "AccountNumber", order.PaymentTypeId);
            return View(model);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Complete(int id, CompleteOrderViewModel model)
        {
            if (id != model.Order.OrderId)
            {
                return NotFound();
            }
            ModelState.Remove("Order.User");
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(model.Order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(model.Order.OrderId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("ThankYou");
            }
            ViewData["PaymentTypeId"] = new SelectList(_context.PaymentType, "PaymentTypeId", "AccountNumber", model.Order.PaymentTypeId);
            return View(model);
        }

        public IActionResult ThankYou()
        {
            return View();
        }

        // GET: Orders/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            var order = await _context.Order
                .Include(o => o.PaymentType)
                .Include(o => o.OrderProduct)
                .ThenInclude(o => o.Product)
                .SingleOrDefaultAsync(m => m.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var orderProducts = await _context.OrderProduct.Where(p => p.OrderId == id).ToListAsync();

            foreach (var p in orderProducts)
            {
                _context.OrderProduct.Remove(p);
            }

            var order = await _context.Order.SingleOrDefaultAsync(m => m.OrderId == id);
            _context.Order.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // GET: Orders/DeleteOrderProduct/5
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            
            var orderProduct = await _context.OrderProduct.SingleOrDefaultAsync(p => p.OrderProductId == id);

            _context.OrderProduct.Remove(orderProduct);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", new {id = orderProduct.OrderId});
        }

        private bool OrderExists(int id)
        {
            return _context.Order.Any(e => e.OrderId == id);
        }
    }
}
