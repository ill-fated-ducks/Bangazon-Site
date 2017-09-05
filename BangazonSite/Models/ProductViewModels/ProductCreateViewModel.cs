using BangazonSite.Data;
using BangazonSite.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BangazonSite.Models.ProductViewModels
{
    public class ProductCreateViewModel
    {
        public List<SelectListItem> ProductTypeId { get; set; }
        public Product Product { get; set; }
        public ProductCreateViewModel(ApplicationDbContext ctx)
        {
            // Creating SelectListItems will be used in a @Html.DropDownList
            // control in a Razor template. See Views/Products/Create.cshtml
            // for an example.
            this.ProductTypeId = ctx.ProductType
                                    .OrderBy(l => l.Type)
                                    .AsEnumerable()
                                    .Select(li => new SelectListItem
                                    {
                                        Text = li.Type,
                                        Value = li.ProductTypeId.ToString()
                                    }).ToList();

            this.ProductTypeId.Insert(0, new SelectListItem
            {
                Text = "Choose category...",
                Value = "0"
            });
        }
        public ProductCreateViewModel()
        {

        }
        public List<IFormFile> ImagePath { get; set; } = new List<IFormFile>();
    }
}