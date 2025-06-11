namespace EstatePro.Models
{
    public enum PropertyType
    {
        Apartment,
        House,
        Land,
        Office,
        Villa
    }

    public enum ListingType
    {
        Sale,
        Rent
    }

    public enum PropertyStatus
    {
        Available,
        Sold,
        Rented
    }

    public enum OwnerRole
    {
        Admin,
        Agent,
        Seller
    }

    public enum BookingStatus
    {
        Confirmed,
        Pending,           // Booking made
        ConfirmedByTenant, // Tenant confirmed interest
        AcceptedByAdmin,   // Admin/Agent/Seller accepted
        LeaseCreated,      // Lease has been created
        Cancelled          // Rejected or canceled
    }

    public enum PaymentMethod
    {
        CreditCard,
        BankTransfer,
        Cash,
        UPI
    }

    public enum PaymentStatus
    {
        Pending,
        Completed,
        Failed
    }

    public enum AppointmentStatus
    {
        Pending,
        Confirmed,
        Cancelled
    }

    public enum ReportType
    {
        Sales,
        MarketTrend,
        Revenue
    }

    public enum RentStatus
    {
        Pending,
        Paid,
        Overdue
    }

    public enum UserRole
    {
        Admin,
        Agent,
        Buyer,
        Seller,
        Tenant
    }

    public enum LeaseStatus
    {
        Pending,
        Accepted,
        Rejected
    }

    public enum TransactionType
    {
        Deposit,
        Rent,
        Other
    }
}
