using Hawalayk_APP.Context;
using Hawalayk_APP.DataTransferObject;
using Hawalayk_APP.Models;

namespace Hawalayk_APP.Services
{
    public class ReviewRepository : IReviewRepository
    {
        ApplicationDbContext Context;
        public ReviewRepository(ApplicationDbContext _Context)
        {
            Context = _Context;
        }

        public Review GetById(int id)
        {
            Review review = Context.Reviews.FirstOrDefault(s => s.Id == id);
            return review;
        }
        public List<Review> GetAll()
        {
            return Context.Reviews.ToList();
        }

        public int Create(ReviewDTO newReview)
        {
            Review review = new Review
            {
                Id = newReview.Id,
                Rating = newReview.Rating,
                Content = newReview.Content,
            };
            Context.Reviews.Add(review);
            int row = Context.SaveChanges();
            return row;
        }
        public int Update(int id, ReviewDTO newReview)
        {
            Review OldReview = Context.Reviews.FirstOrDefault(s => s.Id == id);
            OldReview.Rating = newReview.Rating;
            OldReview.Content = newReview.Content;
            //OldReview.PositiveReacts = newReview.PositiveReacts; //ازاي هعدل على عدد الريأكتات و التاريخ؟؟
            //OldReview.NegativeReacts = newReview.NegativeReacts;
            //OldReview.DatePosted = newReview.DatePosted;
            int row = Context.SaveChanges();
            return row;
        }
        public int Delete(int id)
        {
            Review OldReview = Context.Reviews.FirstOrDefault(s => s.Id == id);
            Context.Reviews.Remove(OldReview);
            int row = Context.SaveChanges();
            return row;
        }
    }
}
