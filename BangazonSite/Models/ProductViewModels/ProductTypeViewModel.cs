using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BangazonSite.Models.ProductViewModels
{
    public class ProductTypeViewModel
    {
        public IEnumerable<ProductType> ProductTypes { get; set; }

        public int TypeCount { get; set; }

        public IEnumerable<Product> Products { get; set; }

        public ProductTypeViewModel(IEnumerable<ProductType> types, IEnumerable<Product> products)
        {
            ProductTypes = types;
            Products = products;
        }
    }
}
