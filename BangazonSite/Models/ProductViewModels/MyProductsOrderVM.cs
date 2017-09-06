using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BangazonSite.Models;
using BangazonSite.Data;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BangazonSite.Models.ProductViewModels
{
    public class GroupedProducts
    {
        public string Title { get; set; }
        public int Quantity { get; set; }
        public int OrderedCount { get; set; }
        public int ProductId { get; set; }
        [DisplayFormat(DataFormatString = "{0:C}")]
        public double Price { get; set; }
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
                where p.User == user
                join op in ctx.OrderProduct
                on p.ProductId equals op.ProductId
                group new { p, op } by new { p.ProductId, p.Quantity, p.Title, p.Price } into grouped
                select new GroupedProducts
                {
                    Title = grouped.Key.Title,
                    OrderedCount = grouped.Select(x => x.op.ProductId).Count(),
                    Quantity = (grouped.Key.Quantity - grouped.Select(x => x.op.ProductId).Count()),
                    ProductId = grouped.Key.ProductId,
                    Price = grouped.Key.Price
                }).ToList();

            

        }
    }
}
