using EstatePro.Data;
using EstatePro.Models;
using EstatePro.Repository;
using Microsoft.EntityFrameworkCore;

namespace EstatePro.Services
{
    public class ReviewService : ReviewRepo
    {
        ApplicationDbContext db;

        public ReviewService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public List<Review> FetchReviews()
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
            db.Reviews.Update(review);
            db.SaveChanges();
        }

        public List<Review> GetReviewsByProperty(int propertyId)
        {
            return db.Reviews
                     .Include(r => r.User)
                     .Include(r => r.Property)
                     .Where(r => r.PropertyId == propertyId)
                     .ToList();
        }
    }
}
