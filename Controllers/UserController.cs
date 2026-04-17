using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using RolesAPI.Model;
using System.Data;
using System.Linq;
using WebAPI_React.Data;
using WebAPI_React.Data.FindYourPrice.Data;
using WebAPI_React.Interfaces;
using WebAPI_React.Model;
using WebAPI_React.Services;
using static WebAPI_React.Interfaces.IUserInterface;

namespace RolesAPI.Controllers
{
    public class UserController : Controller
    {
        //RoledbContext _context = new RoledbContext();
        //ClassContext _user = new ClassContext();
        private readonly IUserInterface _user;
        private readonly ITokenInterface _token;
        private readonly EmailService _emailService;
        public UserController(IUserInterface users, ITokenInterface token, EmailService emailService)
        {
            _user = users;
            _token = token;
            _emailService = emailService;
        }

        [HttpGet("[action]")]
        public ActionResult GetUsersAPI()
        {
            var user = _user.GetUsers();
            if (user == null)
            {
                return NotFound("Пользователи не найдены!");
            }
            return Ok(user);
        }
        [HttpGet("[action]")]
        public ActionResult GetUserProfileAPI(int id)
        {
            var user = _user.GetUserProfile(id);
            if (user == null)
            {
                return NotFound("Не удалось найти пользователя");
            }
            return Ok(user);
        }

        [HttpPost("[action]")]
        public ActionResult AddNewUserAPI([FromBody] AddAndRegistrationNewUserDTO user)
        {
            if (user == null)
            {
                return BadRequest("Тело запроса пустое или не удалось привязать модель CreateUserDto");
            }
            _user.AddNewUser(user);

            return Ok("Пользователь успешно сохранен!");
        }

        [HttpPost("[action]")]
        public ActionResult LoginingAPI([FromBody] LoginingUserDTO user)
        {
            if (user == null)
            {
                return Unauthorized(); // 401 
            }
            var checkUser = _user.Logining(user);
            if (checkUser == null)
            {
                return NotFound("Не удалось войти!");
            }
            var userRole = _user.GetRole(checkUser.RoleId);
            var token = _token.GenerateToken(checkUser.UserName, userRole.RoleName);
            return Ok(new
            {
                checkUser.IdUsers,
                checkUser.UserName,
                checkUser.Login,
                userRole.RoleName,
                token
            });
        }

        [HttpPost("[action]")]
        public ActionResult RegistrationAPI([FromBody] AddAndRegistrationNewUserDTO user)   
        {
            if (user == null)
            {
                return BadRequest("Тело запроса пустое");
            }
            if (string.IsNullOrWhiteSpace(user.Login))
            {
                return BadRequest("Укажите email в поле Login");
            }
            if (!_emailService.IsEmailConfirmed(user.Login))
            {
                return BadRequest("Перед регистрацией подтвердите email кодом из письма");
            }
            _user.Registration(user);
            _emailService.ConsumeConfirmedEmail(user.Login);
            return Ok("Регистрация прошла успешно!");
        }

        [HttpPut("[action]")]
        public ActionResult UpdateByAdminAPI([FromBody] UpdateUserDTO user)
        {
           _user.UpdateByAdmin(user);
            return Ok("Пользователь успешно отредактирован!");
        }

        [HttpDelete("[action]")]
        public ActionResult DeleteByAmdinAPI(int id)
        {
            var user = _user.GetUserProfile(id);
            _user.DeleteUser(id);
            return Ok($"Пользователь Id: {user.UserId} Имя: {user.UserName}\nБыл удален!" );
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> GenerateCodeAPI([FromBody] PostEmail email)
        {
            if (email == null) {
                return BadRequest("Не удалось получить адрес почты");
            }
            try
            {
                var code = await _emailService.GenerateCode(email);
                return Ok(new { message = code });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            catch 
            {
                return StatusCode(500, "Внутренняя ошибка при отправке кода");
            }
        }


        [HttpPost("[action]")]
        public ActionResult VerifyCodeAPI([FromBody] VerifyCode data)
        {
            if (data == null)
            {
                return BadRequest("Данных нет");
            }
            try
            {
                var result = _emailService.VetifyStatusCode(data);
                return Ok(result);
            }
            catch (KeyNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch
            {
                return StatusCode(500, "Внутренняя ошибка при проверке кода");
            }

        }

        [HttpPut("[action]")]
        public ActionResult RememberPasswordAPI(string email)
        {
            _user.RememberPassword(email);
            return Ok("Пароль успешно изменен");
        }

        [HttpGet("[action]")]
        public ActionResult MessageAPI(string str)
        {
            var message =  _user.Message(str);
            return Ok(message);
        }
        


    }
}
