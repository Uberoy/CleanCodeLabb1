using MongoDB.Driver;

namespace Webshop.DataAccess.DbContext;

public interface IMongoDbContext
{
    IMongoClient MongoClient { get; }
    IMongoCollection<T> GetCollection<T>(string name);
}