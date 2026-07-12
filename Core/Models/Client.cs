using Core.Enums;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Client : User
    {
        private decimal _fines;
        public Client(int id, string userName, string email, string password, decimal fines, string verificationCode, bool isVerified)
        {
            this.ID = id;
            this.UserName = userName;
            this.Email = email;
            this.Password = password;
            this.Role = Roles.Client;
            this.Fines = fines;
            this.VerificationCode = verificationCode;
            this.IsVerified = isVerified;
        }

        public decimal Fines
        {
            get
            {
                return _fines;
            }
            set
            {
                if (value < 0)
                    throw new ArgumentException("Fines cannot be negative");
                _fines = value;
            }
        }
    }
}


