using Microsoft.AspNetCore.Http.HttpResults;
using WebAPI_React.Interfaces;
using WebAPI_React.Model;
using static WebAPI_React.Interfaces.IUserInterface;

namespace WebAPI_React.Services
{
    public class ProductService : IProductInterface
    {
        private readonly RoledbContext _context;

        public ProductService(RoledbContext context)
        {
            _context = context;
        }


        public List<Product> GetProducts()
        {
            return _context.Products.ToList();
        }

        public void CreateProduct(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
        }
        public void UpdateProduct(Product product, int id)
        {
            var product_in_db = GetOneProductById(id);
            if (product_in_db == null)
            {
                return;
            }
            product_in_db.ProductName = product.ProductName;
            product_in_db.Description = product.Description;
            product_in_db.Count = product.Count;
            product_in_db.Category = product.Category;

            _context.Products.Update(product_in_db);
            _context.SaveChanges();
        }
        public void DeleteProduct(int id)
        {
            var product_in_db = _context.Products.FirstOrDefault(p => p.IdProduct == id);
            if (product_in_db == null)
            {
                return;
            }
            _context.Products.Remove(product_in_db);
            _context.SaveChanges();
        }
        public Product GetOneProductById(int id)
        {
           return _context.Products.Find(id);
        }
        public List<Product> GetProductFilter(string name)
        {
            return _context.Products.Where(p => p.ProductName.Contains(name) || p.Category.Contains(name)).ToList();
        }
        public List<Product> GetProductSort()
        {
            return _context.Products.OrderByDescending(p => p.Count).ToList();
        }
    }
}
