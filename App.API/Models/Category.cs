using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace App.API.Models
{
    public class Category
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string CategoryId { get; set; }
        [BsonElement]
        public string CategoryName { get; set; }
        [BsonElement]
        public string Description { get; set; }
    }
}
