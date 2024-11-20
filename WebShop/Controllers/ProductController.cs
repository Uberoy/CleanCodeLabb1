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

        // Endpoint för att hämta alla produkter
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            // Behöver använda repository via Unit of Work för att hämta produkter

            var products = await _inventoryManager.GetAllProducts();

            return Ok(products);
        }

        // Endpoint för att lägga till en ny produkt
        [HttpPost]
        public async Task<ActionResult> AddProduct(Product product)
        {
            // Lägger till produkten via repository

            // Sparar förändringar

            // Notifierar observatörer om att en ny produkt har lagts till

            await _inventoryManager.AddProduct(product);

            return Ok();
        }
    }
}
