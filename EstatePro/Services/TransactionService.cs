using EstatePro.Data;
using EstatePro.Models;
using EstatePro.Repository;
using Microsoft.EntityFrameworkCore;

namespace EstatePro.Services
{
    public class TransactionService : ITransactionRepository
    {
        ApplicationDbContext db;
        public TransactionService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public List<Transaction> GetAllTransactions()
        {
            return db.Transactions
             .Include(t => t.Booking)
             .ThenInclude(b => b.Property)
             .Include(bu => bu.Booking.User)
             .OrderByDescending(t => t.TransactionDate)
             .ToList();
            //return db.Transactions.Include(b => b.Booking).ToList();
        }

        public Transaction GetTransactionById(int id)
        {
            return db.Transactions
            .Include(t => t.Booking)
            .ThenInclude(b => b.Property)
            .Include(t => t.Booking.User)
            .FirstOrDefault(t => t.TransactionId == id);
        }
    }
}

