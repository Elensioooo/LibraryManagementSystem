using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IBookService
    {
        //ეს მუშაობს წესებზე და ოპერაციებზე. 
        //თუ ეს ოპერაცია კონკრეტული როლისთვის დაშვებულია
        //ეს გააკეთ, თუ არადა არ გააკეთო. 
        //მაგ შეიძლერბა ეს წიგნი დაემატოს თ არა, ეს isbn გამოყენებულია თ არა ...
        
        //admin and client methods
        List<Book> ViewAllBooks();
        List<Book> SearchBookByTitle(string title);
        List<Book> SearchBookByAuthor(string author);
        Book SearchByISBN(string Isbn);   
        
        //Admin methods
        void AddBook(Book book);
        void UpdateBook(Book book);
        void DeleteBook(Book book);
        void RemoveCopy(Book book, int quantity);
        void IncreaseQuantity(Book book, int quantity);
        void DecreaseQuantity(Book book, int quantity);

    }
}
