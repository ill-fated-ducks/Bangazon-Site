using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BangazonSite.Data;
using BangazonSite.Models;
using BangazonSite.Models.PaymentTypeViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace BangazonSite.Controllers
{
    public class PaymentTypesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public PaymentTypesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // Retrieve currently logged in user
        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);


        // GET: PaymentTypes
        public async Task<IActionResult> Index()
        {
            // Get current user
            var user = await GetCurrentUserAsync();

            IEnumerable<PaymentType> models =  await _context.PaymentType.Where(p => p.User.Id == user.Id && p.IsActive != false).ToListAsync();
            return View(models);
        }

        // GET: PaymentTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paymentType = await _context.PaymentType
                .SingleOrDefaultAsync(m => m.PaymentTypeId == id);
            if (paymentType == null)
            {
                return NotFound();
            }

            return View(paymentType);
        }

        // GET: PaymentTypes/Create
        // This method was authored by Jordan Dhaenens
        public async Task<IActionResult> Create()
        {
            PaymentType model = new PaymentType();
           
            // Get current user
            var user = await GetCurrentUserAsync();
            model.User = user;

            return View(model);
        }

        // POST: PaymentTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        // This method was authored by Jordan Dhaenens
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PaymentTypeId,Type,AccountNumber,IsActive")] PaymentType paymentType)
        {
            ModelState.Remove("User");

            if (ModelState.IsValid)
            {
                // Get current user
                var user = await GetCurrentUserAsync();
                paymentType.User = user;
                paymentType.IsActive = true;

                _context.Add(paymentType);

                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(paymentType);
        }

        // GET: PaymentTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paymentType = await _context.PaymentType.SingleOrDefaultAsync(m => m.PaymentTypeId == id);
            if (paymentType == null)
            {
                return NotFound();
            }
            return View(paymentType);
        }

        // POST: PaymentTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PaymentTypeId,Type,AccountNumber,IsActive")] PaymentType paymentType)
        {
            if (id != paymentType.PaymentTypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(paymentType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PaymentTypeExists(paymentType.PaymentTypeId))
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
            return View(paymentType);
        }

        // GET: PaymentTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paymentType = await _context.PaymentType
                .SingleOrDefaultAsync(m => m.PaymentTypeId == id);
            if (paymentType == null)
            {
                return NotFound();
            }

            return View(paymentType);
        }

        // POST: PaymentTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            PaymentDeleteVM modelVM = new PaymentDeleteVM(_context, id);
            
            if (modelVM.Order == null)
            {
                // No instance of this PaymentTypeId exists in Order table. OK to erase
                _context.PaymentType.Remove(modelVM.PaymentType);
            }
            else if (ModelState.IsValid)
            {
                try
                {
                    modelVM.PaymentType.IsActive = false;
                    _context.Update(modelVM.PaymentType);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PaymentTypeExists(modelVM.PaymentType.PaymentTypeId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool PaymentTypeExists(int id)
        {
            return _context.PaymentType.Any(e => e.PaymentTypeId == id);
        }
    }
}
