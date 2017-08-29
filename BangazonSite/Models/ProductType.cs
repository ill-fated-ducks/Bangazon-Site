using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BangazonSite.Models
{
    public class ProductType
    {
        [Key]
        public int ProductTypeId { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Category")]
        public string Type { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}