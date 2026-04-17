using Microsoft.AspNetCore.Http.HttpResults;
using WebAPI_React.Interfaces;
using WebAPI_React.Model;
using static WebAPI_React.Interfaces.IShopInterface;

namespace WebAPI_React.Services
{
    public class ShopService : IShopInterface
    {
        private readonly RoledbContext _context;
        public ShopService(RoledbContext context)
        {
            _context = context;
        }

        public  Object GetAllShopOrders(int id)
        {
            
            var shopsUser = _context.Shops.Where(u => u.IdUser == id).ToList();
            var finalySum = shopsUser.Sum(s => s.Count * s.Price);

            return new
            {
                shopsUser,
                finalySum
            };
        }

        public void AddNewShopOrder(AddNewOrder shop, int userId, int productId)
        {
            var user = _context.Users.FirstOrDefault(u => u.IdUsers == userId);
            if (user == null)
            {
                return;
            }
            var product = _context.Products.FirstOrDefault(p => p.IdProduct == productId);
            if (product == null)
            {
                return;
            }

            var newShopOrder = new Shop
            {
                Count = shop.Count,
                Price = shop.Price,
                IdProduct = product.IdProduct,
                IdUser = user.IdUsers
            };

            _context.Shops.Add(newShopOrder);
            _context.SaveChanges();
        }

        public void DeleteTovarOfShop(int productId, int userId)
        {
            var order = _context.Shops.FirstOrDefault(u => u.IdUser == userId && u.IdProduct == productId);
            if (order == null)
            {
                throw new KeyNotFoundException("Не удалось найти юзера или товар");
            }
            _context.Shops.Remove(order);
            _context.SaveChanges();
        }
    }

    // Сделать удаление товара из корзины по userid
    // Сделать добавление
    // Vue сделать верстку корзины 
}
