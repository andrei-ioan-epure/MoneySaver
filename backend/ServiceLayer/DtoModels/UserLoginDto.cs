using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.DtoModels
{
    public class UserLoginDto
    {
        public  UserLoginDto(string email,string password)
        {
            Email = email;
            Password = password;

        }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
