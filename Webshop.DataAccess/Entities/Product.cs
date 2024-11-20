using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using WebShop.CommonInterfaces;

namespace Webshop.DataAccess.Entities;

public class Product : IEntity<string>
{
    [BsonRepresentation(BsonType.ObjectId), BsonId]
    public string? Id { get; set; } // Unikt ID för produkten
    public string Name { get; set; } // Namn på produkten
    public int Amount { get; set; }
}