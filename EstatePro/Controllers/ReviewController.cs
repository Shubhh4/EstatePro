using EstatePro.Data;
using EstatePro.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EstatePro.Controllers
{
    public class ReviewController : Controller
    {
        private readonly ApplicationDbContext db;

        public ReviewController(ApplicationDbContext db)
        {
            this.db = db;
        }

        public IActionResult Index()
        {
            var data = db.Reviews
                          .Include(r => r.User)
                          .Include(r => r.Property)
                          .ToList();
            return View(data);
        }

        [HttpPost]
        public IActionResult Index(string txt)
        {
            if (!string.IsNullOrEmpty(txt))
            {
                var data = db.Reviews
                              .Include(r => r.User)
                              .Include(r => r.Property)
                              .Where(r => r.ReviewText.Contains(txt) ||
                                          r.Rating.ToString().Contains(txt) ||
                                          r.User.FirstName.Contains(txt))
                              .ToList();
                return View(data);
            }
            else
            {
                var data = db.Reviews
                              .Include(r => r.User)
                              .Include(r => r.Property)
                              .ToList();
                return View(data);
            }
        }

        public IActionResult AddReview(int id)
        {
            var review = new Review { PropertyId = id };
            return View(review);
        }

        [HttpPost]
        public IActionResult AddReview(Review review)
        {
            if (ModelState.IsValid)
            {
                review.ReviewDate = DateTime.Now;
                db.Reviews.Add(review);
                db.SaveChanges();
                TempData["msg"] = "Review added successfully";
                return RedirectToAction("Index");
            }
            else
            {
                return View(review);
            }
        }

        public IActionResult EditReview(int id)
        {
            var data = db.Reviews
                         .Include(r => r.User)
                         .Include(r => r.Property)
                         .FirstOrDefault(r => r.ReviewId == id);
            return View(data);
        }

        [HttpPost]
        public IActionResult EditReview(Review review)
        {
            review.ReviewDate = DateTime.Now;
            db.Reviews.Update(review);
            db.SaveChanges();
            TempData["upd"] = "Review updated successfully";
            return RedirectToAction("Index");
        }

        public IActionResult DeleteReview(int id)
        {
            var data = db.Reviews.Find(id);
            if (data != null)
            {
                db.Reviews.Remove(data);
                db.SaveChanges();
                TempData["danger"] = "Review deleted successfully";
            }
            return RedirectToAction("Index");
        }

        public IActionResult PropertyReviews(int propertyId)
        {
            var data = db.Reviews
                         .Include(r => r.User)
                         .Include(r => r.Property)
                         .Where(r => r.PropertyId == propertyId)
                         .ToList();
            ViewBag.PropertyId = propertyId;
            return View(data);
        }
    }
}
