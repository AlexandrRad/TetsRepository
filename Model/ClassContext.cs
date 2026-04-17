using WebAPI_React.Model;

namespace RolesAPI.Model
{
    public class ClassContext
    {
        RoledbContext _conext = new RoledbContext();

        public ClassContext() 
        {
            _conext = new RoledbContext();
        }

        public List<User> GetUsers()
        {
            return _conext.Users.ToList();
        }
        public List<RolesTable> GetRoles()
        {
            return _conext.RolesTables.ToList();
        }

        public List<Product> GetProducts() { 
            return _conext.Products.ToList();
        }
        public List<Order> GetOrders()
        {
            return _conext.Orders.ToList();
        }
    }
}
