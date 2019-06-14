using App.API.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace App.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private IMongoCollection<User> _collection;

        public UserController(IMongoCollection<User> collection)
        {
            _collection = collection;
        }

        public IActionResult Index()
        {
            var model = _collection.Find(FilterDefinition<User>.Empty).ToList();
            return Ok(model);
        }
        [HttpPost]
        public IActionResult RegisterUser(User user)
        {
            var model = _collection.Find(a => a.Nombres == user.Nombres);
            if (model != null)
            {
                return BadRequest();
            }

            _collection.InsertOne(user);
            var usuarioInsertado = _collection.Find(a => a.Nombres == user.Nombres);
            return Ok(usuarioInsertado);

        }
    }
}