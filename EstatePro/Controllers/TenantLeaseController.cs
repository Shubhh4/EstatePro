using System.Security.Claims;
using EstatePro.Models;
using EstatePro.Repository;
using EstatePro.Services;
using Microsoft.AspNetCore.Mvc;

namespace EstatePro.Controllers
{
    public class TenantLeaseController : Controller
    {
        LeaseTenantRepo ltrepo;
        public TenantLeaseController(LeaseTenantRepo ltrepo)
        {
            this.ltrepo = ltrepo;
        }
        public IActionResult Index()
        {
            int tenantId = (int)HttpContext.Session.GetInt32("UserId");
            var leases = ltrepo.GetLeasesByTenantId(tenantId);
            return View(leases);
        }

        public IActionResult LeaseDetails(int id)
        {
            var lease = ltrepo.GetLeaseById(id);
            return View(lease);
        }

        [HttpPost]
        public IActionResult AcceptAgreement(int id)
        {
            ltrepo.AcceptLeaseAgreement(id);
            TempData["msg"] = "Lease Accepted!";
            return RedirectToAction("LeaseDetails", new { id });
        }

        [HttpPost]
        public IActionResult RejectAgreement(int id)
        {
            ltrepo.RejectLeaseAgreement(id);
            TempData["danger"] = "Lease Rejected!";
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult PayDeposit(int id)
        {
            var lease = ltrepo.GetLeaseById(id);
            if (lease == null || lease.IsDepositPaid)
            {
                return RedirectToAction("LeaseDetails", new { id });
            }
            ViewBag.KeyId = "rzp_test_Kl7588Yie2yJTV";
            ViewBag.Amount = lease.SecurityDeposit * 100;
            ViewBag.LeaseId = lease.LeaseId;

            return View("PayDeposit");
        }

        [HttpPost]
        public IActionResult ConfirmDeposit(int leaseId)
        {
            ltrepo.PaySecurityDeposit(leaseId);

            var data=ltrepo.GetLeaseById(leaseId);

            var property = ltrepo.GetPropertyById(data.PropertyId);
            property.Status = PropertyStatus.Rented;
            ltrepo.UpdateProperty(property);

            TempData["msg"] = "Security deposit paid!";
            return RedirectToAction("LeaseDetails", new { id = leaseId });
        }

        public IActionResult RentList(int leaseId)
        {
            var rents = ltrepo.GetRentByLeaseId(leaseId);
            ViewBag.LeaseId = leaseId;
            return View(rents);
        }

        public IActionResult PayRent(int id)
        {
            var rent = ltrepo.GetRentById(id);
            if (rent == null || rent.IsPaid)
                return NotFound();

            ViewBag.KeyId = "rzp_test_Kl7588Yie2yJTV";
            ViewBag.RentId = rent.RentId;
            ViewBag.Amount = rent.Amount * 100;

            return View("PayRent");
        }

        [HttpPost]
        public IActionResult ConfirmRent(int rentId)
        {
            ltrepo.PayRent(rentId);
            TempData["msg"] = "Rent paid!";
            var rent = ltrepo.GetRentById(rentId);
            return RedirectToAction("RentList", new { leaseId = rent.LeaseId });
        }
    }
}
