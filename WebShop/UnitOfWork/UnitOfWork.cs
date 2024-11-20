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
        // Hämta produkter från repository
        private readonly IMongoDbContext _context;
        private readonly ProductSubject _productSubject;

        // Konstruktor används för tillfället av Observer pattern
        public UnitOfWork(ProductSubject productSubject, IMongoDbContext context)
        {
            _context = context;

            //// Om inget ProductSubject injiceras, skapa ett nytt
            _productSubject = productSubject ?? new ProductSubject();

            //// Registrera standardobservatörer
            _productSubject.Attach(new EmailNotification());
        }
        public IProductRepository ProductRepository => new ProductRepository(_context);

        public async Task CommitAsync(CancellationToken cancellationToken = default)
        {
            
        }

        public Task RollbackAsync(CancellationToken cancellationToken = default)
        {
            //foreach (var entry in _context.ChangeTracker.Entries())
            //{
            //    entry.State = EntityState.Detached;
            //}

            //return Task.CompletedTask;
            return Task.CompletedTask;
        }

        public void NotifyProductAdded(Product product)
        {
            _productSubject.Notify(product);
        }

        public void Dispose()
        {
            //_context.Dispose();
        }
    }
}
