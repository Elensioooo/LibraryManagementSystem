using Application.Interfaces;
using Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.Menus
{
    public class RegistrationMenu
    {
        private readonly IUserService _userService;

        public RegistrationMenu(IUserService userService)
        {
            this._userService = userService;
        }

        public void DisplayMenu()
        {
            try
            {
                Console.WriteLine("Select Your role: ");
                Console.WriteLine("Client - Press 1");
                Console.WriteLine("Admin - Press 2");
                if (!byte.TryParse(Console.ReadLine(), out byte roleChoice))
                    throw new ArgumentException("Please enter a valid number.");

                Roles role;
                switch (roleChoice)
                {
                    case 1:
                        role = Roles.Client;
                        break;
                    case 2:
                        role = Roles.Admin;
                        break;
                    default:
                        throw new ArgumentException("Invalid Role");
                }

                Console.Write("Please Enter your name: ");
                string userName = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(userName))
                    throw new ArgumentException("Username cannot be empty");

                Console.Write("Please Enter your Email: ");
                string email = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(email) || !email.Contains("@") || !email.Contains("."))
                    throw new ArgumentException("Invalid email");

                Console.Write("Please Enter your password: ");
                string password = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(password))
                    throw new ArgumentException("password cannot be emtpy");
                if (password.Length < 8)
                    throw new ArgumentException("Passowrd must include at least 8 characters");
                _userService.RegisterUser(userName, email, password, role);

                Console.Write("Enter verification code: ");
                string code = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(code))
                    throw new ArgumentException("Cone cannot be empty");

                _userService.VerifyUser(email, code);
                Console.WriteLine("Registered successfully");
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
           
        }
    }
}
