using JobTest.Data;
using JobTest.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        public async Task<IActionResult> Index(string? Name)
        {
            fProvidersDropDownList(Name);

            IQueryable<Provider> provider = _context.Provider;

            if (!string.IsNullOrEmpty(Name))
            {
                provider = provider.Where(c => c.Name.Contains(Name));
            }

            return View(await provider.ToListAsync());
        }
        private void fProvidersDropDownList(object selectedProvider = null)
        {
            var ProvidersQuery = (from k in _context.Provider
                                  select k.Name).Distinct();
            ViewBag.Name = new SelectList(ProvidersQuery.AsNoTracking(), selectedProvider);
        }
    }
}
