using MongoDB.Driver;

namespace Webshop.DataAccess.DbContext;

public interface IMongoDbContext
{
    IMongoCollection<T> GetCollection<T>(string name);
}