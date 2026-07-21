using Application.Interfaces;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.Menus
{
    public class LoginMenu
    {
        private readonly IUserService _userService;

        public LoginMenu(IUserService userService)
        {
            _userService = userService;
        }

        public User DisplayMenu()
        {
            try
            {
                Console.Write("Please Enter Your email: ");
                string email = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(email) || !email.Contains("@") || !email.Contains("."))
                    throw new ArgumentException("Invalid email");

                Console.Write("Please Enter you Passowrd: ");
                string password = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(password))
                    throw new ArgumentException("password cannot be emtpy");
                if (password.Length < 8)
                    throw new ArgumentException("Passowrd must include at least 8 characters");

                var user = _userService.Login(email, password);
                Console.WriteLine("logged in successfully");
                return user;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
