using EstatePro.Models;
using EstatePro.Repository;
using Microsoft.AspNetCore.Mvc;

namespace EstatePro.Controllers
{
    public class DashboardController : Controller
    {
        DashboardRepo drepo;
        public DashboardController(DashboardRepo drepo) 
        { 
            this.drepo = drepo;
        }
        public IActionResult Index()
        {
            int userId = (int)HttpContext.Session.GetInt32("UserId");
            var role = drepo.GetRole(userId).ToString();

            var viewModel = new DashboardView();

            if (role == "Admin")
            {
                viewModel = new DashboardView
                {
                    Role=role,
                    TotalUsers = drepo.GetTotalUsers(),
                    TotalAgents = drepo.GetTotalAgents(),
                    TotalSellers = drepo.GetTotalSellers(),
                    TotalBuyers = drepo.GetTotalBuyers(),
                    TotalTenants = drepo.GetTotalTenants(),

                    TotalProperties = drepo.GetTotalProperties(),
                    AvailableProperties = drepo.GetAvailableProperties(),
                    SoldProperties = drepo.GetSoldProperties(),
                    RentedProperties = drepo.GetRentedProperties(),
                    TotalTransactionAmount = drepo.GetTotalTransactionAmount(),

                    TotalLease = drepo.GetTotalLease(),
                    AcceptedLease = drepo.GetAcceptedLease(),
                    RejectedLease = drepo.GetRejectedLease(),
                    PendingLease = drepo.GetPendingLease(),
                    TotalRentAmount = drepo.GetTotalRentAmount(),
                };
            }
            else if (role == "Agent" || role == "Seller")
            {
                viewModel = new DashboardView
                {
                    Role = role,
                    TotalProperties = drepo.GetTotalProperties(userId),
                    AvailableProperties = drepo.GetAvailableProperties(userId),
                    SoldProperties = drepo.GetSoldProperties(userId),
                    RentedProperties = drepo.GetRentedProperties(userId),

                    TotalLease = drepo.GetTotalLease(userId),
                    AcceptedLease = drepo.GetAcceptedLease(userId),
                    RejectedLease = drepo.GetRejectedLease(userId),
                    PendingLease = drepo.GetPendingLease(userId),
                };
            }
            else
            {
                return Unauthorized();
            }

            return View(viewModel);
        }
    }
}
