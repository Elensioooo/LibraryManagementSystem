using Application.Interfaces;
using Core.Enums;
using Core.Interfaces;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class UserService : IUserService
    {
        //ამ ლაინით მე შემიძლია გამოვიძახო ნებისემირეი მეთოდი, რომელიც
        //IUsermanager-ში მავქს გამოცხადბეული. და ეგენი რახან არი იმპლიმენტირებული
        //usermanager-ში მაგიტომ მათი გმაოძახება მოსულა
        private readonly IUserManeger _fileManager;
        private readonly EmailService _emailService;
        
        public UserService(IUserManeger fileManager, EmailService emailService)
        {
            this._fileManager= fileManager;
            this._emailService = emailService;
        }

        public void RegisterUser(string userName, string email, string passwrod, Roles role)
        {
            var existsUser = _fileManager.GetUserByEmail(email);
            if (existsUser != null)
                throw new ArgumentException("User with this email already exists.");

            var verificationCode = new Random().Next(100000, 999999).ToString();
            int id = _fileManager.GetUserId();
            if (role == Roles.Client)
            {
               
                //თავიდან როცა დრეგისტრირდება fines იქნება 0
                Client newClient = new Client(id, userName, email, BCrypt.Net.BCrypt.HashPassword(passwrod),0, verificationCode, false);
                _fileManager.AddUser(newClient);
                //SendVerificationCode(email, verificationCode);
                Console.WriteLine($"Verification code: {verificationCode}");
            }
            
            if(role == Roles.Admin)
            {
                Admin newAdmin = new Admin(id, userName, email, BCrypt.Net.BCrypt.HashPassword(passwrod), verificationCode, false);
                _fileManager.AddUser(newAdmin);
                //SendVerificationCode(email, verificationCode);
                Console.WriteLine($"Verification code: {verificationCode}");
            }
        }


        public void SendVerificationCode(string email, string verificationCode)
        {
            _emailService.SendEmail(email, "Verification Code", verificationCode);
        }

        public bool VerifyUser(string email, string verificationCode)
        {
            User user = _fileManager.GetUserByEmail(email);
            if (user == null)
                throw new ArgumentException("There is no user with this email");

            if(user.VerificationCode == verificationCode)
            {
                user.IsVerified = true;
                _fileManager.UpdateUser(user);
                return true;
            }
            return false;
        }

        public User Login(string email, string password)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Email and password cannot be empty.");

            User foundUser = _fileManager.GetUserByEmail(email);
            if (foundUser == null)
                throw new ArgumentException("User with this email cannot be found");

            if (!BCrypt.Net.BCrypt.Verify(password, foundUser.Password))
                throw new ArgumentException("Incorrect password");

            return foundUser;
        }
    }
}
