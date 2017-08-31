using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BangazonSite.Models;
using BangazonSite.Data;

namespace BangazonSite.Models.ProductViewModels
{
    public class MyProductsOrderVM
    {
        public ApplicationUser User { get; set; }
        public IEnumerable<OrderProduct> OrderProducts { get; set; }
        public IEnumerable<Product> Products { get; set; }

        public MyProductsOrderVM(ApplicationDbContext ctx, ApplicationUser user)
        {
            User = user;
            Products = ctx.Product.Where(p => p.User == user);
        }
    }
}
