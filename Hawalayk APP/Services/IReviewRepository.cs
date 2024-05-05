using Hawalayk_APP.DataTransferObject;
using Hawalayk_APP.Models;

namespace Hawalayk_APP.Services
{
    public interface IReviewRepository
    {
        Task<int> Create(ReviewDTO newReview);
        Task<int> Delete(int id);
        Task<List<Review>> GetAll();
        Task<Review> GetById(int id);
        Task<int> Update(int id, Review newReview);
    }
}