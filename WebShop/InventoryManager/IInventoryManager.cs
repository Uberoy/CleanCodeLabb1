using Webshop.DataAccess.Entities;

namespace WebShop.ProductManager;

public interface IInventoryManager
{
    Task<IEnumerable<Product>> GetAllProducts();
    Task<Product> GetProductById(string id);
    Task<bool> GetProductStockStatusById(string id);
    Task AddProduct(Product product);
    Task RemoveProductById(string id);
    Task UpdateProductById(string id, Action<Product> updateAction);
}