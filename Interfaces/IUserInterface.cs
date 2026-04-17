using WebAPI_React.Model;

namespace WebAPI_React.Interfaces
{
    public interface IUserInterface
    {
        List<User> GetUsers();
        RolesTable GetRole(int id);
        UserProfileDTO GetUserProfile(int id);
        void AddNewUser(AddAndRegistrationNewUserDTO user);
        void Registration(AddAndRegistrationNewUserDTO user);
        User Logining(LoginingUserDTO user);
        void UpdateByAdmin(UpdateUserDTO user);
        void DeleteUser(int id);
        void RememberPassword(string email);

        string Message(string str);
        public class AddAndRegistrationNewUserDTO
        {
        public string UserName { get; set; } = string.Empty;
        public string Login { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public int RoleId { get; set; }
        }
        public class LoginingUserDTO
        {
            public string Login { get; set; } = string.Empty;
            public string Password { get; set; } = string.Empty;
        }
      
        public class UpdateUserDTO
        {
            public int UserId { get; set; }
            public string UserName { get; set; } = string.Empty;
            public string Login { get; set; } = string.Empty;
            public string Password { get; set; } = string.Empty;
            public int RoleID { get; set; }
        }
        public class UserProfileDTO
        {
            public int UserId { get; set; }
            public string UserName { get; set; } = string.Empty;
            public string Login { get; set; } = string.Empty;
            public string RoleName { get; set; } = string.Empty;
            public bool StatusConfirmed { get; set; } = false;
        }
    }
}
