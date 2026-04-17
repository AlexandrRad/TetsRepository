using WebAPI_React.Interfaces;
using WebAPI_React.Model;

namespace WebAPI_React.Services
{
    public class OrderService : IOrderInterface
    {
        private readonly RoledbContext _context;

        public OrderService(RoledbContext context)
        {
            _context = context;
        }


        public List<Order> GetOrders()
        {
            return _context.Orders.ToList();
        }

        public void AddOrders(Order order)
        {
            var user_in_bd = _context.Users.FirstOrDefault(u => u.IdUsers == order.UserId);
            if (user_in_bd == null)
            {
                throw new KeyNotFoundException("Не удалось найти пользователя!");
            }

            var product_in_db = _context.Products.FirstOrDefault(p => p.IdProduct == order.ProductId);
            if (product_in_db == null)
            {
                throw new KeyNotFoundException("Не удалось найти товар!");
            }

            var addorder = new Order
            {
                UserId = user_in_bd.IdUsers,
                ProductId = product_in_db.IdProduct
            };
            if (addorder == null)
            {
                throw new KeyNotFoundException("addorder(str31) == null");
            }

            _context.Orders.Add(order);
            _context.SaveChanges();
        }
    }
}
