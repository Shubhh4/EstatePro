using EstatePro.Models;

namespace EstatePro.Repository
{
    public interface ReviewRepo
    {
        List<Review> fetchReviews();
        void AddReview(Review review);
        Review GetReviewById(int id);
        void EditReview(Review review);
        void DeleteReview(int id);
        List<Review> GetReviewsByProperty(int propertyId);
        bool HasUserBookedProperty(int userId, int propertyId);
        bool HasUserReviewedProperty(int userId, int propertyId);
    }
}
