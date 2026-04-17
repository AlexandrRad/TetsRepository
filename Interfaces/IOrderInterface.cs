using WebAPI_React.Model;

namespace WebAPI_React.Interfaces
{
    public interface IOrderInterface
    {
        List<Order> GetOrders();
        void AddOrders(Order order);
    }
}
