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
        private readonly IReviewRepository _reviewRepository;
        public ReviewController(IReviewRepository reviewRepository)
        {
           _reviewRepository= reviewRepository;
        }
        [Route("view")]
        [HttpPost]
        public async Task<IActionResult> writeReview(ReviewDTO newreview) 
        {
            await _reviewRepository.Create(newreview);//مش المفروض يباصيلنا حاجة من نوع reviewDTO
            return Ok();
        }

        [Route("like")]
        [HttpPost]
        public async Task<IActionResult> like(int reviewId)
        {
            Review theReview = await _reviewRepository.GetById(reviewId);
            int newPositiveReacts = theReview.PositiveReacts + 1;
            return Ok(newPositiveReacts);
        }
        
        [Route("removeLike")]
        [HttpPost]
        public async Task<IActionResult> Removelike(int reviewId)
        {
            Review theReview = await _reviewRepository.GetById(reviewId);
            int newPositiveReacts = theReview.PositiveReacts - 1;
            return Ok(newPositiveReacts);
        }

        [Route("disLike")]
        [HttpPost]
        public async Task<IActionResult> disLike(int reviewId)
        {
            Review theReview = await _reviewRepository.GetById(reviewId);
            int newNegativeReacts = theReview.NegativeReacts + 1;
            return Ok(newNegativeReacts);///// هل محتاجين يرجع حاجة
        }

       
        [Route("removeDisLike")]
        [HttpPost]
        public async Task<IActionResult> removeDisLike(int reviewId) 
        {
            Review theReview = await _reviewRepository.GetById(reviewId);
            int newNegativeReacts = theReview.NegativeReacts - 1;
            return Ok(newNegativeReacts);
        }

        [HttpGet] //Test
        public async Task<IActionResult> GetAll()
        {
            var Reviews = await _reviewRepository.GetAll();
            return Ok(Reviews);
        }
        [HttpGet("{id}")]//Test
        public async Task<IActionResult> Get(int id)
        {
            var review = await _reviewRepository.GetById(id);

            if (review == null)
            {
                return NotFound();
            }

            var reviewDTO = new ReviewDTO();
            reviewDTO.Id = id;
            reviewDTO.Content = review.Content;
            reviewDTO.Rating = review.Rating;
            reviewDTO.PositiveReacts = review.PositiveReacts;
            reviewDTO.NegativeReacts = review.NegativeReacts;
            return Ok(reviewDTO);
        }
        [HttpPut] //test
        public async Task<IActionResult> Update(int id, ReviewUpdateDTO updatedReviewDTO) {


            var existingReview = await _reviewRepository.GetById(id);

            if (existingReview == null)
            {
                return NotFound();
            }

            
            existingReview.Content = updatedReviewDTO.Content;
            existingReview.Rating = updatedReviewDTO.Rating;

            await _reviewRepository.Update(id,existingReview);

            return NoContent();

        }

        [HttpDelete("{id}")]//test
        public async Task<IActionResult> DeleteReview(int id)
        {
            var existingReview = await _reviewRepository.GetById(id);

            if (existingReview == null)
            {
                return NotFound();
            }

            await _reviewRepository.Delete(id);

            return NoContent();
        }
    }
}
