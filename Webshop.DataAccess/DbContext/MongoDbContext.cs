using MongoDB.Driver;
using Webshop.DataAccess.Entities;

namespace Webshop.DataAccess.DbContext;

public class MongoDbContext : IMongoDbContext
{
    private readonly IMongoDatabase _database;
    private readonly IMongoClient _mongoClient;

    public MongoDbContext(string connectionString, string databaseName)
    {
        _mongoClient = new MongoClient(connectionString);
        _database = _mongoClient.GetDatabase(databaseName);
    }

    public IMongoClient MongoClient => _mongoClient;

    public IMongoCollection<T> GetCollection<T>(string collectionName)
    {
        var settings = new MongoCollectionSettings
        {
            AssignIdOnInsert = true
        };

        return _database.GetCollection<T>(collectionName, settings);
    }
}