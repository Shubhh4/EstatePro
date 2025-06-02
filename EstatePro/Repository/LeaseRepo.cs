using EstatePro.Models;

namespace EstatePro.Repository
{
    public interface LeaseRepo
    {
        List<LeaseAgreement> fetchLeaseAgreement();
        List<Booking> GetConfirmedBookings();
        Booking GetBookingById(int id);
        void UpdateBooking(Booking booking);
        void AddLeaseAgreement(LeaseAgreement e);
        LeaseAgreement GetLeaseAgreementById(int id);
        void EditLeaseAgreement(LeaseAgreement e);
        void DeleteLeaseAgreement(int id);
    }
}
