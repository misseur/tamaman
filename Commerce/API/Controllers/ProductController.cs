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
    public class ProductController : Controller
    {
        private readonly ExampleContext _context;

        public ProductController(ExampleContext context)
        {
            _context = context;

            if (_context.Products.Count() == 0)
            {
                //// Adds some product
                var prod1 = new Product
                {
                    Name = "Topuillette",
                    Price = 3.20f,
                    UrlImg = "http://soleil.jpg"
                };
                _context.Products.Add(prod1);
                _context.SaveChanges();
            }
        }

        [HttpGet]
        public IEnumerable<Product> GetAll()
        {
            return _context.Products.Include(product => product.Product_Categories).Include(product => product.Order_Products).ToList();
        }

        [HttpGet("{id}", Name = "GetProduct")]
        public Product GetById(int id)
        {
            return _context.Products.Find(id);
        }

        [HttpPost("{id}")]
        public JsonResult Post([FromBody] Product product, int id)
        {
            var categorie = _context.Categories.FirstOrDefault(t => t.Id == id);
            if (categorie == null)
            {
                Response.StatusCode = (int)HttpStatusCode.NotFound;
                return Json(new { message = "Categorie don't exist" });
            }

            var productCategory= new Product_Categorie();
            productCategory.Categorie = categorie;
            productCategory.Product = product;

            _context.Products.Add(product);
            _context.Product_Categories.Add(productCategory);
            _context.SaveChanges();

            return Json(new { product });
        }

        [HttpPut("{id}")]
        public JsonResult Put(int id, [FromBody] Product value)
        {
            var product = _context.Categories.FirstOrDefault(t => t.Id == id);
            if (product == null)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { message = "Product don't exist" });
            }

            _context.Categories.Update(product);
            _context.SaveChanges();

            Response.StatusCode = (int)HttpStatusCode.OK;
            return Json(new { message = "Modified " + product.Id });
        }

        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            var product = _context.Categories.FirstOrDefault(t => t.Id == id);
            if (product == null)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { message = "Product don't exist" });
            }
            _context.Categories.Remove(product);
            _context.SaveChanges();
            Response.StatusCode = (int)HttpStatusCode.OK;
            return Json(new { message = "Deleted " + product.Id });
        }
    }
}
