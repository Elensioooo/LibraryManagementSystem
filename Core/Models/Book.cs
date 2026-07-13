using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Book
    {
        private int _id;
        private string _isbn;
        private string _title;
        private string _author;
        private int _quantity;

        public Book(string isbn, string title, string author, int quantity, int id)
        {
            this.Isbn = isbn;
            this.Title = title;
            this.Author = author;
            this.Quantity = quantity;
            this.Id = id;
        }

        public string Isbn
        {
            get
            {
                return _isbn;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("ISBN cannot be empty.");
                _isbn = value.Trim();
            }
        }

        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Title cannot be empty");
                _title = value.Trim();
            }
        }


        public string Author
        {
            get
            {
                return _author;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Author cannot be empty");
                _author = value.Trim();
            }
        }

        public int Quantity
        {
            get
            {
                return _quantity;
            }
            set
            {
                if (value < 0)
                    throw new ArgumentException("Quantity cannot be negative");
                _quantity = value;       
            }
        }
        public int Id
        {
            get
            {
                return _id;
            }
            set
            {
                if (value < 0)
                    throw new ArithmeticException("Id cannot be negative");
                _id = value;
            }
        }
    }
}
