using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IUserManeger
    {
        List<User> GetAllUsers();

        User GetUserById(int id);
        User GetUserByUserName(string userName);
        User GetUserByEmail(string email);
        void AddUser(User user);
        void UpdateUser(User user);
        void DeleteUser(User user);
        void SaveChanges(List<User> users);
        int GetUserId();
        //void VerifyUser(string email, string verificationCode);

    }
}
