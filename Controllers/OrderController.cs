using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RolesAPI.Model;
using System.ComponentModel;
using WebAPI_React.Interfaces;
using WebAPI_React.Model;



namespace WebAPI_React.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        //RoledbContext _context = new RoledbContext();
        //ClassContext _classContext = new ClassContext();
        private readonly IOrderInterface _order;
        public OrderController(IOrderInterface orders)
        {
            _order = orders;
        }

        [HttpGet("[action]")]
        public ActionResult ShowOrders()
        {
            var order = _order.GetOrders();
            if (order == null)
            {
                return NotFound("Не удалось найти заказы!");
            }
            return Ok(order);
        }


        [HttpPost("[action]")]
        public ActionResult AddOrders([FromBody] Order order)
        {
            _order.AddOrders(order);
            return Ok("Товар успешно привязан к пользователю");
        }
    }
}
