using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.AccessControl;
using WebAPI_React.Interfaces;
using WebAPI_React.Model;
using static WebAPI_React.Interfaces.IShopInterface;

namespace WebAPI_React.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShopController : ControllerBase
    {
        private readonly IShopInterface _shop;

        public ShopController(IShopInterface shop)
        {
            _shop = shop;
        }

        [HttpGet("[action]")]
        public ActionResult GetAllShopAPI(int id)
        {
            var getData = _shop.GetAllShopOrders(id);
            return Ok(getData);
        }

        [HttpPost]
        public ActionResult AddNewShopOrderAPI([FromBody] AddNewOrder shop, int userId, int productId)
        {
            _shop.AddNewShopOrder(shop, userId, productId);
            return Ok("Товар в корзине успешно сохранен");
        }


        [HttpDelete("[action]")]
        public ActionResult DeleteTovarOfShopAPI(int productId, int userId)
        {
            _shop.DeleteTovarOfShop(productId, userId);
            return Ok("Товар из карзины пользователя успешно удален");
        }
    }
}
