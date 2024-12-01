using Microsoft.EntityFrameworkCore;
using System.Threading;
using Webshop.DataAccess.DbContext;
using Webshop.DataAccess.Entities;
using WebShop.Notifications;
using WebShop.DataAccess.Repositories;

namespace WebShop.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IMongoDbContext _context;
        private readonly ProductSubject _productSubject;

        public UnitOfWork(ProductSubject productSubject, IMongoDbContext context)
        {
            _context = context;

            _productSubject = productSubject ?? new ProductSubject();
            _productSubject.Attach(new EmailNotification());
        }
        public IProductRepository ProductRepository => new ProductRepository(_context);

        public async Task CommitAsync(CancellationToken cancellationToken = default)
        {
            
        }

        public Task RollbackAsync(CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        public void NotifyProductAdded(Product product)
        {
            _productSubject.Notify(product);
        }
    }
}
