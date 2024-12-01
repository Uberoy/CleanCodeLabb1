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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var products = await _inventoryManager.GetAllProducts();

            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProductById(string id)
        {
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
            var productStockStatus = await _inventoryManager.GetProductStockStatusById(id);

            return Ok(productStockStatus);
        }

        [HttpPost]
        public async Task<ActionResult> AddProduct(Product product)
        {
            await _inventoryManager.AddProduct(product);

            return Ok();
        }
    }
}
