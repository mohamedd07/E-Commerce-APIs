using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Identity;

namespace Talabat.Repository.Identity.Dataseed
{
    public  static class AppIdentityDbContextSeed
    {
        public static async Task SeedUserAsync (UserManager<AppUser> userManager)
        {
            if(userManager.Users.Count() == 0)
            {
                var user = new AppUser()
                {
                    displayName = "Ahmed Nasr",
                    UserName = "ahmedNasr",
                    Email = "ahmed.nasr@linkdev.com",
                    PhoneNumber = "1234567890",


                };
                await userManager.CreateAsync(user , "Pa$$w0rd");
            }
        }
    }
}
