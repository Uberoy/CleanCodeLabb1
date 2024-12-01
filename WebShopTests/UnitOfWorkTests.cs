using Microsoft.EntityFrameworkCore;
using Moq;
using Webshop.DataAccess.DbContext;
using Webshop.DataAccess.Entities;
using WebShop.Notifications;
using WebShop.DataAccess.Repositories;

namespace WebShop.Tests
{
    public class UnitOfWorkTests
    {
        [Fact]
        public void NotifyProductAdded_CallsObserverUpdate()
        {
            // Arrange
            var product = new Product { Id = "1", Name = "Test" };
            var mockObserver = new Mock<INotificationObserver>();
            var mockDbContext = new Mock<IMongoDbContext>();
            var productSubject = new ProductSubject();
            productSubject.Attach(mockObserver.Object);
            var unitOfWork = new UnitOfWork.UnitOfWork(productSubject, mockDbContext.Object);

            // Act
            unitOfWork.NotifyProductAdded(product);

            // Assert
            mockObserver.Verify(o => o.Update(product), Times.Once);
        }
    }
}
