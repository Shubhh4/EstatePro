using EstatePro.Data;
using EstatePro.Models;
using EstatePro.Repository;
using Microsoft.EntityFrameworkCore;

namespace EstatePro.Services
{
    public class DashboardService : DashboardRepo
    {
        ApplicationDbContext db;

        public DashboardService(ApplicationDbContext db)
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
        public int GetTotalUsers()
        { 
            int data=db.Users.Count(); 
            return data;
        }
        public int GetTotalAgents() 
        {
            int data = db.Users.Count(u => u.Role == UserRole.Agent);
            return data;
        }
        public int GetTotalSellers() 
        { 
            int data = db.Users.Count(u => u.Role == UserRole.Seller);
            return data;
        }
        public int GetTotalBuyers() 
        { 
            int data = db.Users.Count(u => u.Role == UserRole.Buyer);
            return data;
        }
        public int GetTotalTenants() 
        { 
            int data = db.Users.Count(u => u.Role == UserRole.Tenant); 
            return data;
        }

        public int GetTotalProperties() 
        {
            int data = db.Properties.Count(); 
            return data;
        }
        public int GetAvailableProperties() 
        { 
            int data = db.Properties.Count(p => p.Status == PropertyStatus.Available);
            return data;
        }
        public int GetSoldProperties() 
        { 
            int data = db.Properties.Count(p => p.Status == PropertyStatus.Sold); 
            return data;
        }
        public int GetRentedProperties() 
        { 
            int data = db.Properties.Count(p => p.Status == PropertyStatus.Rented); 
            return data;
        }

        public decimal GetTotalTransactionAmount() 
        { 
            decimal data=db.Transactions.Sum(t => (decimal)t.Amount);
            return data;
        }

        public int GetTotalLease()
        {
            int data = db.LeaseAgreements.Count();
            return data;
        }
        public int GetAcceptedLease()
        {
            int data = db.LeaseAgreements.Count(l => l.LeaseStatus == LeaseStatus.Accepted);
            return data;
        }
        public int GetPendingLease()
        {
            int data = db.LeaseAgreements.Count(l => l.LeaseStatus == LeaseStatus.Pending);
            return data;
        }
        public int GetRejectedLease()
        {
            int data = db.LeaseAgreements.Count(l => l.LeaseStatus == LeaseStatus.Rejected);
            return data;
        }
        public decimal GetTotalRentAmount()
        {
            decimal data = db.Rents.Sum(r => (decimal)r.Amount);
            return data;
        }

        public int GetTotalProperties(int userId)
        {
            int data= db.Properties.Count(p => p.OwnerId == userId);
            return data;
        }

        public int GetAvailableProperties(int userId)
        {
            int data= db.Properties.Count(p => p.OwnerId == userId && p.Status == PropertyStatus.Available);
            return data;
        }

        public int GetSoldProperties(int userId)
        {
            int data= db.Properties.Count(p => p.OwnerId == userId && p.Status == PropertyStatus.Sold);
            return data;
        }

        public int GetRentedProperties(int userId)
        {
            int data= db.Properties.Count(p => p.OwnerId == userId && p.Status == PropertyStatus.Rented);
            return data;
        }

        public int GetTotalLease(int userId)
        {
            int data = db.LeaseAgreements.Count(l => l.Property.OwnerId == userId);
            return data;
        }

        public int GetAcceptedLease(int userId)
        {
            int data= db.LeaseAgreements.Count(l => l.Property.OwnerId == userId && l.LeaseStatus == LeaseStatus.Accepted);
            return data;
        }

        public int GetPendingLease(int userId)
        {
            int data= db.LeaseAgreements.Count(l => l.Property.OwnerId == userId && l.LeaseStatus == LeaseStatus.Pending);
            return data;
        }

        public int GetRejectedLease(int userId)
        {
            int data= db.LeaseAgreements.Count(l => l.Property.OwnerId == userId && l.LeaseStatus == LeaseStatus.Rejected);
            return data;
        }

    }
}
