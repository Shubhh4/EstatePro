using EstatePro.Models;

namespace EstatePro.Repository
{
    public interface ReviewRepo
    {
        List<Review> FetchReviews();
        void AddReview(Review review);
        Review GetReviewById(int id);
        void EditReview(Review review);
        List<Review> GetReviewsByProperty(int propertyId);
    }
}
