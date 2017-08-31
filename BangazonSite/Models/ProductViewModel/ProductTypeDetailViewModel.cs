using System.Collections.Generic;
using BangazonSite.Models;

namespace Bangazon.Models.ProductViewModels
{
    public class ProductTypeDetailViewModel
    {
        public ProductType ProductType { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }
}