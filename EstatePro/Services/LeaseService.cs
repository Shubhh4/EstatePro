using EstatePro.Data;
using EstatePro.Models;
using EstatePro.Repository;
using Microsoft.EntityFrameworkCore;

namespace EstatePro.Services
{
    public class LeaseService : LeaseRepo
    {
        ApplicationDbContext db;
        public LeaseService(ApplicationDbContext db)
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

        public List<LeaseAgreement> GetLeasesByOwnerId(int id)
        {
            return db.LeaseAgreements
                .Include(l => l.Property)
                .Include(l => l.Tenant)
                .Where(l => l.Property.OwnerId == id)
                .ToList();
        }
        public List<LeaseAgreement> fetchLeaseAgreement()
        {
            var data = db.LeaseAgreements.Include(x => x.Property).Include(x => x.Tenant).ToList();
            return data;
        }

        public List<Booking> GetConfirmedBookings()
        {
            return db.Bookings
                .Include(b => b.Property)
                .Include(b => b.User)
                .Where(b => b.Status == BookingStatus.Confirmed.ToString() && b.Property.ListingType==ListingType.Rent)
                .ToList();
        }

        public List<Booking> GetConfirmedBookings(int id, string role)
        {
            var query = db.Bookings
                .Include(b => b.Property)
                .Include(b => b.User)
                .Where(b => b.Status == BookingStatus.Confirmed.ToString() && b.Property.ListingType == ListingType.Rent);

            if (role == "Agent" || role == "Seller")
            {
                query = query.Where(b => b.Property.OwnerId == id); // Only properties listed by the logged-in agent/seller
            }

            return query.ToList();
        }

        public Booking GetBookingById(int id)
        {
            var data = db.Bookings
                .Include(b => b.Property)
                .Include(b => b.User)
                .FirstOrDefault(b => b.BookingId == id);
            return data;
        }


        public void UpdateBooking(Booking booking)
        {
            db.Bookings.Update(booking);
            db.SaveChanges();
        }
        public void AddLeaseAgreement(LeaseAgreement e)
        {
            db.LeaseAgreements.Add(e);
            db.SaveChanges();
        }

        public LeaseAgreement GetLeaseAgreementById(int id)
        {
            var data = db.LeaseAgreements.Find(id);
            return data;
        }
        public void EditLeaseAgreement(LeaseAgreement e)
        {
            var existing = db.LeaseAgreements.FirstOrDefault(x => x.LeaseId == e.LeaseId);

            if (existing != null)
            {
                existing.LeaseStartDate = e.LeaseStartDate;
                existing.LeaseEndDate = e.LeaseEndDate;
                existing.RentAmount = e.RentAmount;
                existing.SecurityDeposit = e.SecurityDeposit;

                db.SaveChanges();
            }
        }


        public void DeleteLeaseAgreement(int id)
        {
            var data = db.LeaseAgreements.Find(id);
            db.LeaseAgreements.Remove(data);
            db.SaveChanges();
        }

        public List<Rent> GetRentByLeaseId(int id)
        {
            var data = db.Rents.Where(r => r.LeaseId == id).OrderBy(r => r.DueDate).ToList();
            return data;
        }
    }
}
