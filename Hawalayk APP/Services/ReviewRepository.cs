using Hawalayk_APP.Context;
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

        public int Create(Review newReview)
        {
            Context.Reviews.Add(newReview);
            int row = Context.SaveChanges();
            return row;
        }
        public int Update(int id, Review newReview)
        {
            Review OldReview = Context.Reviews.FirstOrDefault(s => s.Id == id);
            OldReview.Rating = newReview.Rating;
            OldReview.Content = newReview.Content;
            OldReview.PositiveReacts = newReview.PositiveReacts;
            OldReview.NegativeReacts = newReview.NegativeReacts;
            OldReview.DatePosted = newReview.DatePosted;
            OldReview.Headline = newReview.Headline;
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
