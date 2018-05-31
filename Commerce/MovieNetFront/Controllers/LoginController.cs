using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MovieNetFront.Repository;

namespace MovieNetFront.Controllers
{
    public class LoginController : Controller
    {
        ExampleRepository repository;

        public LoginController()
        {
            repository = new ExampleRepository();
        }

        // GET: Examples
        public async Task<IActionResult> Index()
        {
            var exampleTask = repository.GetAsync(CancellationToken.None);

            return View(await exampleTask);
        }
    }
}