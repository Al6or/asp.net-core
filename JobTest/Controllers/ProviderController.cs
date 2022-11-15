using JobTest.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobTest.Controllers
{
    public class ProviderController : Controller
    {
        private readonly dbOrderContext _context;

        public ProviderController(dbOrderContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.Provider.ToListAsync());
        }
    }
}
