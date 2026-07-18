using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IBookManager
    {
        List<Book> GetAllBooks();

        Book GetBookById(int id);
        List<Book> GetBooksByTitle(string title);
        List<Book> GetBooksByAuthor(string author);
        Book GetBookByIsbn(string isbn);



        void AddBook(Book book);
        void DeleteBook(Book book);
        void SaveChanges(List<Book> books);
        void UpdateBook(Book book);
        int GetBookId();
    }
}
