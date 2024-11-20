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

        // Ställ in mock av Products-egenskapen
    }
}
