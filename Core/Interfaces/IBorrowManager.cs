using Core.Enums;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IBorrowManager
    {
        void AddBorrowRecord(BorrowRecord borrowRecord);
        List<BorrowRecord> GetAllBorrowRecords();
        BorrowRecord GetBorrowRecordById(int id);
        List<BorrowRecord> GetBorrowRecordsByUserId(int userId);
        List<BorrowRecord> GetBorrowRecordsByBookId(int bookId);
        List<BorrowRecord> GetBorrowRecordsByStatus(BorrowStatus status);
        void UpdateBorrowRecord(BorrowRecord borrowRecord);
        void DeleteBorrowRecord(BorrowRecord borrowRecord);
        void SaveChanges(List<BorrowRecord> borrowRecords);
        int GetBorrowId();
    }
}
