using EstatePro.Data;
using EstatePro.Models;
using Microsoft.AspNetCore.Mvc;

namespace EstatePro.Controllers
{
    public class ReportController : Controller
    {
        private readonly ApplicationDbContext db;

        public ReportController(ApplicationDbContext db)
        {
            this.db = db;
        }

        public IActionResult Index()
        {
            var data = db.Reports.ToList();
            return View(data);
        }

        [HttpPost]
        public IActionResult Index(string txt)
        {
            if (!string.IsNullOrEmpty(txt))
            {
                var data = db.Reports
                              .Where(r => r.ReportType.ToString()!.Contains(txt) ||
                                          (r.ReportData != null && r.ReportData.Contains(txt)))
                              .ToList();
                return View(data);
            }
            else
            {
                var data = db.Reports.ToList();
                return View(data);
            }
        }

        public IActionResult AddReport()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddReport(Report report)
        {
            if (ModelState.IsValid)
            {
                report.CreatedAt = DateTime.UtcNow;
                db.Reports.Add(report);
                db.SaveChanges();
                TempData["msg"] = "Report added successfully";
                return RedirectToAction("Index");
            }
            else
            {
                return View(report);
            }
        }

        public IActionResult EditReport(int id)
        {
            var data = db.Reports.FirstOrDefault(r => r.ReportId == id);
            return View(data);
        }

        [HttpPost]
        public IActionResult EditReport(Report report)
        {
            report.CreatedAt = DateTime.UtcNow;
            db.Reports.Update(report);
            db.SaveChanges();
            TempData["upd"] = "Report updated successfully";
            return RedirectToAction("Index");
        }

        public IActionResult DeleteReport(int id)
        {
            var data = db.Reports.Find(id);
            if (data != null)
            {
                db.Reports.Remove(data);
                db.SaveChanges();
                TempData["danger"] = "Report deleted successfully";
            }
            return RedirectToAction("Index");
        }
    }
}
