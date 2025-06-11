using EstatePro.Models;
using EstatePro.Repository;
using Microsoft.AspNetCore.Mvc;

namespace EstatePro.Controllers
{
    public class ReviewController : Controller
    {
        private readonly ReviewRepo repo;

        public ReviewController(ReviewRepo repo)
        {
            this.repo = repo;
        }

        public IActionResult Index(string txt)
        {
            var list = repo.fetchReviews();

            if (!string.IsNullOrEmpty(txt))
            {
                list = list
                    .Where(r => r.ReviewText.Contains(txt)
                             || r.Rating.ToString().Contains(txt)
                             || r.User.FirstName.Contains(txt))
                    .ToList();
            }

            return View(list);
        }

        public IActionResult AddReview(int propertyId)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
                return RedirectToAction("Login", "LogReg");

            if (!repo.HasUserBookedProperty(userId.Value, propertyId))
            {
                TempData["danger"] = "You must book this property to submit a review.";
                return RedirectToAction("Browse", "Property");
            }

            if (repo.HasUserReviewedProperty(userId.Value, propertyId))
            {
                TempData["danger"] = "You have already reviewed this property.";
                return RedirectToAction("Browse", "Property");
            }

            return View(new Review { PropertyId = propertyId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddReview(Review review)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
                return RedirectToAction("Login", "Account");

            review.UserId = userId.Value;

            if (!repo.HasUserBookedProperty(userId.Value, review.PropertyId))
            {
                TempData["error"] = "You are not authorized to review this property.";
                return RedirectToAction("Index", "Property");
            }

            if (repo.HasUserReviewedProperty(userId.Value, review.PropertyId))
            {
                TempData["error"] = "You have already reviewed this property.";
                return RedirectToAction("Index", "Property");
            }

            if (ModelState.IsValid)
            {
                repo.AddReview(review);
                TempData["msg"] = "Review added successfully";
                return RedirectToAction("Index");
            }

            return View(review);
        }

        public IActionResult EditReview(int id)
        {
            var review = repo.GetReviewById(id);
            if (review == null)
                return NotFound();

            return View(review);
        }

        [HttpPost]
        public IActionResult EditReview(Review review)
        {
            review.ReviewDate = DateTime.Now;
            if (ModelState.IsValid)
            {
                repo.EditReview(review);
                TempData["upd"] = "Review updated successfully";
                return RedirectToAction("Index");
            }

            // Reload data if ModelState is invalid
            var fullReview = repo.GetReviewById(review.ReviewId);
            return View(fullReview);
        }

        public IActionResult DeleteReview(int id)
        {
            repo.DeleteReview(id);
            TempData["danger"] = "Review deleted successfully";
            return RedirectToAction("Index");
        }

        public IActionResult PropertyReviews(int propertyId)
        {
            var list = repo.GetReviewsByProperty(propertyId);
            ViewBag.PropertyId = propertyId;
            return View(list);
        }
    }
}
