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
            EmailService emailService = new EmailService();
            UserService userService = new UserService(repository, emailService);


            userService.RegisterUser("Niko", "testmariami103@gmail.com", "password12357", Roles.Client);

            Console.WriteLine("Run!");
            Console.ReadKey();

           
        }
    }
}
