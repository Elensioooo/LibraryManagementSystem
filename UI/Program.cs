using Application.Services;
using Core.Enums;
using Core.Interfaces;
using Repository.Repositories;

namespace UI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IUserManeger repository = new UserRepository();
            UserService userService = new UserService(repository);

            userService.RegisterUser("Elene", "elene@gmail.com", "12357", Roles.Client, 5);
            Console.WriteLine("Hello, World!");
            Console.ReadKey();
        }
    }
}
