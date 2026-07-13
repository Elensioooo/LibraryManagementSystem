using Core.Enums;
using Core.Interfaces;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class UserRepository : IUserManeger
    {
        private readonly string _filePath = "C:\\Users\\User\\LibraryManagementSystem\\Repository\\DataFiles\\Users.txt";
       

        public void AddUser(User user)
        {
            string line = JsonSerializer.Serialize(user);
            File.AppendAllLines(_filePath, new[] { line });
        }

        public void DeleteUser(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            List<User> users = GetAllUsers();
            var foundUser = GetUserByEmail(user.Email);
            if (foundUser == null)
                throw new ArgumentException("there is not user with this email");

            users.Remove(foundUser);
            SaveChanges(users);

        }

        public List<User> GetAllUsers()
        {
            if (!File.Exists(_filePath))
                return new List<User>();

            string[] lines = File.ReadAllLines(_filePath);
            List<User> users = new List<User>();

            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                //სტრინგი გადაყავს ობიექტში(line არის სტრინგი და გადაკონვერტირდება ობიექტად)
                using JsonDocument document = JsonDocument.Parse(line);
                //Role ფროფერთის ვწვდები ამით(int32  რო არ მიმეწერა დააბრუნებდა jsonს )
                Roles role = (Roles)document.RootElement.GetProperty("Role").GetInt32();

                if (role == Roles.Client)
                {
                    Client newClient = JsonSerializer.Deserialize<Client>(line);
                    users.Add(newClient);
                }
                else if (role == Roles.Admin)
                {
                    Admin newAdmin = JsonSerializer.Deserialize<Admin>(line);
                    users.Add(newAdmin);
                }
            }

            return users;
        }

        public User GetUserById(int id)
        {
            List<User> users = GetAllUsers();
            var user = users.FirstOrDefault(user => user.ID == id);
            return user;
        }

        public User GetUserByEmail(string email)
        {
            List<User> users = GetAllUsers();
            var user = users.FirstOrDefault(user => user.Email == email);
            return user;
        }

        public User GetUserByUserName(string userName)
        {
            List<User> users = GetAllUsers();
            var user = users.FirstOrDefault(user => user.UserName == userName);
            return user;
        }

        public void UpdateUser(User user)
        {
            List<User> users = GetAllUsers();
            int index = users.FindIndex(u => u.ID == user.ID);
            if(index != -1) 
            {
                users[index] = user;
            }
            SaveChanges(users);
        }

        public int GetUserId()
        {
            List<User> users = GetAllUsers();
            int highestId = 999;
            foreach(User user in users)
            {
                if (user.ID > highestId)
                    highestId = user.ID;
            }
            return highestId + 1;
        }

        public void SaveChanges(List<User> users)
        {
            File.Delete(_filePath);
            File.AppendAllLines(_filePath, users.Select(user => JsonSerializer.Serialize(user)));
        }

        //public  void VerifyUser(string email, string verificationCode)
        //{

        //}
    }
}
