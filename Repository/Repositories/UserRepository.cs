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
            //ეს იუზერ ობიექტი უნდა დავასერიალიზირო(ანუ სტრინგად გადავაქციო)
            //დავამატო პროსტა ფაილში
            string line = JsonSerializer.Serialize(user);
            File.AppendAllLines(_filePath, new[] { line });
        }

        public void DeleteUser(User user)
        {
            throw new NotImplementedException();
        }

        public List<User> GetAllUsers()
        {
            if (!File.Exists(_filePath))
                return new List<User>();

            string[] lines = File.ReadAllLines(_filePath);
            List<User> users = new List<User>();
            foreach(var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }

                string[] parts = line.Split('|');
                if (parts[3].Trim() == "Client")
                {
                    //Serialization - ობიექტის სტრინგად გადაყვანა
                    //Deserialization - დასერიალიზებული სტრინგის ობიექტად გადაყვანა
                    //line ანუ სტრინგი გადავიყვანე ობიექტად და ჩავწერე clinet-ში

                    //???????????
                    //string serializedLine = JsonSerializer.Serialize(line);
                    //Client client = JsonSerializer.Deserialize<Client>(serializedLine);
                    //users.Add(client);
                    int id = int.Parse(parts[0].Trim());
                    string userName = parts[1].Trim();
                    string email = parts[2].Trim();
                    string password = parts[3].Trim();
                    decimal fines = decimal.Parse(parts[4].Trim());
                    Client client = new Client(id, userName,email, password, Roles.Client, fines);
                    users.Add(client);
                }

                if (parts[3].Trim() == "Admin")
                {
                    int id = int.Parse(parts[0].Trim());
                    string userName = parts[1].Trim();
                    string email = parts[2].Trim();
                    string password = parts[3].Trim();
                    decimal fines = decimal.Parse(parts[4].Trim());
                    Admin admin = new Admin(id, userName,email, password, Roles.Admin, fines);
                    users.Add(admin);
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
            int highestId = 0;
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
    }
}
