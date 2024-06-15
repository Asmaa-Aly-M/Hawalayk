using Hawalayk_APP.DataTransferObject;
using Hawalayk_APP.Models;
using Hawalayk_APP.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
           _reviewRepository= reviewRepository;
        }
        //[Route("view")]
        [HttpPost("write a Review")]
        public async Task<IActionResult> writeReview(string craftsman,ReviewDTO newreview) 
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return BadRequest("token=null.");
            }
            await _reviewRepository.Create(craftsman, userId ,newreview);//مش المفروض يباصيلنا حاجة من نوع reviewDTO
            return Ok(newreview);
        }
        

        //[Route("like")]
        [HttpPost("click like")]
        public async Task<IActionResult> like(int reviewId)
        {
           var newPositiveReacts = await _reviewRepository.like(reviewId);
            return Ok(newPositiveReacts);
        }
       
        //[Route("removeLike")]
        [HttpPost("remove a Like")]
        public async Task<IActionResult> Removelike(int reviewId)
        {
            var newPositiveReacts = await _reviewRepository.removeLike(reviewId);
            return Ok(newPositiveReacts);
        }

       // [Route("disLike")]
        [HttpPost("click a dislike")]
        public async Task<IActionResult> disLike(int reviewId)
        {
            var newNegativeReacts = await _reviewRepository.disLike(reviewId);
            return Ok(newNegativeReacts);
        }

       
        //[Route("removeDisLike")]
        [HttpPost("removeDisLike")]
        public async Task<IActionResult> removeDisLike(int reviewId) 
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

        [HttpDelete("DeleteReview")]//test
       public async Task<IActionResult> DeleteReview(int reviewID)
       {
           var review = await _reviewRepository.Delete( reviewID);
            if (review == 0)
            {
                // Return a 404 Not Found if no review was deleted
                return NotFound("Review not found.");
            }
            return Ok("review is deleted");
       }


        [HttpGet("Get All Review for a craftsman")]
        public async Task<IActionResult> getAllReview(string craftsmanId) 
        { 
            var reviews = await _reviewRepository.getAllReview(craftsmanId);
            return Ok(reviews);
        }



    }
}
