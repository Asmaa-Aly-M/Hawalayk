using Hawalayk_APP.DataTransferObject;
using Hawalayk_APP.Models;
using Hawalayk_APP.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hawalayk_APP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        IReviewRepository review;
        public ReviewController(IReviewRepository _review)
        {
            review = _review;
        }
        [Route("view")]
        [HttpPost]
        public async Task<IActionResult> writeReview(ReviewDTO newreview) 
        {
            await review.Create(newreview);//مش المفروض يباصيلنا حاجة من نوع reviewDTO
            return Ok();
        }

        [Route("like")]
        [HttpPost]
        public async Task<IActionResult> like(int reviewId)
        {
            Review theReview = await review.GetById(reviewId);
            int newPositiveReacts = theReview.PositiveReacts + 1;
            return Ok(newPositiveReacts);
        }
        
        [Route("removeLike")]
        [HttpPost]
        public async Task<IActionResult> Removelike(int reviewId)
        {
            Review theReview = await review.GetById(reviewId);
            int newPositiveReacts = theReview.PositiveReacts - 1;
            return Ok(newPositiveReacts);
        }

        [Route("disLike")]
        [HttpPost]
        public async Task<IActionResult> disLike(int reviewId)
        {
            Review theReview = await review.GetById(reviewId);
            int newNegativeReacts = theReview.NegativeReacts + 1;
            return Ok(newNegativeReacts);///// هل محتاجين يرجع حاجة
        }

       
        [Route("removeDisLike")]
        [HttpPost]
        public async Task<IActionResult> removeDisLike(int reviewId) 
        {
            Review theReview = await review.GetById(reviewId);
            int newNegativeReacts = theReview.NegativeReacts - 1;
            return Ok(newNegativeReacts);
        }

        [HttpGet] //Test
        public async Task<IActionResult> GetAll()
        {
            var Reviews = await review.GetAll();
            return Ok(Reviews);
        }
        [HttpGet("{id}")]//Test
        public async Task<IActionResult> Get(int id)
        {
            var Review = await review.GetById(id);

            if (Review == null)
            {
                return NotFound();
            }

            var ReviewDTO = new ReviewDTO();
            ReviewDTO.Id = id;
            ReviewDTO.Content = Review.Content;
            ReviewDTO.Rating = Review.Rating;
            ReviewDTO.PositiveReacts = Review.PositiveReacts;
            ReviewDTO.NegativeReacts = Review.NegativeReacts;
            return Ok(ReviewDTO);
        }
        [HttpPut] //test
        public async Task<IActionResult> Update(int id, ReviewUpdateDTO updatedReviewDTO) {


            var existingReview = await review.GetById(id);

            if (existingReview == null)
            {
                return NotFound();
            }

            
            existingReview.Content = updatedReviewDTO.Content;
            existingReview.Rating = updatedReviewDTO.Rating;

            await review.Update(id,existingReview);

            return NoContent();

        }

        [HttpDelete("{id}")]//test
        public async Task<IActionResult> DeleteReview(int id)
        {
            var existingReview = await review.GetById(id);

            if (existingReview == null)
            {
                return NotFound();
            }

            await review.Delete(id);

            return NoContent();
        }
    }
}
