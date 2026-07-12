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
                SendVerificationCode(email, verificationCode);
            }
            
            if(role == Roles.Admin)
            {
                Admin newAdmin = new Admin(id, userName, email, BCrypt.Net.BCrypt.HashPassword(passwrod), verificationCode, false);
                _fileManager.AddUser(newAdmin);
            }
        }


        public void SendVerificationCode(string email, string verificationCode)
        {
            _emailService.SendEmail(email, "Verification Code", verificationCode);
        }

    }
}
