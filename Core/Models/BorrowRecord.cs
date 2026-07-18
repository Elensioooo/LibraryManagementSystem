using Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class BorrowRecord
    {
        private int _borrowId;
        private int _userId;
        private int _bookId;
        public BorrowRecord(int borrowId, int userId, int bookId, DateTime requestDate, BorrowStatus status)
        {
            this.BorrowId = borrowId;
            this.UserId = userId;
            this.BookId = bookId;
            this.RequestDate = requestDate;
            this.Status = status;
        }

        public int BorrowId
        {
            get
            {
                return _borrowId;
            }
            set
            {
                if (value <= 0)
                    throw new ArgumentException("BorrowId cannot be negativ or 0");
                _borrowId = value;
            }
        }

        public int UserId
        {
            get { return _userId; }
            set
            {
                if (value <= 0)
                    throw new ArgumentException("User id cannot be negative or 0");
                _userId = value;
            }
        }

        public int BookId
        {
            get { return _bookId; }
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Book id cannot be negative or 0");
                _bookId = value;
            }
        }

        public DateTime RequestDate { get; set; }
       
        public DateTime BorrowDate { get; set; }

        public DateTime ReturnedDate { get; set; }
        public BorrowStatus Status { get; set; }

    }
}
