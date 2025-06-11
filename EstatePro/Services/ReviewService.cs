using EstatePro.Data;
using EstatePro.Models;
using EstatePro.Repository;
using Microsoft.EntityFrameworkCore;

namespace EstatePro.Services
{
    public class ReviewService : ReviewRepo
    {
        private readonly ApplicationDbContext db;

        public ReviewService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public List<Review> fetchReviews()
        {
            return db.Reviews
                     .Include(r => r.User)
                     .Include(r => r.Property)
                     .ToList();
        }

        public void AddReview(Review review)
        {
            review.ReviewDate = DateTime.Now;
            db.Reviews.Add(review);
            db.SaveChanges();
        }

        public Review GetReviewById(int id)
        {
            return db.Reviews
                     .Include(r => r.User)
                     .Include(r => r.Property)
                     .FirstOrDefault(r => r.ReviewId == id);
        }

        public void EditReview(Review review)
        {
            // Optional: attach and mark entity as modified
            db.Reviews.Update(review);
            db.SaveChanges();
        }

        public void DeleteReview(int id)
        {
            var review = db.Reviews.Find(id);
            if (review != null)
            {
                db.Reviews.Remove(review);
                db.SaveChanges();
            }
        }

        public List<Review> GetReviewsByProperty(int propertyId)
        {
            return db.Reviews
                     .Include(r => r.User)
                     .Include(r => r.Property)
                     .Where(r => r.PropertyId == propertyId)
                     .ToList();
        }

        public bool HasUserBookedProperty(int userId, int propertyId)
        {
            return db.Bookings.Any(b => b.UserId == userId && b.PropertyId == propertyId && b.Status == "Confirmed");
        }

        public bool HasUserReviewedProperty(int userId, int propertyId)
        {
            return db.Reviews.Any(r => r.UserId == userId && r.PropertyId == propertyId);
        }
    }
}
