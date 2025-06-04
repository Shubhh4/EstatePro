using EstatePro.Models;

namespace EstatePro.Repository
{
    public interface IBookingRepository
    {
        List<Booking> GetAllBooking();
        Booking GetBookingById(int id);
        void DeleteBooking(int id);
        void MarkBookingAsPaid(int bookingId, string paymentId);
    }
}
