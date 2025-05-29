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
        Pending,
        Confirmed,
        Cancelled
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
}
