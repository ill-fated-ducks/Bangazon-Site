using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace BangazonSite.Models
{
    public class PaymentType
    {
        [Key]
        public int PaymentTypeId { get; set; }

        [Required]
        [StringLength(12)]
        public string Type { get; set; }

        [Required]
        [StringLength(20)]
        public string AccountNumber { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [Required]
        public ApplicationUser User { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}