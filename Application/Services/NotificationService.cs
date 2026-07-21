using Application.Interfaces;
using Core.Interfaces;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class NotificationService: INotificationService
    {
        private readonly IBorrowService _borrowService;
        private readonly IUserManeger _userManager;
        private readonly IEmailService _emailService;
        private readonly IBookManager _bookManager;
        public NotificationService(IBorrowService borrowService, IUserManeger userManager, IEmailService emailService, IBookManager bookManager)
        {
            this._borrowService = borrowService;
            this._userManager = userManager;
            this._emailService = emailService;
            this._bookManager = bookManager;
        }

        public void SendOneDayLeftNotification()
        {
            List<BorrowRecord> warningRecords = _borrowService.OneDayLeftRecords();
            foreach (BorrowRecord warningRecord in warningRecords)
            {
                var user = _userManager.GetUserById(warningRecord.UserId);
                Client client = user as Client;
                if (client == null)
                    throw new ArgumentException("Only Clients get notifications");
                Book book = _bookManager.GetBookById(warningRecord.BookId);
                string subject = "Book Return Reminder";

                string body = $"Hello {client.UserName}, You have one day left to return the book: {book.Title}";

                _emailService.SendEmail(client.Email, subject, body);

                //სატესტოდ 
                Console.WriteLine($"Notification sent to {client.UserName}");
                Console.WriteLine($"Book: {book.Title}");
                Console.WriteLine(subject);
                Console.WriteLine(body);
            }

        }

        public void SendOverdueNotification()
        {
            List<BorrowRecord> overdueRecords = _borrowService.GetOverdueBorrowRecords();
            foreach (BorrowRecord overdueRecord in overdueRecords)
            {
                var user = _userManager.GetUserById(overdueRecord.UserId);
                Client client = user as Client;
                if (client == null)
                    throw new ArgumentException("Only Clients get notifications");
                Book book = _bookManager.GetBookById(overdueRecord.BookId);
                string subject = "Overdue Book Notifiation";

                string body = $"Hello {client.UserName}, the return deadline for the book {book.Title} has passed." +
                    $"return as fast as possible!";

                _emailService.SendEmail(client.Email, subject, body);

                //სატესტოდ 
                Console.WriteLine($"Notification sent to {client.UserName}");
                Console.WriteLine($"Book: {book.Title}");
                Console.WriteLine(subject);
                Console.WriteLine(body);
            }
        }
    }
}
