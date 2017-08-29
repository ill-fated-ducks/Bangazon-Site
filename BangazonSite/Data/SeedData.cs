using BangazonSite.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using BangazonSite.Models;
using System.Threading.Tasks;

namespace Bangazon_.Data
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                // Look for any ProductTypess as a check to see if db is already seeded.
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
                    });
                    context.SaveChanges();
                }

                if (context.Product.Any())
                {
                    // Product table has been seeded
                }
                else
                {
                    context.Product.AddRange(
                    new Product
                    {
                        ProductTypeId = context.ProductType.First(s => s.Type == "Food").ProductTypeId,
                        Title = "Milk",
                        Description = "A whitish liquid containing proteins, fats, lactose, and various vitamins and minerals that is produced by the mammary glands of all mature female mammals after they have given birth and serves as nourishment for their young.",
                        Price = 1.99,
                        DateCreated = DateTime.Parse("2017-07-23"),
                        Quantity = 34,
                        LocalDeliveryCity = "Nashville",
                        ImagePath = null,
                        IsActive = true,
                        User = context.ApplicationUser.First(i => i.FirstName == "Joan")
                    },

                    new Product
                    {
                        ProductTypeId = context.ProductType.First(s => s.Type == "Electronics").ProductTypeId,
                        Title = "Transistor",
                        Description = "A transistor is a device that regulates current or voltage flow and acts as a switch or gate for electronic signals. It consist of three layers of a semiconductor material, each capable of carrying a current.",
                        Price = 1.02,
                        DateCreated = DateTime.Parse("2017-05-21"),
                        Quantity = 21,
                        LocalDeliveryCity = "Hendersonville",
                        ImagePath = null,
                        IsActive = true,
                        User = context.ApplicationUser.First(i => i.FirstName == "Jimmy")
                    },

                    new Product
                    {
                        ProductTypeId = context.ProductType.First(s => s.Type == "Furniture").ProductTypeId,
                        Title = "Hammock",
                        Description = "A hammock is a sling made of fabric, rope, or netting, suspended between two points, used for swinging, sleeping, or resting.",
                        Price = 195.18,
                        DateCreated = DateTime.Parse("2017-01-11"),
                        Quantity = 8,
                        LocalDeliveryCity = "Brantwood",
                        ImagePath = null,
                        IsActive = true,
                        User = context.ApplicationUser.First(i => i.FirstName == "Drake")
                    },

                    new Product
                    {
                        ProductTypeId = context.ProductType.First(s => s.Type == "Automobile").ProductTypeId,
                        Title = "Toyota",
                        Description = "Discover all the amazing exterior specs and options on the 2018 Toyota Camry, from headlights to taillights and everything in between.",
                        Price = 10959.99,
                        DateCreated = DateTime.Parse("2017-02-17"),
                        Quantity = 3,
                        LocalDeliveryCity = "Nashville",
                        ImagePath = null,
                        IsActive = true,
                        User = context.ApplicationUser.First(i => i.FirstName == "Dominic")
                    },

                    new Product
                    {
                        ProductTypeId = context.ProductType.First(s => s.Type == "Food").ProductTypeId,
                        Title = "Bread",
                        Description = "Bread is a kind of food made of flour or meal that has been mixed with milk or water, made into a dough or batter, with or without yeast or other leavening agent, and baked.",
                        Price = 0.99,
                        DateCreated = DateTime.Parse("2017-06-21"),
                        Quantity = 29,
                        LocalDeliveryCity = "Hendersonville",
                        ImagePath = null,
                        IsActive = true,
                        User = context.ApplicationUser.First(i => i.FirstName == "Wakka")
                    },

                    new Product
                    {
                        ProductTypeId = context.ProductType.First(s => s.Type == "Electronics").ProductTypeId,
                        Title = "Computer",
                        Description = "17-inch laptop with an anti-glare, backlit display. Add options like an FHD screen with discrete graphics to create a PC that reflects what matters to you.",
                        Price = 699.99,
                        DateCreated = DateTime.Parse("2017-03-08"),
                        Quantity = 12,
                        LocalDeliveryCity = "Nashville",
                        ImagePath = null,
                        IsActive = true,
                        User = context.ApplicationUser.First(i => i.FirstName == "Smith")
                    },

                    new Product
                    {
                        ProductTypeId = context.ProductType.First(s => s.Type == "Furniture").ProductTypeId,
                        Title = "Sofa",
                        Description = "A couch also known as a sofa is a piece of furniture for seating three or more people in the form of a bench, with or without armrests, that is partially or entirely upholstered.",
                        Price = 95.99,
                        DateCreated = DateTime.Parse("2017-02-01"),
                        Quantity = 9,
                        LocalDeliveryCity = "Brantwood",
                        ImagePath = null,
                        IsActive = true,
                        User = context.ApplicationUser.First(i => i.FirstName == "Joan")
                    },

                    new Product
                    {
                        ProductTypeId = context.ProductType.First(s => s.Type == "Automobile").ProductTypeId,
                        Title = "Honda",
                        Description = "The 2017 Civic Sedan features aggressive lines and refined features that make the car stand out from the crowd.",
                        Price = 9959.99,
                        DateCreated = DateTime.Parse("2017-07-17"),
                        Quantity = 5,
                        LocalDeliveryCity = "Nashville",
                        ImagePath = null,
                        IsActive = true,
                        User = context.ApplicationUser.First(i => i.FirstName == "Jimmy")
                    });
                    context.SaveChanges();
                }
            }
        }
    }
}