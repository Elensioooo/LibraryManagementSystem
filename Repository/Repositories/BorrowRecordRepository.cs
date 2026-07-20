using Core.Enums;
using Core.Interfaces;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;

namespace Repository.Repositories
{
    public class BorrowRecordRepository : IBorrowManager
    {
        private readonly string _filePath = "C:\\Users\\User\\LibraryManagementSystem\\Repository\\DataFiles\\BorrowRecord.txt";


        public void AddBorrowRecord(BorrowRecord borrowRecord)
        {
            string line = JsonSerializer.Serialize(borrowRecord);
            File.AppendAllLines(_filePath, new[] { line });
        }

        public void DeleteBorrowRecord(BorrowRecord borrowRecord)
        {
            if (borrowRecord == null)
                throw new ArgumentNullException(nameof(borrowRecord));

            List<BorrowRecord> borrowRecords = GetAllBorrowRecords();
            BorrowRecord foundBorrowRecord = GetBorrowRecordById(borrowRecord.BorrowId);
            if (foundBorrowRecord == null)
                throw new ArgumentException("There is no borrow record like this");
            borrowRecords.Remove(foundBorrowRecord);
            SaveChanges(borrowRecords);
        }

        public List<BorrowRecord> GetAllBorrowRecords()
        {
            if (!File.Exists(_filePath))
                return new List<BorrowRecord>();

            string[] lines = File.ReadAllLines(_filePath);
            List<BorrowRecord> borrowRecords = new List<BorrowRecord>();
            foreach (var line in lines)
            {
                if(string.IsNullOrWhiteSpace(line))
                    continue;

                BorrowRecord borrowRecord = JsonSerializer.Deserialize<BorrowRecord>(line);
                borrowRecords.Add(borrowRecord);
            }   
            return borrowRecords;
        }

        public BorrowRecord GetBorrowRecordById(int id)
        {
           if(id <= 0)
                throw new ArgumentException("id cannot be negative or 0");
            List<BorrowRecord> borrowRecords = GetAllBorrowRecords();
            BorrowRecord foundBorrowRecord = borrowRecords.FirstOrDefault(borrowRecord => borrowRecord.BorrowId == id);
            if (foundBorrowRecord == null)
                throw new ArgumentException("There is no borrow record with this id");
            return foundBorrowRecord;
        }

        public List<BorrowRecord> GetBorrowRecordsByBookId(int bookId)
        {
            if (bookId <= 0)
                throw new ArgumentException("id cannot be negative or 0");
            List<BorrowRecord> borrowRecords = GetAllBorrowRecords();
            List<BorrowRecord> foundBorrowRecords = borrowRecords.Where(borrowRecord => borrowRecord.BookId == bookId).ToList();
            if (foundBorrowRecords.Count == 0)
                throw new ArgumentException("There is no borrow record with this book ID");
            return foundBorrowRecords;
        }

        public List<BorrowRecord> GetBorrowRecordsByStatus(BorrowStatus status)
        {
            List<BorrowRecord> borrowRecords = GetAllBorrowRecords();
            List<BorrowRecord> foundBorrowRecords = borrowRecords.Where(borrowRecord => borrowRecord.Status == status).ToList();
            if(foundBorrowRecords.Count == 0)
                throw new ArgumentException("There is no borrow record with this status");
            return foundBorrowRecords;
        }

        public List<BorrowRecord> GetBorrowRecordsByUserId(int userId)
        {
            if (userId <= 0)
                throw new ArgumentException("id cannot be negative or 0");
            
            List<BorrowRecord> borrowRecords = GetAllBorrowRecords();
            List<BorrowRecord> foundBorrowRecords = borrowRecords.Where(borrowRecord => borrowRecord.UserId == userId).ToList();
            return foundBorrowRecords;
        }

        public void SaveChanges(List<BorrowRecord> borrowRecords)
        {
            File.WriteAllLines(_filePath, borrowRecords.Select(borrowRecord => JsonSerializer.Serialize(borrowRecord)));
        }

        public void UpdateBorrowRecord(BorrowRecord borrowRecord)
        {
            List<BorrowRecord> borrowRecords = GetAllBorrowRecords();
            int index = borrowRecords.FindIndex(b => b.BorrowId == borrowRecord.BorrowId);
            if (index != -1)
            {
                borrowRecords[index] = borrowRecord;
            }
            SaveChanges(borrowRecords);
        }

        public int GetBorrowId()
        {
            List<BorrowRecord> borrowRecords = GetAllBorrowRecords();
            int highestId = 0;
            foreach (BorrowRecord borrowRecord in borrowRecords)
            {
                if (borrowRecord.BorrowId > highestId)
                    highestId = borrowRecord.BorrowId;
            }
            return highestId + 1;
        }
    }
}
