using Core.Enums;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public abstract class User
    {
        private int _id;
        private string _userName;
        private string _verificationCode;
        private string _password;
        private string _email;
        public int ID
        {
            get
            {
                return _id;
            }
            set
            {
                
                if (value < 1000 || value > 9999)
                    throw new ArgumentException("ID must be between 1000 and 9999");
                _id = value;
            }

        }

        public string UserName
        {
            get
            {
                return _userName;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Username cannot be empty");
                _userName = value.Trim();
            }
        }

        public Roles Role { get; set; }

        public bool IsVerified { get; set; } = false;
        public string VerificationCode
        {
            get
            {
                return _verificationCode;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentException("verification code cannot be emtpy");
                if (value.Length != 6)
                    throw new ArgumentException("Verificaiotn Code must include 6 characters");

                foreach(char c in value)
                {
                    if (!char.IsLetterOrDigit(c))
                        throw new ArgumentException("Invalid characters.");
                }

                _verificationCode = value;
            }
        }

        public string Email
        {
            get
            {
                return _email;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Email cannot be empty.");
                if (!value.Contains("@") || !value.Contains("."))
                    throw new ArgumentException("Invalid email format.");
                _email = value;
            }
        }

        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Password cannot be empty.");
                _password = value;
            }
        }
    }
}
