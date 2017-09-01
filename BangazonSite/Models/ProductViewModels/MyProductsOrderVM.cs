using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BangazonSite.Models;
using BangazonSite.Data;
using Microsoft.EntityFrameworkCore;

namespace BangazonSite.Models.ProductViewModels
{
    public class GroupedProducts
    {
        public string Title { get; set; }
        public int Quantity { get; set; }
        public int OrderedCount { get; set; }
    }

    // This class was authored by Jordan Dhaenens
    public class MyProductsOrderVM
    {
        public ApplicationUser User { get; set; }
        public IEnumerable<GroupedProducts> UserProducts { get; set; }
        public IEnumerable<Product> Products { get; set; } 

        public MyProductsOrderVM(ApplicationDbContext ctx, ApplicationUser user)
        {
            User = user;
            Products = ctx.Product.Where(p => p.User == user).Include(t => t.ProductType);
           
            UserProducts = (
                from p in ctx.Product
                join op in ctx.OrderProduct
                on p.ProductId equals op.ProductId
                group new { p, op } by new { p.ProductId, p.Quantity, p.Title } into grouped
                select new GroupedProducts
                {
                    Title = grouped.Key.Title,
                    Quantity = grouped.Key.Quantity,
                    OrderedCount = grouped.Select(x => x.op.ProductId).Count(),
                    
                }).ToList();

            

        }
    }
}
