using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IBorrowService
    {
        void RequestBorrow(int userId, int bookId);
        void AcceptBorrowRequest(int borrowRecordId);
        void RejectBorrowRequest(int borrowRecordId);
        void ReturnBook(int borrowRecordId);

        List<BorrowRecord> GetAllBorrowRecords();
        List<BorrowRecord> GetBorrowRecordsByUserId(int userId);
        List<BorrowRecord> GetBorrowRecordsByBookId(int bookId);
        List<BorrowRecord> GetPendingBorrowRequests();
        List<BorrowRecord> GetAcceptedBorrowRecords();
        List<BorrowRecord> GetRejectedBorrowRecords();
        List<BorrowRecord> GetReturnedBorrowRecords();


        void SetOverdueStatus();
        List<BorrowRecord> GetOverdueBorrowRecords();
        decimal CalculateFine(int borrowRecordId);
        void ApplyFine(int borrowRecordId);
        List<BorrowRecord> OneDayLeftRecords();
    }
}
