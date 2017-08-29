using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BangazonSite.Models
{
    public class Order
    {
        [Required]
        public ApplicationUser User { get; set; }

        [Display(Name = "Payment Type")]
        public int ? PaymentTypeId { get; set; }

        public PaymentType PaymentType { get; set; }

        public ICollection<OrderProduct> OrderProduct { get; set; }
    }
}
