using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Web;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Security.Cryptography;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly ExampleContext _context;

        public UserController(ExampleContext context)
        {
            _context = context;
        }

        // Renvoie la liste de tous les utilisateurs
        [HttpGet]
        public IEnumerable<User> GetAll()
        {
            return _context.Users;
        }

        // Renvoie la liste de tous les utilisateurs avec toutes leur données
        // *** TODO Producategorie de Product et ProductOrder de Order
        [HttpGet("/api/user/all", Name = "GetAllInfo")]
        public JsonResult GetAllInfos()
        {
            var user = _context.Users.Include("Orders.Order_Products.Product.Product_Categories.Categorie").ToList();
            //var user = _context.Users.Include("Orders.Order_Products.Product.Order_Products.Product_Categories.Categorie").ToList();

            return Json(new { user });
        }

        // Renvoie l'utilisateur par son id
        [HttpGet("{id}", Name = "GetUser")]
        public User GetById(int id)
        {
            return _context.Users.Find(id);
        }

        // Renvoie l'User avec ses infos
        // *** TODO Producategorie de Product et ProductOrder de Order
        [HttpGet("{id}/all", Name = "GetByIdAll")]
        public JsonResult GetByIdAll(int id)
        {
            var user = _context.Users.Include("Orders.Order_Products.Product.Product_Categories.Categorie").FirstOrDefault(t => t.Id == id);

            return Json(new { user });
        }

        // Encoder le mot de pass
        private string EncodeMD5(string password)
        {
            string passwordSalt = "etna" + password + "etna";
            return BitConverter.ToString(new MD5CryptoServiceProvider().ComputeHash(ASCIIEncoding.Default.GetBytes(passwordSalt)));
        }

        // Authentification form
        public User Authentificate(string name, string pass)
        {
            string passSalt = EncodeMD5(pass);
            return _context.Users.FirstOrDefault(u => u.Name == name && u.Password == passSalt);
        }

        // Créer un nouvelle utilisateur
        [HttpPost]
        public JsonResult Post([FromBody] User user)
        {
            if (null == user.Name || "".Equals(user.Name) || null == user.Password || "".Equals(user.Password))
            {
                user.Password = EncodeMD5(user.Password);

                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { message = "Bad user provided" });
            }

            var tmp = _context.Users.FirstOrDefault(t => t.Name == user.Name);
            if (tmp != null)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { message = "Username already taken" });
            }
            _context.Users.Add(user);
            _context.SaveChanges();

            Response.StatusCode = (int)HttpStatusCode.Created;
            return Json(user.Name);
        }

        // Modifier l'utilisateur
        [HttpPut("{id}")]
        public JsonResult Put(int id, [FromBody] User value)
        {
            var user = _context.Users.FirstOrDefault(t => t.Id == id);
            if (user == null)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { message = "Username don't exist" });
            }
            if (null != value.Name || !"".Equals(value.Name) || !user.Name.Equals(value.Name))
                user.Name = value.Name;
            if (null != value.Password || !"".Equals(value.Password) || !user.Password.Equals(value.Password))
                user.Password = value.Password;

            _context.Users.Update(user);
            _context.SaveChanges();

            Response.StatusCode = (int)HttpStatusCode.OK;
            return Json(new { message = "Modified " + user.Name });
        }

        // Delete de l'utilisateur
        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            var user = _context.Users.FirstOrDefault(t => t.Id == id);
            if (user == null)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { message = "Username don't exist" });
            }
            _context.Users.Remove(user);
            _context.SaveChanges();
            Response.StatusCode = (int)HttpStatusCode.OK;
            return Json(new { message = "Deleted " + user.Name });
        }
    }
}
