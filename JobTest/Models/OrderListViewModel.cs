using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobTest.Models
{
    public class OrderListViewModel
    {
        public IEnumerable<Order> Orders { get; set; }
        public SelectList Providers { get; set; }
    }
}
