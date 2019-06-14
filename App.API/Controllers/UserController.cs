using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.API.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace App.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private IMongoCollection<User> collection;
        public UserController()
        {
            var client = new MongoClient("mongodb://admin:admin@10.0.75.1:27017");
            IMongoDatabase db = client.GetDatabase("Demo01");
            this.collection = db.GetCollection<User>("User");
        }

        public IActionResult Index()
        {
            var model = collection.Find(FilterDefinition<User>.Empty).ToList();
            return Ok(model);
        }
        [HttpPost]
        public IActionResult Register(User user)
        {
            var model = collection.Find(a => a.Nombres == user.Nombres);
            if (model != null)
            {
                return BadRequest();
            }
            collection.InsertOne(user);
            var usuarioInsertado = collection.Find(a => a.Nombres == user.Nombres);
            return Ok(usuarioInsertado);

        }

        [HttpPost]
        public IActionResult Login([FromBody]User user)
        {
            var model = collection.Find(a => a.Nombres == user.Nombres && a.Email == user.Email);
            if (model == null)
                return NotFound();

            return Ok(model);
        }
    }
}