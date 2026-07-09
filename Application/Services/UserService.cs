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
    public class UserService 
    {
        private readonly IUserManeger _fileManager;

        public UserService(IUserManeger fileManager)
        {
            this._fileManager= fileManager;
        }

        public void RegisterUser(string userName, string email, string passwrod, Roles role, decimal fines)
        {
            var existsUser = _fileManager.GetUserByEmail(email);
            if (existsUser != null)
                throw new ArgumentException("User with thie email already exists.");

            var verificationCode = new Random().Next(1000, 9999).ToString();
            int id = _fileManager.GetUserId();
            if (role == Roles.Client)
            {
               
                Client newClient = new Client(id, userName, email, BCrypt.Net.BCrypt.HashPassword(passwrod), Roles.Client, fines);
                _fileManager.AddUser(newClient);
            }
            
            if(role == Roles.Admin)
            {
                Admin newAdmin = new Admin(id, userName, email, BCrypt.Net.BCrypt.HashPassword(passwrod), Roles.Admin, fines);
                _fileManager.AddUser(newAdmin);
            }

        }
    }
}
