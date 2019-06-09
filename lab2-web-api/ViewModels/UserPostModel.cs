using lab2_web_api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab2_web_api.ViewModels
{
    public class UserPostModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserRole { get; set; }

        public static User ToUser(UserPostModel userModel)
        {
            UserRole role = lab2_web_api.Models.UserRole.Regular;

            if (userModel.UserRole == "UserManager")
            {
                role = lab2_web_api.Models.UserRole.UserManager;
            }
            else if (userModel.UserRole == "Admin")
            {
                role = lab2_web_api.Models.UserRole.Admin;
            }

            return new User
            {
                FirstName = userModel.FirstName,
                LastName = userModel.LastName,
                Username = userModel.UserName,
                Email = userModel.Email,
                Password = userModel.Password,
                UserRole = role
            };
        }
    }
}
