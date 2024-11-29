using Microsoft.AspNetCore.Mvc;
using Webshop.DataAccess.Entities;
using WebShop.ProductManager;
using WebShop.UnitOfWork;

namespace WebShop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IInventoryManager _inventoryManager;
        public ProductController(IInventoryManager inventoryManager)
        {
            _inventoryManager = inventoryManager;
        }

        // Endpoint f�r att h�mta alla produkter
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            // Beh�ver anv�nda repository via Unit of Work f�r att h�mta produkter

            var products = await _inventoryManager.GetAllProducts();

            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProductById(string id)
        {
            // Beh�ver anv�nda repository via Unit of Work f�r att h�mta produkter
            Console.WriteLine($"Received ID: {id}"); // Debug log

            try
            {
                var product = await _inventoryManager.GetProductById(id);
                return Ok(product);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

        }
        [HttpGet("{id}/stock")]
        public async Task<ActionResult<bool>> GetProductIsInStockById(string id)
        {
            // Beh�ver anv�nda repository via Unit of Work f�r att h�mta produkter

            var productStockStatus = await _inventoryManager.GetProductStockStatusById(id);

            return Ok(productStockStatus);
        }

        // Endpoint f�r att l�gga till en ny produkt
        [HttpPost]
        public async Task<ActionResult> AddProduct(Product product)
        {
            // L�gger till produkten via repository

            // Sparar f�r�ndringar

            // Notifierar observat�rer om att en ny produkt har lagts till

            await _inventoryManager.AddProduct(product);

            return Ok();
        }
    }
}
