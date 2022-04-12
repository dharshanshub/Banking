using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankEntity
{
    public class AuthenticatedUser<T>
    {
        public T UserId { get; set; }
        public string Name { get; set; }    
        public string RoleName { get; set; }
        public string Token { get; set; }

        public AuthenticatedUser(T userId, string name, string roleName, string token)
        {
            UserId = userId; Name = name; RoleName = roleName; Token = token;
        }

    }
}







