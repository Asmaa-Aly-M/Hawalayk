using Hawalayk_APP.Attributes;
using Hawalayk_APP.DataTransferObject;
using Hawalayk_APP.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Hawalayk_APP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewRepository _reviewRepository;
        public ReviewController(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }
        //[Route("view")]
        [ServiceFilter(typeof(BlockingFilter))]
        [BlockCheck("craftsmanId")]
        [HttpPost("WriteReview/{craftsmanId}")]
        public async Task<IActionResult> WriteReview(string craftsmanId, ReviewDTO newreview)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return BadRequest("invalid Token");
            }
            await _reviewRepository.Create(craftsmanId, userId, newreview);//مش المفروض يباصيلنا حاجة من نوع reviewDTO
            return Ok(newreview);
        }


        //[Route("like")]
        [HttpPost("ClickLike/{reviewId}")]
        public async Task<IActionResult> Like(int reviewId)
        {
            var newPositiveReacts = await _reviewRepository.like(reviewId);
            return Ok(newPositiveReacts);
        }

        //[Route("removeLike")]
        [HttpPost("RemoveLike/{reviewId}")]
        public async Task<IActionResult> RemoveLike(int reviewId)
        {
            var newPositiveReacts = await _reviewRepository.removeLike(reviewId);
            return Ok(newPositiveReacts);
        }

        // [Route("disLike")]
        [HttpPost("ClickDislike/{reviewId}")]
        public async Task<IActionResult> DisLike(int reviewId)
        {
            var newNegativeReacts = await _reviewRepository.disLike(reviewId);
            return Ok(newNegativeReacts);
        }


        //[Route("removeDisLike")]
        [HttpPost("RemoveDisLike/{reviewId}")]
        public async Task<IActionResult> RemoveDisLike(int reviewId)
        {
            var newNegativeReacts = await _reviewRepository.removeLike(reviewId);
            return Ok(newNegativeReacts);
        }
        /*
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
         */

        [HttpDelete("DeleteReview/{reviewId}")]//test
        public async Task<IActionResult> DeleteReview(int reviewID)
        {
            var review = await _reviewRepository.Delete(reviewID);
            if (review == 0)
            {
                // Return a 404 Not Found if no review was deleted
                return NotFound("Review not found.");
            }
            return Ok("review is deleted");
        }

        [ServiceFilter(typeof(BlockingFilter))]
        [BlockCheck("craftsmanId")]
        [HttpGet("GetAllReviewsForCraftsman/{craftsmanId}")]
        public async Task<IActionResult> GetAllReview(string craftsmanId)
        {
            var reviews = await _reviewRepository.getAllReview(craftsmanId);

            return Ok(reviews);
        }



    }
}
