namespace WebShop.CommonInterfaces;

public interface IEntity<T>
{
    T Id { get; set; }
}