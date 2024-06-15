using Hawalayk_APP.DataTransferObject;
using Hawalayk_APP.Models;

namespace Hawalayk_APP.Services
{
    public interface IReviewRepository
    {
        Task<int> Create(string craftsmanID, string customerID, ReviewDTO newReview);
        Task<int> Delete(int id);
        Task<List<Review>> GetAll();
        Task<Review> GetById(int id);
        Task<int> Update(int id, Review newReview);
        Task<int> like(int reviewID);
        Task<int> removeLike(int reviewID);
        Task<int> disLike(int reviewID);
        Task<int> removeDisLike(int reviewID);
        Task<List<ShowReviewDTO>> getAllReview(string craftsmanId);
        Task<int> DeleteReview(int reviewID);

    }
}