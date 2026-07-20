using Application.Interfaces;
using Core.Enums;
using Core.Interfaces;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class BorrowService : IBorrowService
    {
        private readonly IBorrowManager _borrowManager;
        private readonly IBookManager _bookManager;
        private readonly IUserManeger _userManager;

        public BorrowService(IBorrowManager borrowManager, IBookManager bookManager, IUserManeger userManager)
        {
            this._borrowManager = borrowManager;
            this._bookManager = bookManager;
            this._userManager = userManager;
        }

        public void AcceptBorrowRequest(int borrowRecordId)
        {

            if (borrowRecordId <= 0)
                throw new ArgumentException("Borrow Record Id cannot be negative or 0");
            BorrowRecord borrowRecordById = _borrowManager.GetBorrowRecordById(borrowRecordId);
            if (borrowRecordById.Status != BorrowStatus.Pending)
                throw new ArgumentException("Not pending status!");
  
            int bookId = borrowRecordById.BookId;
            Book book = _bookManager.GetBookById(bookId);
            if (book.Quantity <= 0)
                throw new ArgumentException("Sorry there is no copy left");
            borrowRecordById.Status = BorrowStatus.Accepted;

            borrowRecordById.BorrowDate = DateTime.Now;
            book.Quantity -= 1;

            _bookManager.UpdateBook(book);
            _borrowManager.UpdateBorrowRecord(borrowRecordById);
        }

        public List<BorrowRecord> GetAcceptedBorrowRecords()
        {
            //ისთეი სტატუს მქონა ბოროუ რექორდები უნდა ჩავარდეს, რომელსაც აქვს ექსეფთი სტატუსს
            List<BorrowRecord> borrowRecords = GetAllBorrowRecords();
            List<BorrowRecord> acceptedBorrowRecords = borrowRecords.Where(borrowRecord => borrowRecord.Status == BorrowStatus.Accepted).ToList();
            return acceptedBorrowRecords;
        }

        public List<BorrowRecord> GetAllBorrowRecords()
        {
            List<BorrowRecord> borrowRecords = _borrowManager.GetAllBorrowRecords();
            return borrowRecords;
        }

        public List<BorrowRecord> GetBorrowRecordsByBookId(int bookId)
        {
            if (bookId <= 0)
                throw new ArgumentException("BookId cannot be negative or 0");
            List<BorrowRecord> borrowRecordsById = _borrowManager.GetBorrowRecordsByBookId(bookId);
            return borrowRecordsById;
        }

        public List<BorrowRecord> GetBorrowRecordsByUserId(int userId)
        {
            if (userId <= 0)
                throw new ArgumentException("User id cannot be negative or 0");
            List<BorrowRecord> borrowRecordsByUserId = _borrowManager.GetBorrowRecordsByUserId(userId);
            return borrowRecordsByUserId;
        }

        public void SetOverdueStatus()
        {
            List<BorrowRecord> borrowRecords = GetAllBorrowRecords();
            List<BorrowRecord> acceptedRecords = borrowRecords.Where(borrowRecord => borrowRecord.Status == BorrowStatus.Accepted).ToList();

            List<BorrowRecord> overdueRecords = acceptedRecords.Where(record => record.BorrowDate.AddDays(30) < DateTime.Now).ToList();
            foreach(BorrowRecord record in overdueRecords)
            {
                record.Status = BorrowStatus.Overdue;
                _borrowManager.UpdateBorrowRecord(record);
            }
        }
        public List<BorrowRecord> GetOverdueBorrowRecords()
        {
            List<BorrowRecord> borrowRecords = GetAllBorrowRecords();
            List<BorrowRecord> overdueRecords = borrowRecords.Where(borrowRecord => borrowRecord.Status == BorrowStatus.Overdue).ToList();
            return overdueRecords;
        }

        public void ApplyFine(int borrowRecordId)
        {
            if (borrowRecordId <= 0)
                throw new ArgumentException("Borrow record id cannot be negative or 0");

            BorrowRecord borrowRecord =_borrowManager.GetBorrowRecordById(borrowRecordId);
            if (borrowRecord.Status != BorrowStatus.Overdue)
                throw new ArgumentException("Not oevrdue status");

            User user = _userManager.GetUserById(borrowRecord.UserId);
            Client client = user as Client;
            if (client == null)
                throw new ArgumentException("Only clients get fines");

            decimal fine = CalculateFine(borrowRecordId);
            client.Fines += fine;
            _userManager.UpdateUser(client);
        }

        public decimal CalculateFine(int borrowRecordId)
        {
            if (borrowRecordId <= 0)
                throw new ArgumentException("Borrow record id cannot be negative or 0");

            BorrowRecord borrowRecord = _borrowManager.GetBorrowRecordById(borrowRecordId);
            if (borrowRecord.Status != BorrowStatus.Overdue)
                throw new Exception("Not overdue status");

            decimal finePerOvedueRecord = 5;
            return finePerOvedueRecord;
        }
        public List<BorrowRecord> GetPendingBorrowRequests()
        {
            List<BorrowRecord> borrowRecords = GetAllBorrowRecords();
            List<BorrowRecord> pendingBorrowRecords = borrowRecords.Where(record => record.Status == BorrowStatus.Pending).ToList();
            return pendingBorrowRecords;
        }

        public List<BorrowRecord> GetRejectedBorrowRecords()
        {
            List<BorrowRecord> borrowRecords = GetAllBorrowRecords();
            List<BorrowRecord> rejectedBorrowRecords = borrowRecords.Where(record => record.Status == BorrowStatus.Rejected).ToList();
            return rejectedBorrowRecords;
        }

   

        public void RejectBorrowRequest(int borrowRecordId)
        {
            if (borrowRecordId <= 0)
                throw new ArgumentException("Borrow Record Id cannot be negtive or 0");
            BorrowRecord borrowRecordById = _borrowManager.GetBorrowRecordById(borrowRecordId);
            if (borrowRecordById.Status != BorrowStatus.Pending)
                throw new ArgumentException("Not Pending Status");

            borrowRecordById.Status = BorrowStatus.Rejected;
            _borrowManager.UpdateBorrowRecord(borrowRecordById);

        }

        public void RequestBorrow(int userId, int bookId)
        {   
            if(userId <= 0) 
                throw new ArgumentException("UserId cannot be negative or 0");
            
            if (bookId <= 0)
                throw new ArgumentException("Book id cannot be negative of 0");
            
            var user = _userManager.GetUserById(userId);
            if (user.Role == Roles.Admin)
                throw new ArgumentException("Admin cannot send borrow request");

            Client client = user as Client;
            if (client == null)
                throw new ArgumentException("Only Clients can request books");

            if (client.Fines > 0)
                throw new ArgumentException("You must pay your fines before requesting another book");

            Book book = _bookManager.GetBookById(bookId);
            if (book.Quantity <= 0)
                throw new ArgumentException("There are no available copies of this book");
           
            //მივწვედეთ ამ იუზერის ბოროუ რექორდს
            List <BorrowRecord> userBorrowRecords = _borrowManager.GetBorrowRecordsByUserId(userId);
            bool isRequested = userBorrowRecords.Any(record => record.BookId == bookId && (record.Status == BorrowStatus.Pending || record.Status == BorrowStatus.Accepted || record.Status == BorrowStatus.Overdue));

            if (isRequested)
                throw new ArgumentException("You have already accepted or have a pending request for this book");

            int borrowId = _borrowManager.GetBorrowId();
            BorrowRecord newBorrowRecord = new BorrowRecord(borrowId, userId, bookId, DateTime.Now, BorrowStatus.Pending);
            _borrowManager.AddBorrowRecord(newBorrowRecord);

        }

        public List<BorrowRecord> GetReturnedBorrowRecords()
        {
            List<BorrowRecord> borrowRecords = GetAllBorrowRecords();
            List<BorrowRecord> returnedBorrowRecords = borrowRecords.Where(record => record.Status == BorrowStatus.Returned).ToList();
            return returnedBorrowRecords;
        }


        public void ReturnBook(int borrowRecordId)
        {
            if (borrowRecordId <= 0)
                throw new ArgumentException("borrow Record Id cannot be negative or 0");
            
            BorrowRecord borrowRecord = _borrowManager.GetBorrowRecordById(borrowRecordId);
            if (borrowRecord.Status == BorrowStatus.Returned || borrowRecord.Status == BorrowStatus.Pending || borrowRecord.Status == BorrowStatus.Rejected)
                throw new ArgumentException("Status must be overdue or aceppted");

            if (borrowRecord.Status == BorrowStatus.Overdue)
                ApplyFine(borrowRecord.BorrowId);

            Book book = _bookManager.GetBookById(borrowRecord.BookId);
            book.Quantity += 1;

            borrowRecord.Status = BorrowStatus.Returned;
            borrowRecord.ReturnedDate = DateTime.Now;
            _bookManager.UpdateBook(book);
            _borrowManager.UpdateBorrowRecord(borrowRecord);
        }

   
    }
}
