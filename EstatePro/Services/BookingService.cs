using EstatePro.Data;
using EstatePro.Models;
using EstatePro.Repository;
using Microsoft.EntityFrameworkCore;

namespace EstatePro.Services
{
    public class BookingService : IBookingRepository
    {
        ApplicationDbContext db;
        public BookingService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public List<Booking> GetAllBooking()
        {
            return db.Bookings.Include(u => u.User).Include(p => p.Property).ToList();
        }

        public Booking GetBookingById(int id)
        {
            return db.Bookings.Include(b => b.User).Include(b => b.Property).FirstOrDefault(b => b.BookingId == id);
        }

        public void DeleteBooking(int id)
        {
            var del_id = db.Bookings.Find(id);
            if (del_id != null)
            {
                db.Bookings.Remove(del_id);
                db.SaveChanges();
            }
        }

        public void MarkBookingAsPaid(int bookingId, string paymentId)
        {
            var booking = db.Bookings.Include(b => b.Property).FirstOrDefault(b => b.BookingId == bookingId);
            //var booking = db.Bookings.FirstOrDefault(b => b.BookingId == bookingId);

            if (booking == null)
            {
                throw new Exception("Booking not found.");
            }

            booking.IsPaid = true;

            db.Transactions.Add(new Transaction
            {
                BookingId = bookingId,
                Amount = (decimal)booking.Property.Price,
                PaymentMethod = "Razorpay",
                PaymentStatus = PaymentStatus.Completed.ToString(),
                TransactionDate = DateTime.Now
            });

            db.SaveChanges();
        }

    }
}
