using Application.Services;
using Core.Enums;
using Core.Interfaces;
using Repository.Repositories;
using UI.Menus;

namespace UI
{
    internal class Program
    {
        static void Main(string[] args)
        {

            IUserManeger repository = new UserRepository();
            EmailService emailService = new EmailService();
            UserService userService = new UserService(repository, emailService);

            RegistrationMenu registrationMenu = new RegistrationMenu(userService);
            registrationMenu.DisplayMenu();

            LoginMenu login = new LoginMenu(userService);
            login.DisplayMenu();

            //{"ID":1000,"UserName":"Niko","Role":0,"IsVerified":false,"VerificationCode":"664524","Email":"testmariami103@gmail.com","Password":"$2a$11$ZZQPdlDyc/zYdQmzqURWCeyISOW5XHD6h61LLOqnASQw465yO11nO"}

            //userService.RegisterUser("Niko", "testmariami103@gmail.com", "password12357", Roles.Client);
            //userService.VerifyUser("testmariami103@gmail.com", "664524");
            //Console.WriteLine("Run!");

            //var user = userService.Login("testmariami103@gmail.com", "password12357");
            //Console.WriteLine(user.Email);

            Console.ReadKey();
        }
    }
}
