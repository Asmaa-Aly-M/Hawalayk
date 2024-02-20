using Hawalayk_APP.Models;

namespace Hawalayk_APP.Services
{
    public interface IReviewRepository
    {
        int Create(Review newReview);
        int Delete(int id);
        List<Review> GetAll();
        Review GetById(int id);
        int Update(int id, Review newReview);
    }
}