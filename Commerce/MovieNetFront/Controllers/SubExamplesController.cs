//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Rendering;
//using Microsoft.EntityFrameworkCore;
//using API.Model;
//using MovieNetFront.Repository;
//using System.Threading;

//namespace MovieNetFront.Controllers
//{
//    public class SubExamplesController : Controller
//    {
//        SubExampleRepository repository;
//        ExampleRepository repoExample;

//        public SubExamplesController()
//        {
//            repository = new SubExampleRepository();
//            repoExample = new ExampleRepository();
//        }

//        // GET: Examples
//        public async Task<IActionResult> Index()
//        {
//            var exampleTask = repository.GetAsync(CancellationToken.None);

//            return View(await exampleTask);
//        }

//        // GET: Examples/Details/5
//        public async Task<IActionResult> Details(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var example = repository.GetAsync(CancellationToken.None);

//            await example;

//            var details = example.Result.SingleOrDefault(m => m.Id == id);
//            if (example == null)
//            {
//                return NotFound();
//            }
            
//            return View(details);
//        }

//        // GET: Examples/Create
//        public async Task<IActionResult> Create()
//        {
//            var examples = repoExample.GetAsync(CancellationToken.None);
//            await examples;
//            ViewData["ExampleId"] = new SelectList(examples.Result, "Id", "Name");
//            return View();
//        }

//        // POST: Examples/Create
//        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
//        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Create([Bind("Id,Title,ExampleId")] SubExample example)
//        {
//            if (ModelState.IsValid)
//            {
//                await repository.AddAsync(example);
//                return RedirectToAction(nameof(Index));
//            }
//            return View(example);
//        }

//        // GET: Examples/Edit/5
//        public async Task<IActionResult> Edit(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }
//            var subexamples = repository.GetAsync(CancellationToken.None);

//            await subexamples;
//            var details = subexamples.Result.SingleOrDefault(m => m.Id == id);

//            if (details == null)
//            {
//                return NotFound();
//            }

//            var examples = repoExample.GetAsync(CancellationToken.None);
//            await examples;

//            ViewData["ExampleId"] = new SelectList(examples.Result, "Id", "Name", details.Example.Id);
//            return View(details);
//        }

//        // POST: Examples/Edit/5
//        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
//        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Edit(int id, [Bind("Id,ExampleId,Title")] SubExample example)
//        {
//            if (id != example.Id)
//            {
//                return NotFound();
//            }
//            if (ModelState.IsValid)
//            {
//                try
//                {
//                    await repository.EditAsync(example);
//                }
//                catch (DbUpdateConcurrencyException)
//                {
//                    throw;
//                }
//                return RedirectToAction(nameof(Index));
//            }
//            return View(example);
//        }

//        // GET: Examples/Delete/5
//        public async Task<IActionResult> Delete(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var example = repository.GetAsync(CancellationToken.None);

//            await example;
//            var details = example.Result.SingleOrDefault(m => m.Id == id);

//            if (details == null)
//            {
//                return NotFound();
//            }

//            return View(details);
//        }

//        // POST: Examples/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> DeleteConfirmed(int id)
//        {
//            var example = repository.GetAsync(CancellationToken.None);

//            await example;
//            var details = example.Result.SingleOrDefault(m => m.Id == id);
            
//            await repository.RemoveAsync(details);

//            return RedirectToAction(nameof(Index));
//        }

//        private async Task<bool> ExampleExists(int id)
//        {
//            var exampleTask = repository.GetAsync(CancellationToken.None);

//            await exampleTask;
//            return (exampleTask.Result.Any(e => e.Id == id));
//        }
//    }
//}
