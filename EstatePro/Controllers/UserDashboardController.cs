using EstatePro.Models;
using EstatePro.Repository;
using Microsoft.AspNetCore.Mvc;

namespace EstatePro.Controllers
{
    public class UserDashboardController : Controller
    {
        UserDashboardRepo udrepo;
        public UserDashboardController(UserDashboardRepo udrepo)
        {
            this.udrepo = udrepo;
        }
        public IActionResult Index()
        {
            int userId = (int)HttpContext.Session.GetInt32("UserId");
            var role = udrepo.GetRole(userId).ToString();

            var viewModel = new DashboardView();

            if (role == "Buyer")
            {
                viewModel = new DashboardView
                {
                    Role = role,

                    TotalBookings = udrepo.GetTotalBookings(userId),
                    AcceptedBookings = udrepo.GetAcceptedBookings(userId),
                    PendingBookings = udrepo.GetPendingBookings(userId),
                    RejectedBookings = udrepo.GetRejectedBookings(userId),

                };
            }
            else if (role == "Tenant")
            {
                viewModel = new DashboardView
                {
                    Role = role,

                    TotalBookings = udrepo.GetTotalBookings(userId),
                    AcceptedBookings = udrepo.GetAcceptedBookings(userId),
                    PendingBookings = udrepo.GetPendingBookings(userId),
                    RejectedBookings = udrepo.GetRejectedBookings(userId),

                    TotalLease = udrepo.GetTotalLease(userId),
                    AcceptedLease = udrepo.GetAcceptedLease(userId),
                    RejectedLease = udrepo.GetRejectedLease(userId),
                    PendingLease = udrepo.GetPendingLease(userId),
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
