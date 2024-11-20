using MongoDB.Driver;
using Webshop.DataAccess.Entities;

namespace Webshop.DataAccess.DbContext;

public class MongoDbContext : IMongoDbContext
{
    private readonly IMongoDatabase _database;

    public MongoDbContext(string connectionString, string databaseName)
    {
        var client = new MongoClient(connectionString);
        _database = client.GetDatabase(databaseName);
    }
    public IMongoCollection<T> GetCollection<T>(string collectionName)
    {
        var settings = new MongoCollectionSettings
        {
            AssignIdOnInsert = true
        };

        return _database.GetCollection<T>(collectionName, settings);
    }
}