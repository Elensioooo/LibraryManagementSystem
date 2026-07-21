using Core.Interfaces;
using Core.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class BookRepository : IBookManager
    {
        //ეს მუშაობს ფაილზე, როგორ წაიკითხოს დატა
        private readonly string _filePath = "C:\\Users\\User\\LibraryManagementSystem\\Repository\\DataFiles\\Books.txt";

        public void AddBook(Book book)
        {
           if (book == null)
                throw new ArgumentNullException(nameof(book));

            string line = JsonSerializer.Serialize(book);
            File.AppendAllLines(_filePath, new[] { line });
        }

        public void DeleteBook(Book book)
        {
            if (book == null)
                throw new ArgumentNullException(nameof(book));

            List<Book> books = GetAllBooks();
            Book foundBook = GetBookById(book.Id);
            books.Remove(foundBook);
            SaveChanges(books);
        }

        public List<Book> GetAllBooks()
        {
            if (!File.Exists(_filePath))
                return new List<Book>();

            string[] lines = File.ReadAllLines(_filePath);
            List<Book> books = new List<Book>();

            foreach(var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                Book book = JsonSerializer.Deserialize<Book>(line);
                //აქ ეს ბუქი რაღაცნაირად ნალი რო იყოს მერე რა უდნა ქნდა მოიფიქრე
                books.Add(book);
            }
            return books;
        }

        public List<Book> GetBooksByAuthor(string author)
        {
            if (string.IsNullOrWhiteSpace(author))
                throw new ArgumentException("Author cannot be empty");
            List<Book> books = GetAllBooks();
            //ეხლა დააბრუნებს მაშინაც თუ სრულად არ დაწერს ავტორის სახლ გვრ
            List<Book> foundBooks = books.Where(book => book.Author.Contains(author, StringComparison.OrdinalIgnoreCase)).ToList();
            if (foundBooks.Count == 0)
                throw new ArgumentException("There are no books by this author");
            return foundBooks;
        }

        public Book GetBookById(int id)
        {
            if (id <= 0)
                throw new ArgumentException("id cannot be negative or 0");
            List<Book> books = GetAllBooks();
            Book foundBook = books.FirstOrDefault(book => book.Id == id);
            if (foundBook == null)
                throw new ArgumentException("There is no book with this id");
            return foundBook;
        }

        public Book GetBookByIsbn(string isbn)
        {
            if(string.IsNullOrWhiteSpace(isbn))
                throw new ArgumentException("Isbn cannot be empty");
            List<Book> books = GetAllBooks();
            Book foundBook = books.FirstOrDefault(book => book.Isbn == isbn);
           
            return foundBook;
        }

        public int GetBookId()
        {
            //ეს იმისთის რომ აიდები სწორად გავზარდო
            List<Book> books = GetAllBooks();
            int highestId = 0;
            foreach(Book book in books)
            {
                if (book.Id > highestId)
                    highestId = book.Id;
            }
           return highestId + 1;
        }

        public List<Book> GetBooksByTitle(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("The title cannot be empty");
            List<Book> books = GetAllBooks();
            List<Book> foundBooks = books.Where(book => book.Title.Contains(title, StringComparison.OrdinalIgnoreCase)).ToList();
            if (foundBooks.Count == 0)
                throw new ArgumentException("There is no book with this title");
            return foundBooks;
                
        }

        public void SaveChanges(List<Book> books)
        {
            //File.Delete(_filePath);
            //writeAllLine - ეს არ შელის ფაილს უბრალოდ ანაცვლებს მთლიან კონტენტს
            File.WriteAllLines(_filePath, books.Select(book=> JsonSerializer.Serialize(book)));
        }

        public void UpdateBook(Book book)
        {
            if (book == null)
                throw new ArgumentNullException(nameof(book));

            List<Book> books = GetAllBooks();
            int index = books.FindIndex(b => b.Id == book.Id);
            if (index == -1)
                throw new ArgumentException("There is no book with this ID.");
            books[index] = book;
            SaveChanges(books);
        }
    }
}
