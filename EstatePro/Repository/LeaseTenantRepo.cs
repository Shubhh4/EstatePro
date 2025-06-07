using EstatePro.Models;

namespace EstatePro.Repository
{
    public interface LeaseTenantRepo
    {
        List<LeaseAgreement> GetLeasesByTenantId(int id);
        LeaseAgreement GetLeaseById(int id);
        void AcceptLeaseAgreement(int id);
        void RejectLeaseAgreement(int id);
        void PaySecurityDeposit(int id);
        Property GetPropertyById(int id);
        void UpdateProperty(Property property);
        List<Rent> GetRentByLeaseId(int id);
        Rent GetRentById(int id);
        void PayRent(int id);
    }
}
