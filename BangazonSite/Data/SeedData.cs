using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using BangazonSite.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using BangazonSite.Data;
using System.ComponentModel.DataAnnotations;

namespace BangazonSite.Data
{
    public class SeedData
    {

        public async static void Initialize(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetService<ApplicationDbContext>();

            if(context.Users.Any())
            {
                //user table seeded
            }
            else
            {
                List<ApplicationUser> userList = new List<ApplicationUser>();
            
                userList.Add( new ApplicationUser
                {
                    UserName = "john@bangazon.com",
                    NormalizedUserName = "JOHN@BANGAZON.COM",
                    Email = "john@bangazon.com",                
                    NormalizedEmail = "JOHN@BANGAZON.COM",
                    FirstName = "John",
                    LastName = "Wayne",
                    Address = "somewhere out there",
                    City = "Nashville",
                    State = "TN",
                    Zip = "37202",
                    Phone = 1234567890,
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    SecurityStamp = Guid.NewGuid().ToString("D")
                });

                userList.Add(new ApplicationUser
                {
                    UserName = "jacques@bangazon.com",
                    NormalizedUserName = "JACQUES@BANGAZON.COM",
                    Email = "jacques@bangazon.com",
                    NormalizedEmail = "JACQUES@BANGAZON.COM",
                    Phone = 1234567890,
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    SecurityStamp = Guid.NewGuid().ToString("D"),
                    FirstName = "Jacques",
                    LastName = "Cousteau",
                    Address = "still out there",
                    City = "Nashville",
                    State = "TN",
                    Zip = "37202"
                });

                userList.Add(new ApplicationUser
                {
                    UserName = "drake@bangazon.com",
                    NormalizedUserName = "DRAKE@BANGAZON.COM",
                    Email = "drake@bangazon.com",
                    NormalizedEmail = "DRAKE@BANGAZON.COM",
                    Phone = 1234567890,
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    SecurityStamp = Guid.NewGuid().ToString("D"),
                    City = "Nashville",
                    State = "TN",
                    Zip = "37202",
                    FirstName = "Drake",
                    LastName = "Carter",
                    Address = "here"
                });

                userList.Add(new ApplicationUser
                {
                    UserName = "dominic@bangazon.com",
                    NormalizedUserName = "DOMINIC@BANGAZON.COM",
                    Email = "dominic@bangazon.com",
                    NormalizedEmail = "DOMINIC@BANGAZON.COM",
                    Phone = 1234567890,
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    SecurityStamp = Guid.NewGuid().ToString("D"),
                    City = "Nashville",
                    State = "TN",
                    Zip = "37202",
                    FirstName = "Dominic",
                    LastName = "Verton",
                    Address = "somewhere out there",
                });

                userList.Add(new ApplicationUser
                {
                    UserName = "wakka@bangazon.com",
                    NormalizedUserName = "WAKKA@BANGAZON.COM",
                    Email = "wakka@bangazon.com",
                    NormalizedEmail = "WAKKA@BANGAZON.COM",
                    Phone = 1234567890,
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    SecurityStamp = Guid.NewGuid().ToString("D"),
                    City = "Nashville",
                    State = "TN",
                    Zip = "37202",
                    FirstName = "Wakka",
                    LastName = "Flocka",
                    Address = "way out there"
                });

                userList.Add(new ApplicationUser
                {
                    UserName = "smith@bangazon.com",
                    NormalizedUserName = "SMITH@BANGAZON.COM",
                    Email = "smith@bangazon.com",
                    NormalizedEmail = "SMITH@BANGAZON.COM",
                    Phone = 1234567890,
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    SecurityStamp = Guid.NewGuid().ToString("D"),
                    Zip = "37202",
                    FirstName = "Smith",
                    LastName = "Havier",
                    Address = "somewhere out there",
                    City = "Florence",
                    State = "AL"
                });

                foreach(ApplicationUser user in userList)
                {
                    var password = new PasswordHasher<ApplicationUser>();
                    var hashed = password.HashPassword(user, "secret");
                    user.PasswordHash = hashed;

                    var userStore = new UserStore<ApplicationUser>(context);
                    await userStore.CreateAsync(user);
                }

                context.SaveChanges();
            }



            if (context.ProductType.Any())
            {
               // ProductType Table has been seeded
            }
            else
            {
                context.ProductType.AddRange(
                    new ProductType
                    {
                        Type = "Food"
                    },
                    new ProductType
                    {
                        Type = "Electronics"
                    },
                    new ProductType
                    {
                        Type = "Furniture"
                    },
                    new ProductType
                    {
                        Type = "Automobile"
                    }
                );
                context.SaveChanges();
            }


            if (context.Product.Any())
            {
                // Product table has been seeded
            }
            else
            {
                var userStore = new UserStore<ApplicationUser>(context);
                context.Product.AddRange(
                    new Product
                    {
                        ProductTypeId = context.ProductType.First(p => p.Type == "Food").ProductTypeId,
                        Title = "CrackerJacks",
                        Description = "Caramel covered popcorn with peanuts. May rot or break your teeth",
                        Price = 1,
                        Quantity = 5,
                        DateCreated = DateTime.Today,
                        LocalDeliveryCity = "",
                        ImagePath = null,
                        IsActive = true,
                        User = userStore.Users.First<ApplicationUser>(u => u.UserName == "smith@bangazon.com")
                        

                    },
                    new Product
                    {
                        ProductTypeId = context.ProductType.First(p => p.Type == "Electronics").ProductTypeId,
                        Title = "Cruzer USB drive",
                        Description = "35GB of storage",
                        Price = 56.34,
                        Quantity = 6,
                        DateCreated = DateTime.Today,
                        LocalDeliveryCity = "",
                        ImagePath = null,
                        IsActive = true,
                        User = userStore.Users.First<ApplicationUser>(u => u.UserName == "jacques@bangazon.com")
                    },
                    new Product
                    {
                        ProductTypeId = context.ProductType.First(p => p.Type == "Furniture").ProductTypeId,
                        Title = "Tete e Tete Chair",
                        Description = "Wonderful chair that enables conversation bewtween two people without craning your neck",
                        Price = 100,
                        Quantity = 1,
                        DateCreated = DateTime.Today,
                        LocalDeliveryCity = "Nashville",
                        ImagePath = null,
                        IsActive = true,
                        User = userStore.Users.First<ApplicationUser>(u => u.UserName == "john@bangazon.com")
                    },
                    new Product
                    {
                        ProductTypeId = context.ProductType.First(p => p.Type == "Automobile").ProductTypeId,
                        Title = "Mazda Miata",
                        Description = "Super cool convertible",
                        Price = 1500,
                        Quantity = 1,
                        DateCreated = DateTime.Today,
                        LocalDeliveryCity = "San Jose",
                        ImagePath = null,
                        IsActive = true,
                        User = userStore.Users.First<ApplicationUser>(u => u.UserName == "john@bangazon.com")
                    },
                    new Product
                    {
                        ProductTypeId = context.ProductType.First(s => s.Type == "Food").ProductTypeId,
                        Title = "Milk",
                        Description = "A whitish liquid containing proteins, fats, lactose, and various vitamins and minerals that is produced by the mammary glands of all mature female mammals after they have given birth and serves as nourishment for their young.",
                        Price = 1.99,
                        Quantity = 34,
                        DateCreated = DateTime.Today,
                        LocalDeliveryCity = "Nashville",
                        ImagePath = null,
                        IsActive = true,
                        User = userStore.Users.First<ApplicationUser>(u => u.UserName == "john@bangazon.com")
                    },

                    new Product
                    {
                        ProductTypeId = context.ProductType.First(s => s.Type == "Electronics").ProductTypeId,
                        Title = "Transistor",
                        Description = "A transistor is a device that regulates current or voltage flow and acts as a switch or gate for electronic signals. It consist of three layers of a semiconductor material, each capable of carrying a current.",
                        Price = 1.02,
                        Quantity = 21,
                        DateCreated = DateTime.Today,
                        LocalDeliveryCity = "Hendersonville",
                        ImagePath = null,
                        IsActive = true,
                        User = userStore.Users.First<ApplicationUser>(u => u.UserName == "jacques@bangazon.com")
                    },

                    new Product
                    {
                        ProductTypeId = context.ProductType.First(s => s.Type == "Furniture").ProductTypeId,
                        Title = "Hammock",
                        Description = "A hammock is a sling made of fabric, rope, or netting, suspended between two points, used for swinging, sleeping, or resting.",
                        Price = 195.18,
                        Quantity = 8,
                        DateCreated = DateTime.Today,
                        LocalDeliveryCity = "Brantwood",
                        ImagePath = null,
                        IsActive = true,
                        User = userStore.Users.First<ApplicationUser>(u => u.UserName == "drake@bangazon.com")
                    },

                    new Product
                    {
                        ProductTypeId = context.ProductType.First(s => s.Type == "Automobile").ProductTypeId,
                        Title = "Toyota",
                        Description = "Discover all the amazing exterior specs and options on the 2018 Toyota Camry, from headlights to taillights and everything in between.",
                        Price = 10959.99,
                        Quantity = 3,
                        DateCreated = DateTime.Today,
                        LocalDeliveryCity = "Nashville",
                        ImagePath = null,
                        IsActive = true,
                        User = userStore.Users.First<ApplicationUser>(u => u.UserName == "dominic@bangazon.com")
                    },

                    new Product
                    {
                        ProductTypeId = context.ProductType.First(s => s.Type == "Food").ProductTypeId,
                        Title = "Bread",
                        Description = "Bread is a kind of food made of flour or meal that has been mixed with milk or water, made into a dough or batter, with or without yeast or other leavening agent, and baked.",
                        Price = 0.99,
                        Quantity = 29,
                        DateCreated = DateTime.Today,
                        LocalDeliveryCity = "Hendersonville",
                        ImagePath = null,
                        IsActive = true,
                        User = userStore.Users.First<ApplicationUser>(u => u.UserName == "wakka@bangazon.com")
                    },

                    new Product
                    {
                        ProductTypeId = context.ProductType.First(s => s.Type == "Electronics").ProductTypeId,
                        Title = "Computer",
                        Description = "17-inch laptop with an anti-glare, backlit display. Add options like an FHD screen with discrete graphics to create a PC that reflects what matters to you.",
                        Price = 699.99,
                        Quantity = 12,
                        DateCreated = DateTime.Today,
                        LocalDeliveryCity = "Nashville",
                        ImagePath = null,
                        IsActive = true,
                        User = userStore.Users.First<ApplicationUser>(u => u.UserName == "smith@bangazon.com")
                    },

                    new Product
                    {
                        ProductTypeId = context.ProductType.First(s => s.Type == "Furniture").ProductTypeId,
                        Title = "Sofa",
                        Description = "A couch also known as a sofa is a piece of furniture for seating three or more people in the form of a bench, with or without armrests, that is partially or entirely upholstered.",
                        Price = 95.99,
                        Quantity = 9,
                        DateCreated = DateTime.Today,
                        LocalDeliveryCity = "Brantwood",
                        ImagePath = null,
                        IsActive = true,
                        User = userStore.Users.First<ApplicationUser>(u => u.UserName == "john@bangazon.com")
                    },

                    new Product
                    {
                        ProductTypeId = context.ProductType.First(s => s.Type == "Automobile").ProductTypeId,
                        Title = "Honda",
                        Description = "The 2017 Civic Sedan features aggressive lines and refined features that make the car stand out from the crowd.",
                        Price = 9959.99,
                        Quantity = 5,
                        DateCreated = DateTime.Today,
                        LocalDeliveryCity = "Nashville",
                        ImagePath = null,
                        IsActive = true,
                        User = userStore.Users.First<ApplicationUser>(u => u.UserName == "smith@bangazon.com")
                        
                    }
                );
                context.SaveChanges();
            }

            if (context.Order.Any())
            {
                // Already seeded
            }
            else
            {
                var userStore = new UserStore<ApplicationUser>(context);
                context.Order.AddRange(
                    new Order
                    {
                        User = userStore.Users.First<ApplicationUser>(u => u.UserName == "wakka@bangazon.com"),
                        PaymentTypeId = 1
                    },

                    new Order
                    {
                        User = userStore.Users.First<ApplicationUser>(u => u.UserName == "wakka@bangazon.com"),
                        PaymentTypeId = 1
                    }
                );
                context.SaveChanges();
            }

            if (context.PaymentType.Any())
            {
                // Already seeded
            }
            else
            {
                var userStore = new UserStore<ApplicationUser>(context);
                context.PaymentType.AddRange(
                    new PaymentType
                    {
                        User = userStore.Users.First<ApplicationUser>(u => u.UserName == "wakka@bangazon.com"),
                        IsActive = true,
                        AccountNumber = "1234123412341234",
                        Type = "Outdoors"
                    },

                    new PaymentType
                    {
                        User = userStore.Users.First<ApplicationUser>(u => u.UserName == "wakka@bangazon.com"),
                        IsActive = true,
                        AccountNumber = "1234123412341234",
                        Type = "Vida"
                    }
                );
                context.SaveChanges();
            }
        }

    }

}
                
