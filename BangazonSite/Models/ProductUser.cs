using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BangazonSite.Models
{
    public class ProductUser
    {
        [Key]
        public int ProductUserId { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        public ApplicationUser User { get; set; }

        [Required]
        public Product Product { get; set; }
    }
}
