using Application.Interfaces;
using Application.Services;
using Core.Enums;
using Core.Interfaces;
using Core.Models;
using Repository.Repositories;
using UI.Menus;

namespace UI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //repos
            IUserManeger userRepository = new UserRepository();
            IBookManager bookRepository = new BookRepository();
            IBorrowManager borrowRepository = new BorrowRecordRepository();

            //email
            EmailService emailService = new EmailService();
            
            //services
            UserService userService = new UserService(userRepository, emailService);
            IBookService bookService = new BookService(bookRepository);
            IBorrowService borrowService = new BorrowService(borrowRepository, bookRepository, userRepository);


            while (true)
            {
                Console.WriteLine("Library Managment System: ");
                Console.WriteLine("1. Registration");
                Console.WriteLine("2. Login");
                Console.WriteLine("3. Exit");
                Console.Write("Your Option: ");
                try
                {
                    string option = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(option))
                        throw new ArgumentException("option cannot be empty");

                    switch (option)
                    {
                        case "1":
                            {
                                Console.WriteLine("Registration: ");
                                RegistrationMenu registrationMenu = new RegistrationMenu(userService);
                                registrationMenu.DisplayMenu();
                                break;
                            }

                        case "2":
                            {
                                Console.WriteLine("Login: ");
                                LoginMenu loginMenu = new LoginMenu(userService);
                                User loggedInUser = loginMenu.DisplayMenu();

                                if (loggedInUser == null)
                                {
                                    Console.WriteLine("Failed login");
                                    break;
                                }

                                if (loggedInUser.Role == Roles.Client)
                                {
                                    ClientMenu clientMenu = new ClientMenu(bookService, borrowService, loggedInUser.ID);
                                    clientMenu.DisplayMenu();
                                }
                                else
                                {
                                    AdminMenu adminMenu = new AdminMenu(bookService, borrowService);
                                    adminMenu.DisplayMenu();
                                }
                                break;
                            }

                        case "3":
                            Console.WriteLine("Exit");
                            return;
                        default:
                            Console.WriteLine("Invalid option!");
                            break;

                    }

                }
                catch(Exception ex) 
                {
                    Console.WriteLine(ex.Message);
                }
            }
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
