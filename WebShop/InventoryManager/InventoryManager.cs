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
        try
        {
            await _unitOfWork.StartTransactionAsync();
            var products = await _unitOfWork.ProductRepository.GetManyAsync(0, 0);
            if (products == null)
            {
                throw new ProductNotFoundException($"No products found!");
            }
            await _unitOfWork.CommitAsync();
            return products;
        }
        catch (Exception)
        {
            await _unitOfWork.RollbackAsync();
            throw;
        }
    }

    public async Task<Product> GetProductById(string id)
    {
        try
        {
            await _unitOfWork.StartTransactionAsync();
            var product = await _unitOfWork.ProductRepository.GetByIdAsync(id);
            if (product == null)
            {
                throw new ProductNotFoundException($"Product with ID {id} not found.");
            }
            await _unitOfWork.CommitAsync();
            return product;
        }
        catch (Exception)
        {
            await _unitOfWork.RollbackAsync();
            throw;
        }
    }
    public async Task<bool> GetProductStockStatusById(string id)
    {
        try
        {
            await _unitOfWork.StartTransactionAsync();
            var product = await _unitOfWork.ProductRepository.GetByIdAsync(id);
            await _unitOfWork.CommitAsync();
            return product?.Amount > 0;
        }
        catch (Exception)
        {
            await _unitOfWork.RollbackAsync();
            throw;
        }
    }

    public async Task AddProduct(Product product)
    {
        try
        {
            await _unitOfWork.StartTransactionAsync();
            await _unitOfWork.ProductRepository.AddOneAsync(product);
            _unitOfWork.NotifyProductAdded(product);
            await _unitOfWork.CommitAsync();
        }
        catch (Exception)
        {
            await _unitOfWork.RollbackAsync();
            throw;
        }
    }

    public async Task RemoveProductById(string id)
    {
        try
        {
            await _unitOfWork.StartTransactionAsync();
            await _unitOfWork.ProductRepository.DeleteOneAsync(id);
            await _unitOfWork.CommitAsync();
        }
        catch (Exception)
        {
            await _unitOfWork.RollbackAsync();
            throw;
        }
    }
}