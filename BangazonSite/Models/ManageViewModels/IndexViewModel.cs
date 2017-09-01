using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using BangazonSite.Models;

namespace BangazonSite.Models.ManageViewModels
{
    public class IndexViewModel
    {
        public ApplicationUser User { get; set; }

        public bool HasPassword { get; set; }

        public IList<UserLoginInfo> Logins { get; set; }

        public bool BrowserRemembered { get; set; }
    }
}
