using JobTest.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobTest.Data
{
    public class dbOrderContext : DbContext
    {
        public dbOrderContext(DbContextOptions<dbOrderContext> options)
            : base(options)
        { 
        }
        public DbSet<Order> Order { get; set; }
        public DbSet<Provider> Provider { get; set; }
    }
}
