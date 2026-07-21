using Application.Interfaces;
using Core.Interfaces;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class BookService : IBookService
    {
        private readonly IBookManager _bookManager;
        
        public BookService(IBookManager bookManager)
        {
            this._bookManager = bookManager;
        }
        public void AddBook(Book book)
        {
            //ვამატებ წიგნს ლაიბრარიში(ჯენერალ ლისთში ჩაემატბება)
            if (book == null)
                throw new ArgumentNullException(nameof(book));
            List<Book> books = _bookManager.GetAllBooks();
            Book foundBooks = books.FirstOrDefault(b => b.Isbn == book.Isbn);

            if (foundBooks != null)
                throw new ArgumentException("The library has book with this ISBN");
            book.Id = _bookManager.GetBookId();
            book.Id = _bookManager.GetBookId();
            _bookManager.AddBook(book);
        }

        public void DecreaseQuantity(Book book, int quantity)
        {
            //უკვე არსებული წგინის კოპის გამოკლება(აი როცა გაიტანს კლიენტი რო გამოკალდეს)
            if (book == null)
                throw new ArgumentNullException(nameof(book));
            if (quantity <= 0)
                throw new ArgumentException("Quantity cannot be negative or 0");
            
            List<Book> books = _bookManager.GetAllBooks();
            Book foundBook = books.FirstOrDefault(b => b.Isbn == book.Isbn);
            
            if (foundBook == null)
                throw new ArgumentException("There is not book with the given book ISBN");
            
            if(foundBook.Quantity < quantity)
                throw new ArgumentException("We do not have that many copies");
            foundBook.Quantity -= quantity;
            _bookManager.UpdateBook(foundBook);
        }

        public void RemoveCopy(Book book, int quantity)
        {
            if (book == null)
                throw new ArgumentNullException(nameof(book));
            if (quantity <= 0)
                throw new ArgumentException("Quantity cannot be negative or  0");
            
            List<Book> books = _bookManager.GetAllBooks();
            Book foundBook = books.FirstOrDefault(b => b.Isbn == book.Isbn);
            if (foundBook == null)
                throw new ArgumentException("There is not book with the given book ISBN");

            if (quantity > foundBook.Quantity)
                throw new ArgumentException("We do not have that many copies");
            foundBook.Quantity -= quantity;
            _bookManager.UpdateBook(foundBook);

        }

        public void DeleteBook(Book book)
        {
            if (book == null)
                throw new ArgumentNullException(nameof(book));
            List<Book> books = _bookManager.GetAllBooks();
            Book foundBook = books.FirstOrDefault(b => b.Isbn == book.Isbn);
            if(foundBook == null)
                throw new ArgumentException("There is not book with the given book ISBN");

            _bookManager.DeleteBook(foundBook);
        }
        public void IncreaseQuantity(Book book, int quantity)
        {
            //უკვე არსებული წიგნის კოპის დამატება
            //მაგ ჰარი პოტერის 5 წიგნი შემომაქ
            if (book == null)
                throw new ArgumentNullException(nameof(book));
            if (quantity <= 0)
                throw new ArgumentException("Quantity cannot be negative or 0");
            List<Book> books = _bookManager.GetAllBooks();
            Book foundBook = books.FirstOrDefault(b => b.Isbn == book.Isbn);
            if (foundBook == null)
                throw new ArgumentException("There is not book with the given book ISBN");
            foundBook.Quantity += quantity;
            _bookManager.UpdateBook(foundBook);
        }

        public Book SearchByISBN(string Isbn)
        {
            if (string.IsNullOrWhiteSpace(Isbn))
                throw new ArgumentException("Isbn cannot be empty");
            Book foundBook = _bookManager.GetBookByIsbn(Isbn);
            if (foundBook == null)
                throw new ArgumentException("There is no book with this isbn");
            return foundBook;
        }

        public List<Book> SearchBookByAuthor(string author)
        {
            if (string.IsNullOrWhiteSpace(author))
                throw new ArgumentException("Author cannot be empty");
            List<Book> foundBooks = _bookManager.GetBooksByAuthor(author);
            if (foundBooks.Count == 0)
                throw new ArgumentException("There is no book with this author");
            return foundBooks;
        }

        public List<Book> SearchBookByTitle(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Title cannot be empty");
            List<Book> foundBooks = _bookManager.GetBooksByTitle(title);
            if (foundBooks.Count == 0)
                throw new ArgumentException("There is no book with this title");
            return foundBooks;
        }

        public void UpdateBook(Book book)
        {
            if (book == null)
                throw new ArgumentNullException(nameof(book));
            Book foundBookById = _bookManager.GetBookById(book.Id);
            if (foundBookById == null)
                throw new ArgumentException("There is no book with this id");

            Book foundBookByIsbn = _bookManager.GetBookByIsbn(book.Isbn);
            if (foundBookByIsbn != null && foundBookByIsbn.Id != book.Id)
                throw new ArgumentException("Given isbn is used in another book");

            _bookManager.UpdateBook(book);
        }


        public List<Book> ViewAllBooks()
        {
            List<Book> books = _bookManager.GetAllBooks();
            return books;
        }
    }
}
