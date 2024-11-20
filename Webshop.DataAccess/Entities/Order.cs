using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Webshop.DataAccess.Entities;
using WebShop.CommonInterfaces;

namespace WebShop;

public class Order : IEntity<string>
{
    [BsonRepresentation(BsonType.ObjectId), BsonId]
    public string Id { get; set; }
    public List<Product> Products { get; set; } = new List<Product>();
    public int TotalCost { get; set; }
}