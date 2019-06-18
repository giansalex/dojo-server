using App.API.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System.Threading.Tasks;

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
        public async Task<IActionResult> Register(User user)
        {
            var model = _collection.Find(a => a.Nombres == user.Nombres);
            if (model != null)
            {
                return BadRequest();
            }
            await _collection.InsertOneAsync(user);

            var usuarioInsertado = _collection.Find(a => a.Nombres == user.Nombres);
            return Ok(usuarioInsertado);
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody]User user)
        {
            var model = await _collection.FindAsync(a => a.Nombres == user.Nombres && a.Email == user.Email);
            if (model == null)
                return NotFound();

            return Ok(model);
        }
    }
}