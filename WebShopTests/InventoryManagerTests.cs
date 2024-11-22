using Microsoft.AspNetCore.Mvc;
using Moq;
using WebShop;
using WebShop.Controllers;
using Webshop.DataAccess.Entities;
using WebShop.ProductManager;
using WebShop.DataAccess.Repositories;
using WebShop.UnitOfWork;

public class InventoryManagerTests
{
    private readonly Mock<IProductRepository> _mockProductRepository;
    private readonly Mock<IUnitOfWork> _mockUnitOfWork;
    private readonly InventoryManager _inventoryManager;

    public InventoryManagerTests()
    {
        _mockProductRepository = new Mock<IProductRepository>();
        _mockUnitOfWork = new Mock<IUnitOfWork>();
    }


    [Fact]
    public async Task GetProductStockStatusById_WithAmountGreaterThanZero_ReturnsTrue()
    {
        // Arrange
        string productId = "1";
        var testProduct = new Product { Id = productId, Amount = 10, Name = "Apple" };

        _mockUnitOfWork
            .Setup(uow => uow.ProductRepository.GetByIdAsync(productId))
            .ReturnsAsync(testProduct);

        var inventoryManager = new InventoryManager(_mockUnitOfWork.Object);

        // Act
        var result = await inventoryManager.GetProductStockStatusById(productId);

        // Assert
        Assert.True(result);
        _mockUnitOfWork.Verify(uow => uow.ProductRepository.GetByIdAsync(productId), Times.Once);
    }

    [Fact]
    public async Task GetProductStockStatusById_WithAmountEqualsZero_ReturnsFalse()
    {
        // Arrange
        string productId = "1";
        var testProduct = new Product { Id = productId, Amount = 0, Name = "Apple" };

        _mockUnitOfWork
            .Setup(uow => uow.ProductRepository.GetByIdAsync(productId))
            .ReturnsAsync(testProduct);

        var inventoryManager = new InventoryManager(_mockUnitOfWork.Object);

        // Act
        var result = await inventoryManager.GetProductStockStatusById(productId);

        // Assert
        Assert.False(result);

        _mockUnitOfWork.Verify(uow => uow.ProductRepository.GetByIdAsync(productId), Times.Once);
    }

    [Fact]
    public async Task AddProductToDatabase_WithCorrectInput_CallsRepositoryToAddProduct()
    {
        //Arrange
        var testProduct = new Product { Id = "1", Amount = 1, Name = "Apple" };

        _mockUnitOfWork
            .Setup(uow => uow.ProductRepository.AddOneAsync(testProduct));

        var inventoryManager = new InventoryManager(_mockUnitOfWork.Object);

        //Act
        await inventoryManager.AddProduct(testProduct);

        //Assert
        _mockUnitOfWork.Verify(uow => uow.ProductRepository.AddOneAsync(testProduct), Times.Once);
    }
}