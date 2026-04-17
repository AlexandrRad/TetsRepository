using WebAPI_React.Model;

namespace WebAPI_React.Interfaces
{
    public interface IProductInterface
    {
        List<Product> GetProducts();
        Product GetOneProductById(int id);
        void CreateProduct(Product product);
        void UpdateProduct(Product product, int id);
        void DeleteProduct(int id);
        List<Product> GetProductFilter(string name);
        List<Product> GetProductSort();

       
    }
}
