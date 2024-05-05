using Hawalayk_APP.Context;
using Hawalayk_APP.DataTransferObject;
using Hawalayk_APP.Models;
using Microsoft.EntityFrameworkCore;

namespace Hawalayk_APP.Services
{
    public class ReviewRepository : IReviewRepository
    {
        ApplicationDbContext Context;
        public ReviewRepository(ApplicationDbContext _Context)
        {
            Context = _Context;
        }

        public async Task<Review> GetById(int id)
        {
            Review review = await Context.Reviews.FirstOrDefaultAsync(s => s.Id == id);
            return review;
        }
        public async Task<List<Review>> GetAll()
        {
            return await Context.Reviews.ToListAsync();
        }

        public async Task<int> Create(ReviewDTO newReview)
        {
            Review review = new Review
            {
                Id = newReview.Id,
                Rating = newReview.Rating,
                Content = newReview.Content,
            };
            Context.Reviews.Add(review);
            int row = await Context.SaveChangesAsync();
            return row;
        }
        public async Task<int> Update(int id, Review newReview)
        {
            Review OldReview = await Context.Reviews.FirstOrDefaultAsync(s => s.Id == id);
            OldReview.Rating = newReview.Rating;
            OldReview.Content = newReview.Content;
            //OldReview.PositiveReacts = newReview.PositiveReacts; //ازاي هعدل على عدد الريأكتات و التاريخ؟؟
            //OldReview.NegativeReacts = newReview.NegativeReacts;
            //OldReview.DatePosted = newReview.DatePosted;
            int row = await Context.SaveChangesAsync();
            return row;
        }
        public async Task<int> Delete(int id)
        {
            Review OldReview = await Context.Reviews.FirstOrDefaultAsync(s => s.Id == id);
            Context.Reviews.Remove(OldReview);
            int row = await Context.SaveChangesAsync();
            return row;
        }
    }
}
