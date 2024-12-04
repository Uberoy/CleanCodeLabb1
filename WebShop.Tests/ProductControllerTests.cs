using Microsoft.AspNetCore.Mvc;
using Moq;
using WebShop;
using WebShop.Controllers;
using Webshop.DataAccess.Entities;
using WebShop.ProductManager;
using WebShop.DataAccess.Repositories;
using WebShop.UnitOfWork;

public class ProductControllerTests
{
    private readonly Mock<IProductRepository> _mockProductRepository;
    private readonly ProductController _controller;
    private readonly Mock<IInventoryManager> _mockInventoryManager;
    private readonly Mock<IUnitOfWork> _mockUnitOfWork;

    public ProductControllerTests()
    {
        _mockProductRepository = new Mock<IProductRepository>();
        _mockUnitOfWork = new Mock<IUnitOfWork>();
        _mockInventoryManager = new Mock<IInventoryManager>();
    }

    [Fact]
    public async Task GetAllProducts_WithFakeProductList_ReturnsListOfProducts()
    {
        //Assign
        var testProductList = new List<Product>()
        {
            new Product(){Id = "1", Amount = 1, Name = "Apple"},
            new Product(){Id = "2", Amount = 5, Name = "Banana"},
            new Product(){Id = "3", Amount = 30, Name = "Cherry"}
        };
        _mockInventoryManager.Setup(manager => manager.GetAllProducts()).ReturnsAsync(testProductList);
        var productController = new ProductController(_mockInventoryManager.Object);

        //Act
        var apiCall = await productController.GetProducts();

        //Assert
        var okResult = Assert.IsType<OkObjectResult>(apiCall.Result); 
        var returnedProducts = Assert.IsType<List<Product>>(okResult.Value); 
        Assert.Equal(3, returnedProducts.Count); 
        Assert.Equal("Apple", returnedProducts[0].Name); 
    }

    [Fact]
    public async Task GetSpecifiedProduct_WithFakeProductListAndCorrectId_ReturnsAProduct()
    {
        //Assign
        var testProduct = new Product() { Id = "1", Amount = 1, Name = "Apple" };
        _mockInventoryManager.Setup(manager => manager.GetProductById("1")).ReturnsAsync(testProduct);
        var productController = new ProductController(_mockInventoryManager.Object);

        //Act
        var apiCall = await productController.GetProductById("1");

        //Assert
        var okResult = Assert.IsType<OkObjectResult>(apiCall.Result);
        var returnedProduct = Assert.IsType<Product>(okResult.Value);
        Assert.Equal("Apple", returnedProduct.Name);
    }

    [Fact]
    public async Task GetStockStatusForSpecifiedProduct_ReturnsExpectedStatus()
    {
        //Assign
        const string productId = "1"; 
        const bool expectedStockStatus = true;
        _mockInventoryManager.Setup(manager => manager.GetProductStockStatusById("1")).ReturnsAsync(expectedStockStatus);
        var productController = new ProductController(_mockInventoryManager.Object);

        //Act
        var apiCall = await productController.GetProductIsInStockById("1");

        //Assert
        var okResult = Assert.IsType<OkObjectResult>(apiCall.Result);
        var returnedStockStatus = Assert.IsType<bool>(okResult.Value);

        Assert.Equal(expectedStockStatus, returnedStockStatus);
        _mockInventoryManager.Verify(manager => manager.GetProductStockStatusById(productId), Times.Once);
    }
}
