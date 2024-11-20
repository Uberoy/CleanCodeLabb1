using Webshop.DataAccess.Entities;
using WebShop.DataAccess.Repositories;

namespace WebShop.UnitOfWork
{
    // Gränssnitt för Unit of Work
    public interface IUnitOfWork : IDisposable
    {
        // Repository för produkter
        // Sparar förändringar (om du använder en databas)
        IProductRepository ProductRepository { get; }
        Task CommitAsync(CancellationToken cancellationToken = default);
        Task RollbackAsync(CancellationToken cancellationToken = default);
        void NotifyProductAdded(Product product); // Notifierar observatörer om ny produkt
    }
}

