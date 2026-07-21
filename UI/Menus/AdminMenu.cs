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
            while (true)
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
                Console.WriteLine("9. Logout");
                Console.Write("Your Option: ");

                try
                {
                    string option = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(option))
                        throw new ArgumentException("option cannot be emtpy");

                    switch (option)
                    {
                        case "1":
                            {
                                Console.WriteLine("View All books");
                                List<Book> books = _bookService.ViewAllBooks();
                                foreach (Book book in books)
                                {
                                    Console.WriteLine(book.ToString());
                                }
                                break;
                            }
                        case "2":
                            {
                                Console.WriteLine("Add book");

                                Console.Write("Enter Isbn: ");
                                string isbn = Console.ReadLine();
                                if (string.IsNullOrWhiteSpace(isbn))
                                    throw new ArgumentException("ISBN cannot be empty");

                                Console.Write("Enter title: ");
                                string title = Console.ReadLine();
                                if (string.IsNullOrWhiteSpace(title))
                                    throw new ArgumentException("Title cannot be empty");

                                Console.Write("Enter author: ");
                                string author = Console.ReadLine();
                                if (string.IsNullOrWhiteSpace(author))
                                    throw new ArgumentException("Author cannot be empty");

                                Console.Write("Enter quantity: ");
                                string quantityInput = Console.ReadLine();
                                if (!int.TryParse(quantityInput, out int quantity))
                                    throw new ArgumentException("Quantity must be a number");
                                if (quantity <= 0)
                                    throw new ArgumentException("Quantity must be greater than 0");

                                Book book = new Book(isbn, title, author, quantity);
                                _bookService.AddBook(book);
                                Console.WriteLine("Book Added successfully");
                                break;
                            }
                        case "3":
                            {
                                Console.WriteLine("Update Book");

                                Console.Write("Enter book id: ");
                                string inputBookId = Console.ReadLine();
                                bool isValidBookId = int.TryParse(inputBookId, out int bookId);
                                if (!isValidBookId || bookId <= 0)
                                    throw new ArgumentException("Book id must be positive");

                                List<Book> books = _bookService.ViewAllBooks();
                                Book foundBookById = books.FirstOrDefault(b => b.Id == bookId);
                                if (foundBookById == null)
                                    throw new ArgumentException("There is not book with this id");

                                Console.Write("Enter new ISBN: ");
                                string inputIsbn = Console.ReadLine();
                                if (string.IsNullOrWhiteSpace(inputIsbn))
                                    throw new ArgumentException("ISBN cannot be empty.");

                                Console.Write("Title: ");
                                string bookTitle = Console.ReadLine();
                                if (string.IsNullOrWhiteSpace(bookTitle))
                                    throw new ArgumentException("Title cannot be empty");

                                Console.Write("Author: ");
                                string bookAuthor = Console.ReadLine();
                                if (string.IsNullOrWhiteSpace(bookAuthor))
                                    throw new ArgumentException("Author cannot be empty");

                                Console.Write("Quantity: ");
                                string BookQuantity = Console.ReadLine();
                                bool isValidBookQunatity = int.TryParse(BookQuantity, out int quantity);
                                if (!isValidBookQunatity || quantity < 0)
                                    throw new ArgumentException("quantity cannot be negative and it must be a number");

                                foundBookById.Title = bookTitle;
                                foundBookById.Author = bookAuthor;
                                foundBookById.Isbn = inputIsbn;
                                foundBookById.Quantity = quantity;
                                _bookService.UpdateBook(foundBookById);

                                Console.WriteLine("Updated successfully");
                                break;
                            }
                        case "4":
                            {
                                Console.WriteLine("Remove book copies");
                                Console.Write("Enter book ID: ");
                                string bookIdInput = Console.ReadLine();
                                bool isValidBookId = int.TryParse(bookIdInput, out int bookId);
                                if (!isValidBookId || bookId <= 0)
                                    throw new ArgumentException("Book id must be positive");

                                List<Book> books = _bookService.ViewAllBooks();
                                Book foundBookById = books.FirstOrDefault(b => b.Id == bookId);
                                if (foundBookById == null)
                                    throw new ArgumentException("There is no book with this id");

                                Console.Write("Enter Quantoty: ");
                                string quantityInput = Console.ReadLine();
                                bool isValidQuantity = int.TryParse(quantityInput, out int quantity);
                                if (!isValidQuantity || quantity <= 0)
                                    throw new ArgumentException("Quantity must be positive");


                                _bookService.RemoveCopy(foundBookById, quantity);
                                Console.WriteLine("removed successfully");
                                break;
                            }
                        case "5":
                            {
                                Console.WriteLine("Pending Requets: ");
                                List<BorrowRecord> pendingRequests = _borrowService.GetPendingBorrowRequests();
                                foreach (BorrowRecord pendingRequest in pendingRequests)
                                {
                                    Console.WriteLine(pendingRequest);
                                }
                                break;
                            }
                        case "6":
                            {
                                Console.WriteLine("Accept borrow request");
                                Console.Write("Enter borrow request id: ");
                                string id = Console.ReadLine();
                                int borrowId;
                                bool isValidId = int.TryParse(id, out borrowId);

                                if (!isValidId || borrowId <= 0)
                                    throw new ArgumentException("ID MUST be positive");
                                _borrowService.AcceptBorrowRequest(borrowId);
                                Console.WriteLine("Accepted successfully");
                                break;
                            }
                        case "7":
                            {
                                Console.WriteLine("Reject borrow request");
                                Console.Write("Enter request id: ");
                                string borrowId = Console.ReadLine();
                                int id;
                                bool isValidBorrowId = int.TryParse(borrowId, out id);

                                if (!isValidBorrowId || id <= 0)
                                    throw new ArgumentException("id must be positive");

                                _borrowService.RejectBorrowRequest(id);
                                Console.WriteLine("Rejected successfully");
                                break;
                            }
                        case "8":
                            {
                                Console.WriteLine("View all borrow records");
                                List<BorrowRecord> borrowRecords = _borrowService.GetAllBorrowRecords();

                                foreach (BorrowRecord record in borrowRecords)
                                {
                                    Console.WriteLine(record);
                                }
                                break;
                            }
                        case "9":
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
