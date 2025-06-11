using EstatePro.Models;

namespace EstatePro.Repository
{
    public interface UserDashboardRepo
    {
        //Total Bookings,Accepted and Rejected
        //Total Lease, Accepted and Rejected
        UserRole GetRole(int id);
        int GetTotalBookings(int userId);
        int GetAcceptedBookings(int userId);
        int GetPendingBookings(int userId);
        int GetRejectedBookings(int userId);

        int GetTotalLease(int userId);
        int GetAcceptedLease(int userId);
        int GetPendingLease(int userId);
        int GetRejectedLease(int userId);
    }
}
