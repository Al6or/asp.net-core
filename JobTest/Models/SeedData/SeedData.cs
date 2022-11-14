using JobTest.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobTest.Models.SeedData
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new dbOrderContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<dbOrderContext>>()))
            {             
                if (context.Order.Any())
                {
                    return;   
                }

                context.Order.AddRange(
                    new Order
                    {
                        Number = "zak-123",
                        Date = DateTime.Parse("2022-11-14"),
                        ProviderId = 1
                    },

                    new Order
                    {
                        Number = "zak-999",
                        Date = DateTime.Parse("2022-11-15"),
                        ProviderId = 2
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
