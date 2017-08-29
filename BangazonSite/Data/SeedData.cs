using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BangazonSite.Models;

namespace BangazonSite.Data
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                // Look for any Users
                if (context.ApplicationUser.Any())
                {
                    //Users table already seeded
                }
                else
                {
                    context.ApplicationUser.AddRange(
                        new ApplicationUser
                        {
                            FirstName: "John",
                            LastName: "Wayne",
                            Address: "somewhere out there",
                            City: "Nashville",
                            State: "TN",
                            Zip: 37202,
                            Phone: 615-555-5050
                        },
                        new ApplicationUser
                        {
                            FirstName: "Jimmy",
                            LastName: "Dean",
                            Address: "somewhere out here",
                            City: "Nashville",
                            State: "TN",
                            Zip: 37212,
                            Phone: 615-555-5050
                        },
                        new ApplicationUser
                        {
                            FirstName: "Jacques",
                            LastName: "Cousteau",
                            Address: "still out there",
                            City: "Nashville",
                            State: "TN",
                            Zip: 37202,
                            Phone: 615-555-5050
                        },
                        new ApplicationUser
                        {
                            FirstName: "Drake",
                            LastName: "Carter",
                            Address: "here",
                            City: "Nashville",
                            State: "TN",
                            Zip: 37202,
                            Phone: 615-555-5050
                        },
                        new ApplicationUser
                        {
                            FirstName: "Dominic",
                            LastName: "Verton",
                            Address: "somewhere out there",
                            City: "Nashville",
                            State: "TN",
                            Zip: 37202,
                            Phone: 615-555-5050
                        },
                        new ApplicationUser
                        {
                            FirstName: "Wakka",
                            LastName: "Flocka",
                            Address: "way out there",
                            City: "Nashville",
                            State: "TN",
                            Zip: 37202,
                            Phone: 615-555-5050
                        },
                        new ApplicationUser
                        {
                            FirstName: "Smith",
                            LastName: "Havier",
                            Address: "somewhere out there",
                            City: "Florence",
                            State: "AL",
                            Zip: 37202,
                            Phone: 615-555-5050
                        }
                    );
                    context.SaveChanges();
                }

                if (context.Product.Any())
                {
                    //Product already seeded
                }
                else
                {
                    context.Product.AddRange(
                        new Product
                        {
                            ProductTypeId = context.ProductType.First(p => p.Type == "Food").ProductTypeId,
                            Title = "CrackerJacks",
                            Description = "Caramel covered popcorn with peanuts. May rot or break your teeth",
                            Price = 1,
                            DateCreated = DateTime.Now,
                            QuantityAvailable = 5,
                            LocalDeliveryCity = "",
                            ImagePath = "",
                            IsActive = true,
                            User = context.ApplicationUser.First(u => u.FirstName == "Smith")
                        },
                        new Product
                        {
                            ProductTypeId = context.ProductType.First(p => p.Type == "Electronics").ProductTypeId,
                            Title = "Cruzer USB drive",
                            Description = "35GB of storage",
                            Price = 56.34,
                            DateCreated = DateTime.Now,
                            QuantityAvailable = 6,
                            LocalDeliveryCity = "",
                            ImagePath = "",
                            IsActive = true,
                            User = context.ApplicationUser.First(u => u.FirstName == "Jacques")
                        },
                        new Product
                        {
                            ProductTypeId = context.ProductType.First(p => p.Type == "Furniture").ProductTypeId,
                            Title = "Tete e Tete Chair",
                            Description = "Wonderful chair that enables conversation bewtween two people without craning your neck",
                            Price = 100,
                            DateCreated = DateTime.Now,
                            QuantityAvailable = 1,
                            LocalDeliveryCity = "Nashville",
                            ImagePath = "",
                            IsActive = true,
                            User = context.ApplicationUser.First(u => u.FirstName == "Jimmy")
                        },
                        new Product
                        {
                            ProductTypeId = context.ProductType.First(p => p.Type == "Automobile").ProductTypeId,
                            Title = "Mazda Miata",
                            Description = "Super cool convertible",
                            Price = 1500,
                            DateCreated = DateTime.Now,
                            QuantityAvailable = 1,
                            LocalDeliveryCity = "San Jose",
                            ImagePath = "",
                            IsActive = true,
                            User = context.ApplicationUser.First(u => u.FirstName == "John")
                        }
                    );
                    context.SaveChanges();
                }


            }
        }
    }
}
