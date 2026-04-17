using Microsoft.AspNetCore.SignalR;
using WebAPI_React.Model;

namespace WebAPI_React.Interfaces
{
    public interface IShopInterface
    {
        Object GetAllShopOrders(int id);
        void DeleteTovarOfShop(int productId, int userId);
        void AddNewShopOrder(AddNewOrder shop, int userId, int productId);

        public class AddNewOrder
        {
            public int Count { get; set; }
            public decimal Price {  get; set; }
        }
    }
}
