using EstatePro.Models;

namespace EstatePro.Repository
{
    public interface DashboardRepo
    {
        UserRole GetRole(int id);
        int GetTotalUsers();
        int GetTotalAgents();
        int GetTotalSellers();
        int GetTotalBuyers();
        int GetTotalTenants();

        int GetTotalProperties();
        int GetAvailableProperties();
        int GetSoldProperties();
        int GetRentedProperties();
        decimal GetTotalTransactionAmount();

        int GetTotalLease();
        int GetAcceptedLease();
        int GetPendingLease();
        int GetRejectedLease();
        decimal GetTotalRentAmount();

        int GetTotalProperties(int userId);
        int GetAvailableProperties(int userId);
        int GetSoldProperties(int userId);
        int GetRentedProperties(int userId);

        int GetTotalLease(int userId);
        int GetAcceptedLease(int userId);
        int GetPendingLease(int userId);
        int GetRejectedLease(int userId);

    }
}
