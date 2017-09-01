using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BangazonSite.Models;
using BangazonSite.Data;

namespace BangazonSite.Models.PaymentTypeViewModels
{
    public class PaymentDeleteVM
    {
        public PaymentType PaymentType { get; set; }
        public Order Order { get; set; }
        public PaymentDeleteVM(ApplicationDbContext ctx, int id)
        {
            this.PaymentType = ctx.PaymentType.SingleOrDefault(m => m.PaymentTypeId == id);
            this.Order = ctx.Order.FirstOrDefault(o => o.PaymentTypeId == id);
        }
    }
}
