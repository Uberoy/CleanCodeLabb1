using MongoDB.Bson;
using MongoDB.Driver;
using Webshop.DataAccess.DbContext;
using Webshop.DataAccess.Entities;

namespace WebShop.DataAccess.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly IMongoCollection<Product> _collection;

    public ProductRepository(IMongoDbContext context)
    {
        _collection = context.GetCollection<Product>("Products");
    }

    public async Task<Product> GetByIdAsync(string id)
    {
        if (!ObjectId.TryParse(id, out var objectId))
        {
            throw new ArgumentException("Invalid ID format", nameof(id));
        }

        var filter = Builders<Product>.Filter.Eq(p => p.Id, objectId.ToString());
        return await _collection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Product>> GetManyAsync(int start, int count)
    {
        var filter = Builders<Product>.Filter.Empty;
        var products = await _collection.Find(filter).Skip(start).Limit(count).ToListAsync();
        return products;
    }

    public async Task AddOneAsync(Product item)
    {
        await _collection.InsertOneAsync(item);
    }

    public async Task PutOneAsync(string id, Product item)
    {
        var filter = Builders<Product>.Filter.Eq(p => p.Id, id);

        var update = Builders<Product>.Update.Set(
            p => p.Amount, item.Amount).Set(
            p => p.Name, item.Name);

        _collection.UpdateOne(filter, update);
    }

    public async Task DeleteOneAsync(string id)
    {
        var filter = Builders<Product>.Filter.Eq(p => p.Id, id);

        _collection.DeleteOne(filter);
    }
}