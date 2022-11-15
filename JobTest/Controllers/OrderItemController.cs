using JobTest.Data;
using JobTest.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobTest.Controllers
{
    public class OrderItemController : Controller
    {
        private readonly dbOrderContext _context;

        public OrderItemController(dbOrderContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            IQueryable<OrderItem> orderItems = _context.OrderItem
                .Include(o => o.Order)
                .Where(c => c.OrderId == id);

            List<Order> listOrders = (_context.Order
                .Include(p => p.Provider)
                .Where(k => k.Id == id)).ToList();

            Order orders = new Order
            {
                OrderItems = await orderItems.ToListAsync(),
                Id = listOrders[0].Id,
                Number = listOrders[0].Number,
                Date = listOrders[0].Date,
                Provider = new Provider()
                {
                    Id = listOrders[0].Provider.Id,
                    Name = listOrders[0].Provider.Name
                }
            };
            return View(orders);
        }

        public IActionResult Create(int? OrderItemId)
        {
            ViewData["OrderId"] = OrderItemId;
            return View();
        }
     
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Quantity,Unit,OrderId")] OrderItem orderItem, int? OrderItemId)
        {
            if (ModelState.IsValid)
            {
                orderItem.OrderId = (int)OrderItemId;
                _context.Add(orderItem);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", new { id = OrderItemId });               
            }
            return View(orderItem);
        }
        public async Task<IActionResult> Edit(int? id, int? OrderItemId)
        {
            ViewData["OrderId"] = OrderItemId;
            if (id == null)
            {
                return NotFound();
            }

            var orderItem = await _context.OrderItem.FindAsync(id);
            if (orderItem == null)
            {
                return NotFound();
            }

            return View(orderItem);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Quantity,Unit,OrderId")] OrderItem orderItem, int? OrderItemId)
        {
            if (id != orderItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    orderItem.OrderId = (int)OrderItemId;
                    _context.Update(orderItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderItemExists(orderItem.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", new { id = OrderItemId });
            }
            return View(orderItem);
        }

        public async Task<IActionResult> Delete(int? id, int? OrderItemId)
        {
            ViewData["OrderId"] = OrderItemId;
            if (id == null)
            {
                return NotFound();
            }

            var orderItem = await _context.OrderItem
                .Include(o => o.Order)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orderItem == null)
            {
                return NotFound();
            }

            return View(orderItem);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, int? OrderItemId)
        {
            var orderItem = await _context.OrderItem.FindAsync(id);
            _context.OrderItem.Remove(orderItem);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", new { id = OrderItemId });
        }

        private bool OrderItemExists(int id)
        {
            return _context.OrderItem.Any(e => e.Id == id);
        }
    }
}
