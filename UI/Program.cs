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


            //userService.RegisterUser("Niko", "testmariami103@gmail.com", "password12357", Roles.Client);
            //userService.VerifyUser("testmariami103@gmail.com", "664524");
            Console.WriteLine("Run!");

            var user = userService.Login("testmariami103@gmail.com", "password12357");
            Console.WriteLine(user.Email);

            Console.ReadKey();

           
        }
    }
}
