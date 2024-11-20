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
