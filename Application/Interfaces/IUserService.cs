using Core.Enums;
using Core.Models;
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
       
        public void SendVerificationCode(string email, string verificationCode);
        public bool VerifyUser(string email, string verificationCode);
        public User Login(string email, string password);
    }
}
