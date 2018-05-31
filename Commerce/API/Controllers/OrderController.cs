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
    public class OrderController : Controller
    {
        private readonly ExampleContext _context;

        public OrderController(ExampleContext context)
        {
            _context = context;

            if (_context.Orders.Count() == 0)
            {
                // Adds User
                var user = new User
                {
                    Name = "Olivier",
                    Password = "Olivier",
                    Admin = false
                };
                // Adds some orders to User
                var order1 = new Order
                {

                    Date = DateTime.Now,
                    User = user
                };
                _context.Orders.Add(order1);
                _context.SaveChanges();
            }
        }

        [HttpGet]
        public IEnumerable<Order> GetAll()
        {
            return _context.Orders;
        }

        [HttpGet("/api/order/all")]
        public JsonResult GetAllOrder()
        {
            var order = _context.Users.Include("Orders.Order_Products.Product.Product_Categories.Categorie").ToList();


            //var order = _context.Orders
            // .Include("Order_Products")
            // .Include("Order_Products.Product")
            // .Include("Order_Products.Product.Product_Categories")
            // .Include("Order_Products.Product.Product_Categories.Categorie");

            return Json(new { order });
        }

        // Renvoie l'Order avec ses infos
        [HttpGet("{id}", Name = "GetOrder")]
        public JsonResult GetById(int id)
        {
            var order = _context.Orders
                .Include("Order_Products")
                .Include("Order_Products.Product")
                .Include("Order_Products.Product.Product_Categories")
                .Include("Order_Products.Product.Product_Categories.Categorie")
                .FirstOrDefault(t => t.Id == id);

            return Json(new { order });
        }

        [HttpPost("{id}", Name = "PostOrder")]
        public JsonResult Post([FromBody] Order order, int id)
        {
            var user = _context.Users.FirstOrDefault(t => t.Id == id);
            if (user == null)
            {
                Response.StatusCode = (int)HttpStatusCode.NotFound;
                return Json(new { message = "Username don't exist" });
            }

            //if(value.Order_Products.Count < 0)
            //{
            //    Response.StatusCode = (int)HttpStatusCode.BadRequest;
            //    return Json(new { message = "Order contains no products" });
            //}

            order.Date = DateTime.Now;
            order.User = user;
            _context.Orders.Add(order);
            _context.SaveChanges();

            Response.StatusCode = (int)HttpStatusCode.OK;
            return Json(new { order });
        }

        [HttpPost("{idUser}/{idProduct}", Name = "PostOrderProduct")]
        public JsonResult Post([FromBody] Order order, int idUser, int idProduct)
        {
            var user = _context.Users.FirstOrDefault(t => t.Id == idUser);
            if (user == null)
            {
                Response.StatusCode = (int)HttpStatusCode.NotFound;
                return Json(new { message = "Username don't exist" });
            }

            var product = _context.Products.FirstOrDefault(t => t.Id == idProduct);
            if (product == null)
            {
                Response.StatusCode = (int)HttpStatusCode.NotFound;
                return Json(new { message = "Product don't exist" });
            }

            order = _context.Orders.FirstOrDefault(t => t.Id == order.Id);
            if (product == null)
            {
                Response.StatusCode = (int)HttpStatusCode.NotFound;
                return Json(new { message = "Product don't exist" });
            }

            var orderProduct = new Order_Product();
            orderProduct.Order = order;
            orderProduct.Product = product;

            //if(value.Order_Products.Count < 0)
            //{
            //    Response.StatusCode = (int)HttpStatusCode.BadRequest;
            //    return Json(new { message = "Order contains no products" });
            //}

            _context.Order_Products.Add(orderProduct);
            _context.SaveChanges();

            Response.StatusCode = (int)HttpStatusCode.OK;
            return Json(new { orderProduct });
        }

        [HttpPut("{id}")]
        public JsonResult Put(int id, [FromBody] Order value)
        {
            var order = _context.Orders.FirstOrDefault(t => t.Id == id);
            if (order == null)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { message = "Order don't exist" });
            }

            _context.Orders.Update(order);
            _context.SaveChanges();

            Response.StatusCode = (int)HttpStatusCode.OK;
            return Json(new { message = "Modified " + order.Id });
        }

        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            var order = _context.Orders.FirstOrDefault(t => t.Id == id);
            if (order == null)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { message = "Order don't exist" });
            }
            _context.Orders.Remove(order);
            _context.SaveChanges();
            Response.StatusCode = (int)HttpStatusCode.OK;
            return Json(new { message = "Deleted " + order.Id });
        }
    }
}
