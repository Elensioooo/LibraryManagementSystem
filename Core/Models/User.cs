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
        private decimal _fines;
        private string _email;
        public int ID
        {
            get
            {
                return _id;
            }
            set
            {
                
                if (value <= 0)
                    throw new ArgumentException("id cannot be less than 0");
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

        public bool IsVerified { get; set; }
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

        //take care later
        public string Password { get; set; }
        public decimal Fines { get; set; }
        public string Email { get; set; }
    }
}
