using EstatePro.Data;
using EstatePro.Models;
using EstatePro.Repository;

namespace EstatePro.Services
{
    public class UserDashboardService : UserDashboardRepo
    {
        ApplicationDbContext db;

        public UserDashboardService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public UserRole GetRole(int id)
        {
            var role = db.Users
                         .Where(x => x.UserId == id)
                         .Select(x => x.Role)
                         .FirstOrDefault();

            return role;
        }

        public int GetTotalBookings(int userId)
        {
            int data = db.Bookings.Count(b => b.UserId == userId);
            return data;
        }

        public int GetAcceptedBookings(int userId)
        {
            int data = db.Bookings.Count(b => b.UserId == userId && (b.Status == BookingStatus.AcceptedByAdmin.ToString() || b.Status == BookingStatus.LeaseCreated.ToString())); 
            return data;
        }

        public int GetPendingBookings(int userId)
        {
            int data = db.Bookings.Count(b => b.UserId == userId && b.Status == BookingStatus.Pending.ToString());
            return data;
        }

        public int GetRejectedBookings(int userId)
        {
            int data = db.Bookings.Count(b => b.UserId == userId && b.Status == BookingStatus.Cancelled.ToString());
            return data;
        }

        public int GetTotalLease(int userId)
        {
            int data = db.LeaseAgreements.Count(l => l.TenantId == userId);
            return data;
        }

        public int GetAcceptedLease(int userId)
        {
            int data = db.LeaseAgreements.Count(l => l.TenantId == userId && l.LeaseStatus == LeaseStatus.Accepted);
            return data;
        }

        public int GetPendingLease(int userId)
        {
            int data = db.LeaseAgreements.Count(l => l.TenantId == userId && l.LeaseStatus == LeaseStatus.Pending);
            return data;
        }

        public int GetRejectedLease(int userId)
        {
            int data = db.LeaseAgreements.Count(l => l.TenantId == userId && l.LeaseStatus == LeaseStatus.Rejected);
            return data;
        }
    }
}
