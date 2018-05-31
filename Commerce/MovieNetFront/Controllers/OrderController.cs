using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using API.Model;
using MovieNetFront.Repository;
using System.Threading;

namespace MovieNetFront.Controllers
{
    public class OrderController : Controller
    {
        OrderRepository repository;


        public OrderController()
        {
            repository = new OrderRepository();
        }

        // GET: Examples
        public async Task<IActionResult> Index()
        {
            var exampleTask = repository.GetAsync(CancellationToken.None);

            return View(await exampleTask);
        }

        // GET: Examples/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = repository.GetAsync(CancellationToken.None);

            await order;

            var details = order.Result.SingleOrDefault(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(details);
        }

        // GET: Examples/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Examples/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Order order)
        {
            if (ModelState.IsValid)
            {
                await repository.AddAsync(order);
                return RedirectToAction(nameof(Index));
            }
            return View(order);
        }

        // GET: Examples/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var order = repository.GetAsync(CancellationToken.None);

            await order;
            var details = order.Result.SingleOrDefault(m => m.Id == id);

            if (details == null)
            {
                return NotFound();
            }
            return View(details);
        }

        // 
        // POST: Examples/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Order order)
        {
            if (id != order.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    await repository.EditAsync(order);
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(order);
        }

        // GET: Examples/Delete/5
        public async Task<IActionResult> Remove(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = repository.GetAsync(CancellationToken.None);

            await order;
            var details = order.Result.SingleOrDefault(m => m.Id == id);

            if (details == null)
            {
                return NotFound();
            }

            return View(details);
        }

        // POST: Examples/Delete/5
        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = repository.GetAsync(CancellationToken.None);

            await order;
            var details = order.Result.SingleOrDefault(m => m.Id == id);

            await repository.RemoveAsync(details);

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> OrderExists(int id)
        {
            var exampleTask = repository.GetAsync(CancellationToken.None);

            await exampleTask;
            return (exampleTask.Result.Any(e => e.Id == id));
        }
    }
}
