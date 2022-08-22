using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.DAL.Entities.Identity;

namespace Talabat.DAL.Identity
{
    public class AppIdentityDbContextSeed
    {
        public static async Task SeedUsersAsync(UserManager<AppUser> userManager)
        {
            if(!userManager.Users.Any())
            {
                var user = new AppUser()
                {
                    DisplayName = "Islam Ibrahim",
                    UserName = "islamIbrahim95",
                    Email = "islam.Ibrahim6464@gmail.com",
                    PhoneNumber = "01119183250",
                    Address = new Address()
                    {
                        FirstName = "islam",
                        LastName = "Ibrahim",
                        Country = "Egypt",
                        City = "Cairo",
                        Street = "Ain shams"
                    }
                    
                };

                await userManager.CreateAsync(user, "P@ss0rd");
            }
            
            
        }

    }
}
