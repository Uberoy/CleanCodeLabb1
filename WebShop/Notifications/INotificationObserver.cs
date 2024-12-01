using Webshop.DataAccess.Entities;

namespace WebShop.Notifications
{
    public interface INotificationObserver
    {
        void Update(Product product);
    }
}
