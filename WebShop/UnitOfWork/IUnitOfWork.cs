using Webshop.DataAccess.Entities;
using WebShop.DataAccess.Repositories;

namespace WebShop.UnitOfWork
{
    public interface IUnitOfWork
    {
        IProductRepository ProductRepository { get; }
        Task CommitAsync(CancellationToken cancellationToken = default);
        Task RollbackAsync(CancellationToken cancellationToken = default);
        Task StartTransactionAsync(CancellationToken cancellationToken = default);
        void NotifyProductAdded(Product product);
    }
}

