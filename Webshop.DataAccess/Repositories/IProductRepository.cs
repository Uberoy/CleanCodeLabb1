using WebShop.CommonInterfaces;
using Webshop.DataAccess.Entities;

namespace WebShop.DataAccess.Repositories
{
    // Gränssnitt för produktrepositoryt enligt Repository Pattern
    public interface IProductRepository : IRepository<Product, string>
    {

    }
}
