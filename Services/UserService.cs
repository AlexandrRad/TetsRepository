using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Crypto.Generators;
using WebAPI_React.Interfaces;
using WebAPI_React.Model;
using static WebAPI_React.Interfaces.IUserInterface;

namespace WebAPI_React.Services
{
    public class UserService : IUserInterface
    {
        private readonly RoledbContext _context;
        public readonly EmailService _emailService;
        public UserService(RoledbContext context, EmailService emailService)
        {
            _context = context; 
            _emailService = emailService;
        }

        public List<User> GetUsers()
        {
            return _context.Users.ToList();
        }

        public UserProfileDTO GetUserProfile(int id) 
        {
            var user = _context.Users.FirstOrDefault(u => u.IdUsers == id);
            if (user == null) 
            {
                return null;
            } 
            var role = _context.RolesTables.FirstOrDefault(r => r.IdRoles == user.RoleId);
            if (role == null)
            {
                return null;
            }
            return new UserProfileDTO
            {
                UserName = user.UserName,
                Login = user.Login,
                RoleName = role.RoleName,
                StatusConfirmed = user.StatusConfirmed
            };
        }
        public RolesTable GetRole(int id)
        {
            return _context.RolesTables.Find(id);
        }
        public void AddNewUser(AddAndRegistrationNewUserDTO user)
        {
            var role = GetRole(user.RoleId);
            if (role == null)
            {
                throw new KeyNotFoundException("Не удалось найти роль");
            }
            var newUser = new User
            {
                UserName = user.UserName,
                Login = user.Login,
                Password = user.Password,
                RoleId = user.RoleId
            };
            _context.Users.Add(newUser);
            _context.SaveChanges();
        }
        public User Logining(LoginingUserDTO user)
        {
            var userVerifty = _context.Users.FirstOrDefault(u => u.Login == user.Login);
            if (userVerifty == null)
            {
                return null;
            }

            bool passwordValid = BCrypt.Net.BCrypt.Verify(user.Password, userVerifty.Password);
            if (passwordValid)
            {
                return userVerifty;
            }
            return null;
        }
        public void Registration(AddAndRegistrationNewUserDTO user)
        {
            var role = _context.RolesTables.FirstOrDefault(r => r.RoleName == "User");
            if (role == null)
            {
                throw new KeyNotFoundException("Не удалось определить роль! RegistrationUser");
            }
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(user.Password);

            var newUser = new User
            {
                UserName = user.UserName,
                Login = user.Login,
                Password = passwordHash,
                RoleId = role.IdRoles,
                StatusConfirmed = true
            };
            _context.Users.Add(newUser);
            _context.SaveChanges();
        }
        public void UpdateByAdmin(UpdateUserDTO user)
        {
            var userInDb = _context.Users.FirstOrDefault(u => u.IdUsers == user.UserId);
            if (userInDb == null)
            {
                throw new KeyNotFoundException("Не удалось найти пользователя");
            }
            userInDb.UserName = user.UserName;
            userInDb.Login = user.Login;
            userInDb.Password = user.Password;
            //userInDb.RoleId = user.RoleID;

            _context.Users.Update(userInDb);
            _context.SaveChanges();
        }
        public void DeleteUser(int id)
        {
            var user = _context.Users.FirstOrDefault(u => u.IdUsers == id);
            if (user == null)
            {
                throw new KeyNotFoundException("Пользователь не обнаружен");
            }
            _context.Users.Remove(user);
            _context.SaveChanges();
        }

        public void RememberPassword(string email)
        {
            var user = _context.Users.FirstOrDefault(u => u.Login == email);

            var new_password = new Random().Next(10, 1000).ToString();
            var new_passwordHash = BCrypt.Net.BCrypt.HashPassword(new_password);
           
            _emailService.SendCodeAsync(user.Login, new_password);
           
            user.Password = new_passwordHash;

            _context.Users.Update(user);
            _context.SaveChanges();

        }
    }
}
