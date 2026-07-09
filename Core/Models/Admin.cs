using Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Admin : User
    {
        public Admin(int id, string userName,string email, string password, Roles role, decimal fines)
        {
            this.ID = id;
            this.UserName = userName;
            this.Email = email;
            this.Password = password;
            this.Role = role;
            this.Fines = fines;
        }
    }
}
