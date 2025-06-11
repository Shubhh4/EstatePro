using EstatePro.Models;

namespace EstatePro.Repository
{
    public interface IBookingRepository
    {
        List<Booking> GetAllBooking();
        Booking GetBookingById(int id);
        void DeleteBooking(int id);
        void MarkBookingAsPaid(int bookingId, string paymentId);
        void UpdateBooking(Booking booking);
        List<Booking> GetBookingsByUserId(int userId);
        void AddBooking(Booking booking);
        void UpdatePropertyStatus(Property property);

    }
}
