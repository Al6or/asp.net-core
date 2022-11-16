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
    public class OrderController : Controller
    {
        private readonly dbOrderContext _context;

        public OrderController(dbOrderContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(string? Provider, string? Number, string? dateStart, string? dateEnd)
        {
            fProvidersDropDownList(Provider);
            fNumbersDropDownList(Number);
            IQueryable<Order> orders = _context.Order.Include(k => k.Provider);

            if (!string.IsNullOrEmpty(Provider))
            {
                orders = orders.Where(k => k.Provider.Name.Contains(Provider));
            }

            if (!string.IsNullOrEmpty(Number))
            {
                orders = orders.Where(c => c.Number == Number);
            }

            if (!string.IsNullOrEmpty(dateStart) && !string.IsNullOrEmpty(dateEnd))
            {
                orders = orders.Where(c => c.Date >= Convert.ToDateTime(dateStart)
                                        && c.Date <= Convert.ToDateTime(dateEnd));
            }

            return View(await orders.ToListAsync());
        }
        public IActionResult Create()
        {
            ProvidersDropDownList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Number,Date,ProviderId")] Order order)
        {
            if (ModelState.IsValid)
            {
                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ProvidersDropDownList(order.ProviderId);
            return View(order);
        }
        

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            ProvidersDropDownList();
            return View(order);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Number,Date,ProviderId")] Order order)
        {
            if (id != order.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "OrderItem", new { id = order.Id });
            }
            ProvidersDropDownList(order.ProviderId);
            return View(order);
        }
        //для добавления/редактирования
        private void ProvidersDropDownList(object selectedProvider = null)
        {
            var ProvidersQuery = from k in _context.Provider
                                 orderby k.Id
                                 select k;
            ViewBag.ProviderId = new SelectList(ProvidersQuery.AsNoTracking(), "Id", "Name", selectedProvider);
        }
        //для фильтра
        private void fProvidersDropDownList(object selectedProvider = null)
        {
            var ProvidersQuery = (from k in _context.Provider
                                 select k.Name).Distinct();
            ViewBag.Name = new SelectList(ProvidersQuery.AsNoTracking(), selectedProvider);
        }

        private void fNumbersDropDownList(object selectedNumber = null)
        {
            var NumbersQuery = (from k in _context.Order
                                select k.Number).Distinct();
            ViewBag.Number = new SelectList(NumbersQuery.AsNoTracking(), selectedNumber);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order
                 .Include(c => c.Provider)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Order.FindAsync(id);
            _context.Order.Remove(order);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
            return _context.Order.Any(e => e.Id == id);
        }

        [AcceptVerbs("GET", "POST")]
        public IActionResult VerifyNumber(string number, int providerId)
        {

            List<Order> uniqueness = _context.Order
                .Include(k => k.Provider)
                .Where(c => c.ProviderId == providerId).ToList();

            uniqueness = uniqueness.Where(c => c.Number == number).ToList();

            if (uniqueness.Count() > 0)
            {
                return Json($"Заказ с \"{number}\" существует у поставщика \"{uniqueness[0].Provider.Name}\"");
            }

            return Json(true);
        }
    }
}
