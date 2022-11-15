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
                if (context.Provider.Any())
                {
                    return;
                }

                context.Provider.AddRange(
                    new Provider { Name = "Поставщик 1" },
                    new Provider { Name = "Поставщик Ранетки" },
                    new Provider { Name = "Поставщик Глюкоза" },
                    new Provider { Name = "Поставщик Яблоки" },
                    new Provider { Name = "Поставщик Груши" }
                );


                context.SaveChanges();
            }
        }
    }
}
