using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BangazonSite.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        [Required]
        public ApplicationUser User { get; set; }

        // paymentype id is a nullable field 
        [Display(Name = "Payment Type")]
        public int ? PaymentTypeId { get; set; }

        //instanse of payment type 
        public PaymentType PaymentType { get; set; }

        //collection of orders in joint table 
        public ICollection<OrderProduct> OrderProduct { get; set; }
    }
}
