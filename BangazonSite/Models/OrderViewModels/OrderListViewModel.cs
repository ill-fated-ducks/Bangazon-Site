using System;
using System.Collections.Generic;
using BangazonSite.Models;
using BangazonSite.Data;

namespace BangazonSite.Models.OrderViewModels
{
    public class OrderListViewModel
    {
        public IEnumerable<Order> Orders { get; set; }
        public Order Order { get; set; }
    }
}
