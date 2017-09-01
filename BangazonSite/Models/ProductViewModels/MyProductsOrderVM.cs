using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BangazonSite.Models;
using BangazonSite.Data;
using Microsoft.EntityFrameworkCore;

namespace BangazonSite.Models.ProductViewModels
{
    // This class was authored by Jordan Dhaenens
    public class MyProductsOrderVM
    {
        public ApplicationUser User { get; set; }
        public IEnumerable<OrderProduct> OrderProducts { get; set; }
        public IEnumerable<Product> Products { get; set; }

        public MyProductsOrderVM(ApplicationDbContext ctx, ApplicationUser user)
        {
            User = user;
            Products = ctx.Product.Where(p => p.User == user).Include(t => t.ProductType);

            // get all orderProducts where Product belongs to user and orderId on order table has a paymentType !null
            OrderProducts = from op in ctx.OrderProduct
                            from p in ctx.Product
                            from o in ctx.Order
                            where (p => p.User == user)
                            where (o => o.PaymentType != null)
                            where op.ProductId == p.ProductId
                            && op.OrderId == o.OrderId
                            group op by op.ProductId into grouped
                            select new {  };

                             
        }
    }
}
