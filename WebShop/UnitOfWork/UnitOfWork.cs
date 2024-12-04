using MongoDB.Driver;
using Webshop.DataAccess.DbContext;
using Webshop.DataAccess.Entities;
using WebShop.Notifications;
using WebShop.DataAccess.Repositories;
using MongoDB.Driver.Core.Clusters;

namespace WebShop.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IMongoDbContext _context;
        private readonly ProductSubject _productSubject;
        private IClientSessionHandle? _session;

        public UnitOfWork(ProductSubject productSubject, IMongoDbContext context)
        {
            _context = context;

            _productSubject = productSubject ?? new ProductSubject();
            _productSubject.Attach(new EmailNotification());
        }
        public IProductRepository ProductRepository => new ProductRepository(_context);
        public async Task CommitAsync(CancellationToken cancellationToken = default)
        {
            if (_session != null)
            {
                await _session.CommitTransactionAsync(cancellationToken);
                _session.Dispose();
                _session = null;
            }
        }
        public async Task RollbackAsync(CancellationToken cancellationToken = default)
        {
            if (_session != null)
            {
                await _session.AbortTransactionAsync(cancellationToken);
                _session.Dispose();
                _session = null;
            }
        }
        public async Task StartTransactionAsync(CancellationToken cancellationToken = default)
        {
            if (_context.MongoClient.Cluster.Description.Type != ClusterType.ReplicaSet)
            {
                return;
            }

            if (_session == null)
            {
                _session = await _context.MongoClient.StartSessionAsync(cancellationToken: cancellationToken);
                _session.StartTransaction();
            }
        }

        public void NotifyProductAdded(Product product)
        {
            _productSubject.Notify(product);
        }
    }
}
