using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using WebShop.CommonInterfaces;

namespace Webshop.DataAccess.Entities;

public class Product : IEntity<string>
{
    [BsonRepresentation(BsonType.ObjectId), BsonId]
    public string? Id { get; set; }
    public string Name { get; set; }
    public int Amount { get; set; }
}