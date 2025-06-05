using EstatePro.Models;

namespace EstatePro.Repository
{
    public interface LeaseRepo
    {
        UserRole GetRole(int id);
        List<LeaseAgreement> fetchLeaseAgreement();
        List<LeaseAgreement> GetLeasesByOwnerId(int id);
        List<Booking> GetConfirmedBookings(int id, string role);

        Booking GetBookingById(int id);
        void UpdateBooking(Booking booking);
        void AddLeaseAgreement(LeaseAgreement e);
        LeaseAgreement GetLeaseAgreementById(int id);
        void EditLeaseAgreement(LeaseAgreement e);
        void DeleteLeaseAgreement(int id);
        List<Rent> GetRentByLeaseId(int id);
    }
}
