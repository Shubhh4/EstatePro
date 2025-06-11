namespace EstatePro.Models
{
    public class DashboardView
    {
        public string? Role { get; set; }
        public int? TotalUsers { get; set; }
        public int? TotalAgents { get; set; }
        public int? TotalSellers { get; set; }
        public int? TotalBuyers { get; set; }
        public int? TotalTenants { get; set; }

        public int? TotalProperties { get; set; }
        public int? AvailableProperties { get; set; }
        public int? SoldProperties { get; set; }
        public int? RentedProperties { get; set; }
        public decimal? TotalTransactionAmount { get; set; }

        public int? TotalLease { get; set; }
        public int? AcceptedLease { get; set; }
        public int? PendingLease { get; set; }
        public int? RejectedLease { get; set; }
        public decimal? TotalRentAmount { get; set; }

        public int? TotalBookings { get; set; }
        public int? AcceptedBookings { get; set; }
        public int? PendingBookings { get; set; }
        public int? RejectedBookings { get; set; }

    }
}
