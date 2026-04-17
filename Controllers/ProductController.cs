using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Transport.Sockets;
using RolesAPI.Model;
using System.Linq;
using WebAPI_React.Interfaces;
using WebAPI_React.Model;


namespace WebAPI_React.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        //RoledbContext _context = new RoledbContext();
        //ClassContext _product = new ClassContext();
        private readonly IProductInterface _products;
        public ProductController(IProductInterface products)
        {
            _products = products;
        }

        [HttpGet("[action]")]
        public ActionResult GetAllProductsAPI()
        {
            var products = _products.GetProducts();
            if (products == null)
            {
                return NotFound("Таблица продуктов пуста!");
            }
            return Ok(products);
        }
        [HttpGet("[action]")]
        public ActionResult GetOneProductByIdAPI(int id)
        {
            var product = _products.GetOneProductById(id);
            if (product == null) {
                return NotFound("Не удалось найти товар по Id");
            }
            return Ok(product);

        }
        [HttpPost("[action]")]
        public ActionResult CreateProductAPI([FromBody] Product product)
        {
            _products.CreateProduct(product);
            return Ok();
        }

        [HttpPut("[action]")]
        public ActionResult UpdateProduct([FromBody] Product product, int id)
        {
            _products.UpdateProduct(product, id);
            return Ok("Товар успешно отредактирован!");
        }

        [HttpDelete("[action]")]
        public ActionResult DeleteProductById(int id)
        {
            _products.DeleteProduct(id);
            return Ok("Товар успешно удален!");
        }

        [HttpGet("[action]")]
        public ActionResult GetProductFilter(string name) 
        {
            if (name == null)
            {
                return BadRequest("Поле пустое");
            }
            var tovars = _products.GetProductFilter(name);
            if(!tovars.Any())
            {
                return NotFound("Не удалось найти товар");
            }
            return Ok(tovars);
        }

        [HttpGet("[action]")]
        public ActionResult GetProductSort()
        {
            var sort = _products.GetProductSort();
            if (!sort.Any())
            {
                return NotFound("Не удалось найти товар");
            }
            return Ok(sort);
        }

    }

}
