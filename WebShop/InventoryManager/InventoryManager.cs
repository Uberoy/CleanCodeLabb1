using WebShop.UnitOfWork;
using Webshop.DataAccess.Entities;
using WebShop.Exceptions;

namespace WebShop.ProductManager;

public class InventoryManager : IInventoryManager
{
    private readonly IUnitOfWork _unitOfWork;
    public InventoryManager(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<IEnumerable<Product>> GetAllProducts()
    {
        var products = await _unitOfWork.ProductRepository.GetManyAsync(0,0);
        return products;
    }

    public async Task<Product> GetProductById(string id)
    {
        var product = await _unitOfWork.ProductRepository.GetByIdAsync(id);
        if (product == null)
        {
            throw new ProductNotFoundException($"Product with ID {id} not found.");
        }
        return product;
    }
    public async Task<bool> GetProductStockStatusById(string id)
    {
        var product = await _unitOfWork.ProductRepository.GetByIdAsync(id);
        return product?.Amount > 0;
    }

    public async Task AddProduct(Product product)
    {
        await _unitOfWork.ProductRepository.AddOneAsync(product);
        _unitOfWork.NotifyProductAdded(product);
    }

    public async Task RemoveProductById(string id)
    {
        await _unitOfWork.ProductRepository.DeleteOneAsync(id);
    }

    public async Task UpdateProductById(string id, Action<Product> updateAction)
    {
        throw new NotImplementedException();
    }
}