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
    public class ValuesController : ControllerBase
    {
        private IMongoCollection<Category> collection;
        public ValuesController()
        {
            var client = new MongoClient("mongodb://admin:admin@10.0.75.1:27017");
            IMongoDatabase db = client.GetDatabase("Demo01");
            this.collection = db.GetCollection<Category>("Category");
        }

        public IActionResult Index()
        {
            var model = collection.Find(FilterDefinition<Category>.Empty).ToList();
            return Ok(model);
        }

    }
}
