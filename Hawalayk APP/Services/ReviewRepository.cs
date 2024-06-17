using Hawalayk_APP.Context;
using Hawalayk_APP.DataTransferObject;
using Hawalayk_APP.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hawalayk_APP.Services
{
    public class ReviewRepository : IReviewRepository
    {
        ApplicationDbContext Context;
        private readonly ICraftsmenRepository craftsmenRepository;
        private readonly IBlockingRepository _blockingRepository;
        public ReviewRepository(ApplicationDbContext _Context, ICraftsmenRepository _craftsmenRepository, IBlockingRepository blockingRepository)
        {
            Context = _Context;
            craftsmenRepository = _craftsmenRepository;
            _blockingRepository = blockingRepository;
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

        public async Task<int> Create(string craftsmanID,string customerID,ReviewDTO newReview)
        {
            var craftsman = await craftsmenRepository.GetByID(craftsmanID);

            Review review = new Review
            {
               // Id=newReview.Id,
                Rating = newReview.Rating,
                PositiveReacts=0,
                NegativeReacts=0,
                Content = newReview.Content,
                CraftsmanId= craftsman.Id,
                CustomerId= customerID,
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

        public async Task<int> like(int reviewID) 
        {

            Review theReview = await Context.Reviews.FirstOrDefaultAsync(s => s.Id == reviewID);
            int newPositiveReacts = (int)(theReview.PositiveReacts + 1);
            theReview.PositiveReacts = newPositiveReacts;
            await Context.SaveChangesAsync();

            return (int)theReview.PositiveReacts;

        }

        public async Task<int> removeLike(int reviewID)
        {

            Review theReview = await Context.Reviews.FirstOrDefaultAsync(s => s.Id == reviewID);
            int newPositiveReacts = (int)(theReview.PositiveReacts - 1);
            theReview.PositiveReacts = newPositiveReacts;
            await Context.SaveChangesAsync();

            return (int)theReview.PositiveReacts;

        }

        public async Task<int> disLike(int reviewID)
        {

            Review theReview = await Context.Reviews.FirstOrDefaultAsync(s => s.Id == reviewID);
            int newNegativeReacts = (int)(theReview.NegativeReacts + 1);
            theReview.NegativeReacts = newNegativeReacts;
            await Context.SaveChangesAsync();

            return (int)theReview.NegativeReacts;

        }

        public async Task<int> removeDisLike(int reviewID)
        {

            Review theReview = await Context.Reviews.FirstOrDefaultAsync(s => s.Id == reviewID);
            int newNegativeReacts = (int)(theReview.NegativeReacts - 1);
            theReview.NegativeReacts = newNegativeReacts;
            await Context.SaveChangesAsync();

            return (int)theReview.NegativeReacts;

        }

        public async Task<int> DeleteReview(int reviewID)
        {
            var existingReview = await Context.Reviews.FirstOrDefaultAsync(s => s.Id == reviewID);
            if (existingReview == null)
            {
                return 0; // No review found to delete
            }
            Context.Reviews.Remove(existingReview);
            int row = await Context.SaveChangesAsync();
            return row;
        }

        public async Task<List<ShowReviewDTO>> getAllReview(string craftsmanId)
        {
            var reviews=await Context.Reviews
                .Include(c=>c.Customer)
                .Where(r=>r.CraftsmanId == craftsmanId).ToListAsync();


            var ShowReviewDTO = reviews.Select(r =>
            {
                return new ShowReviewDTO
                {
                   Id=r.Id,
                   Rating = r.Rating,
                   Content = r.Content,
                   PositiveReacts = r.PositiveReacts,
                   NegativeReacts = r.NegativeReacts,
                   CustomerFristName = r.Customer.FirstName,
                   CustomerLastName = r.Customer.LastName,
                   CustomerProfileImage = r.Customer.ProfilePicture,
                };
            }).ToList();

            return ShowReviewDTO;
        }

    }
}
