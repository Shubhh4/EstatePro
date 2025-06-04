using EstatePro.Models;
using EstatePro.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EstatePro.Controllers
{
    public class LeaseController : Controller
    {
        LeaseRepo lrepo;
        public LeaseController(LeaseRepo lrepo)
        {
            this.lrepo = lrepo;
        }
        public IActionResult Index()
        {
            int userId = 1;
            string role = lrepo.GetRole(userId).ToString();

            List<LeaseAgreement> leases;

            if (role == "Admin")
            {
                leases = lrepo.fetchLeaseAgreement();
            }
            else if (role == "Agent" || role == "Seller")
            {
                leases = lrepo.GetLeasesByOwnerId(userId);
            }
            else
            {
                return Unauthorized();
            }

            return View(leases);
        }

        public IActionResult AddLeaseAgreement()
        {
            int userId = 1;
            string role = lrepo.GetRole(userId).ToString();

            var bookings = lrepo.GetConfirmedBookings(userId, role);
            ViewBag.Bookings = bookings.Select(b => new SelectListItem
            {
                Value = b.BookingId.ToString(),
                Text = $"{b.Property.Title} booked by {b.User.FirstName} {b.User.LastName}"
            }).ToList();

            return View();
        }

        [HttpPost]
        public IActionResult AddLeaseAgreement(LeaseAgreement e)
        {
            var booking = lrepo.GetBookingById(e.BookingId);
            e.PropertyId = booking.PropertyId;
            e.TenantId = booking.UserId;
            e.LeaseStatus = LeaseStatus.Pending;

            lrepo.AddLeaseAgreement(e);

            booking.Status = BookingStatus.LeaseCreated.ToString();
            lrepo.UpdateBooking(booking);

            TempData["msg"] = "Lease Agreement created and sent to tenant for approval.";
            return RedirectToAction("Index");
        }

        public IActionResult EditLeaseAgreement(int id)
        {
            var data = lrepo.GetLeaseAgreementById(id);

            var booking = lrepo.GetBookingById(data.BookingId);

            ViewBag.PropertyTitle = booking.Property.Title;
            ViewBag.TenantName = booking.User.FirstName + " " + booking.User.LastName;


            return View(data);
        }

        [HttpPost]
        public IActionResult EditLeaseAgreement(LeaseAgreement e)
        {
            var existingLease = lrepo.GetLeaseAgreementById(e.LeaseId);
            if (existingLease == null)
            {
                TempData["upd"] = "LeaseAgreement not found!";
                return RedirectToAction("Index");
            }

            e.PropertyId = existingLease.PropertyId;
            e.TenantId = existingLease.TenantId;

            lrepo.EditLeaseAgreement(e);

            TempData["upd"] = "LeaseAgreement Updated!!!";
            return RedirectToAction("Index");
        }

        public IActionResult DeleteLeaseAgreement(int id)
        {
            lrepo.DeleteLeaseAgreement(id);
            TempData["danger"] = "LeaseAgreement Deleted!!!";
            return RedirectToAction("Index");
        }
    }
}
