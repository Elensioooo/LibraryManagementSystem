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
        //კლიენტმა უნდა მოითხოვოს წიგნი(რექვესთი გამოგზავნოს)
        //ადმინმა უნდა დააექსეფთოს ან  დაარიჯექთოს რექვესთი
        //კლიენტი აბრუნებს წიგნს
        //ადმინს აქ ნახულობს აბსოლუტურად ყველა ტიპს ვინც ლოდინის რეჟმშია
        void RequestBorrow(int userId, int bookId);
        void AcceptBorrowRequest(int borrowRecordId);
        void RejectBorrowRequest(int borrowRecordId);
        void ReturnBook(int borrowRecordId);

        //ვაფშე ყველა იუზერის borrowreqcord
        List<BorrowRecord> GetAllBorrowRecords();
        //ერთი კონკრეტული იუზერს borrowRecor history
        List<BorrowRecord> GetBorrowRecordsByUserId(int userId);

        //კონკრეტული წიგნისთვის borrowRecord history
        List<BorrowRecord> GetBorrowRecordsByBookId(int bookId);
        
        //იმ იუზერების სია ვინც ელოდება რო მიიღოს პასუხი ადმინისგან
        List<BorrowRecord> GetPendingBorrowRequests();

        //დაექსეფთებულები
        List<BorrowRecord> GetAcceptedBorrowRecords();

        //დარეჯექთებულები
        List<BorrowRecord> GetRejectedBorrowRecords();

        //ადმინმა რო ნახოს რმადენი წიგნი დაბრუნდა(სიტყვაზე ამ თვეში)
        List<BorrowRecord> GetReturnedBorrowRecords();


        //overdue and fines
        //ვაფშე ყველა overdue reocrd
        void SetOverdueStatus();
        List<BorrowRecord> GetOverdueBorrowRecords();
        decimal CalculateFine(int borrowRecordId);
        void ApplyFine(int borrowRecordId);
    }
}
