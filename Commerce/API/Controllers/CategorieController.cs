using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using API.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    public class CategorieController : Controller
    {
        private readonly ExampleContext _context;

        public CategorieController(ExampleContext context)
        {
            _context = context;

            if (_context.Categories.Count() == 0)
            {
                // Adds some categories
                var cat1 = new Categorie
                {
                    Name = "Vegetal",
                };
                _context.Categories.Add(cat1);
                _context.SaveChanges();
            }
        }

        //[HttpGet]
        public IEnumerable<Categorie> GetAll()
        {
            return _context.Categories.Include(cat => cat.Product_Categories).ToList();
        }

        [HttpGet("{id}", Name = "GetCategorie")]
        public Categorie GetById(int id)
        {
            return _context.Categories.Find(id);
        }

        [HttpPost]
        public JsonResult Post([FromBody] Categorie categorie)
        {
            if (null == categorie.Name || "".Equals(categorie.Name))
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { message = "Bad categorie provided" });
            }

            var tmp = _context.Categories.FirstOrDefault(t => t.Name == categorie.Name);
            if (tmp != null)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { message = "Categorie name already taken" });
            }
            _context.Categories.Add(categorie);
            _context.SaveChanges();

            return Json(new { categorie });
        }

        [HttpPut("{id}")]
        public JsonResult Put(int id, [FromBody] Categorie value)
        {

            var categorie = _context.Categories.FirstOrDefault(t => t.Id == id);
            if (categorie == null)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { message = "Categorie don't exist" });
            }

            if (categorie.Name != null && !categorie.Equals("") && !categorie.Equals(value.Name))
            {
                Response.StatusCode = (int)HttpStatusCode.OK;
                categorie.Name = value.Name;
                _context.Categories.Update(categorie);
                _context.SaveChanges();
                return Json(new { message = "Modified " + categorie });
            }
            
            return Json(new { message = "Unmodified cause name = null || name =  || already named like this "});
        }

        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            var categorie = _context.Categories.FirstOrDefault(t => t.Id == id);
            if (categorie == null)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { message = "Categorie don't exist" });
            }
            _context.Categories.Remove(categorie);
            _context.SaveChanges();
            Response.StatusCode = (int)HttpStatusCode.OK;
            return Json(new { message = "Deleted " + categorie.Id });
        }
    }
}
