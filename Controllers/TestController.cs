using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI_React.Services;

namespace WebAPI_React.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly TokenService _token;
        public TestController(TokenService token)
        {
            _token = token;
        }
        [HttpPost("[action]")]
        public ActionResult TestJwtTokenAPI(string userName, string Surname)
        {
            if(userName == "test" && Surname == "test1")
            {
                var token = _token.GenerateToken(userName, Surname);
                return Ok(new {token});
            }

           return Unauthorized("Неправильный логин или пароль");
        }
    }
}
