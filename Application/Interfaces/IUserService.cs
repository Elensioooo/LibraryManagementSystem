using Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IUserService
    {
        public void RegisterUser(string userName, string email, string passwrod, Roles role);
        //public void LoginUser(string email, string passwrod);
        public void SendVerificationCode(string email, string verificationCode);

    }
}
