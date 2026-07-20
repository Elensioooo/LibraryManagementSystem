using Application.Interfaces;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.Menus
{
    public class AdminMenu
    {
        private readonly IBookService _bookService;
        private readonly IBorrowService _borrowService;

        public AdminMenu(IBookService bookService, IBorrowService borrowService)
        {
            _bookService = bookService;
            _borrowService = borrowService;
        }

        public void DisplayMenu()
        {
            Console.WriteLine("Admin Menu: ");
            Console.WriteLine("1. View all books");
            Console.WriteLine("2. Add book");
            Console.WriteLine("3. Update book");
            Console.WriteLine("4. Remove book copies");
            Console.WriteLine("5. View pending borrow requests");
            Console.WriteLine("6. Accept borrow request");
            Console.WriteLine("7. Reject borrow request");
            Console.WriteLine("8. View all borrow records");
            Console.WriteLine("0. Logout");
            Console.Write("Enter Your Option: ");

            try
            {
                string option = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(option))
                    throw new ArgumentException("option cannot be emtpy");

                switch (option) 
                {
                    case "1":
                        Console.WriteLine("View All books");
                        _bookService.ViewAllBooks();
                        break;
                    case "2":
                        Console.WriteLine("Add book");
                        Console.WriteLine("To be continued...");
                        break;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
