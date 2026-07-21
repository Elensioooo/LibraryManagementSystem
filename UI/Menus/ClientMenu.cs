using Application.Interfaces;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.Menus
{
    public class ClientMenu
    {
        private readonly IBookService _bookService;
        private readonly IBorrowService _borrowService;
        private readonly int _currentUserId; //login-ის მერე აქ გამოვატან ამას 

        public ClientMenu(IBookService bookService, IBorrowService borrowService, int currentUserId)
        {
            this._bookService = bookService;
            this._borrowService = borrowService;
            this._currentUserId = currentUserId;
        }

        public void DisplayMenu()
        {
            while (true)
            {

                Console.WriteLine("Client Menu:");
                Console.WriteLine("1. View all books");
                Console.WriteLine("2. Search book by title");
                Console.WriteLine("3. Search book by author");
                Console.WriteLine("4. Request to borrow a book");
                Console.WriteLine("5. Your borrow Records");
                Console.WriteLine("6. Book return");
                Console.WriteLine("7. Logout");
                Console.Write("Your option: ");

                try
                {
                    string option = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(option))
                        throw new ArgumentException("Option cannot be empty");

                    switch (option)
                    {
                        case "1":
                            {
                                Console.WriteLine("View All books: ");
                                List<Book> books = _bookService.ViewAllBooks();
                                foreach (Book book in books)
                                {
                                    Console.WriteLine(book);
                                }
                                break;
                            }
                        case "2":
                            {
                                Console.WriteLine("Search Book by title");
                                Console.Write("Please Enter a title: ");

                                string title = Console.ReadLine();
                                if (string.IsNullOrWhiteSpace(title))
                                    throw new ArgumentException("Title cannot be emtpy");

                                List<Book> books = _bookService.SearchBookByTitle(title);
                                foreach (Book book in books)
                                {
                                    Console.WriteLine(book);
                                }
                                break;
                            }
                        case "3":
                            {
                                Console.WriteLine("Search book by author");
                                Console.Write("Please Enter a author: ");

                                string author = Console.ReadLine();
                                if (string.IsNullOrWhiteSpace(author))
                                    throw new ArgumentException("Author cannot be emtpy");

                                List<Book> books = _bookService.SearchBookByAuthor(author);
                                foreach (Book book in books)
                                {
                                    Console.WriteLine(book);
                                }
                                break;
                            }
                        case "4":
                            {
                                Console.WriteLine("Request to borrow a book");
                                Console.Write("Enter book Id: ");
                                string enteredBookId = Console.ReadLine();
                                int bookId;
                                bool isValidEnteredBookId = int.TryParse(enteredBookId, out bookId);
                                if (!isValidEnteredBookId || bookId <= 0)
                                    throw new ArgumentException("id must be positive");

                                _borrowService.RequestBorrow(_currentUserId, bookId);
                                Console.WriteLine("Request sent successfully");

                                break;
                            }
                        case "5":
                            {
                                Console.WriteLine("Your Borrow Records");
                                List<BorrowRecord> borrowRecords = _borrowService.GetBorrowRecordsByUserId(_currentUserId);
                                if (borrowRecords.Count == 0)
                                {
                                    Console.WriteLine("You do not have borrow records yet");
                                    break;
                                }


                                foreach (BorrowRecord record in borrowRecords)
                                {
                                    Console.WriteLine(record);
                                }
                                break;
                            }
                        case "6":
                            {
                                Console.WriteLine("Book return: ");
                                Console.Write("Enter you borrow record id: ");
                                string inputId = Console.ReadLine();
                                int borrowRecordId;
                                bool isValidInputId = int.TryParse(inputId, out borrowRecordId);
                                if (!isValidInputId || borrowRecordId <= 0)
                                    throw new ArgumentException("id must be positive");

                                //კონკრეტული იუზერის რექორდებს მივწვდი
                                //აქ ვამოწმებ ამ იუზერის რექორდებში მისმიერშემოტანილი აიდის მქონე რექორდი ვაფშე არსებობს თუ არა
                                //სხვა იუზერის რიქორდებზე წვდომა რო არ ქონდეს 
                                List<BorrowRecord> userBorrowRecords = _borrowService.GetBorrowRecordsByUserId(_currentUserId);
                                BorrowRecord foundRecord = userBorrowRecords.FirstOrDefault(record => record.BorrowId == borrowRecordId);
                                if (foundRecord == null)
                                    throw new ArgumentException("This borrow record is not in your borrow records");
                                _borrowService.ReturnBook(borrowRecordId);
                                Console.WriteLine("Returned Successfully");
                                break;

                            }
                        case "7":
                            {
                                Console.WriteLine("Log out");
                                return;
                            }
                        default:
                            Console.WriteLine("Invalid option!");
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
}
