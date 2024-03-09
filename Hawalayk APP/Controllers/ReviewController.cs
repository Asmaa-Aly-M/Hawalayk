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
        public IActionResult writeReview(Review newreview) /////هل محتاجين يرجع حاجة معينة
        {
            review.Create(newreview);//مش المفروض يباصيلنا حاجة من نوع reviewDTO
            return Ok();
        }
        [Route("like")]
        [HttpPost]
        public IActionResult like(int reviewId)
        {
            Review theReview = review.GetById(reviewId);
            int newPositiveReacts = theReview.PositiveReacts + 1;
            return Ok(newPositiveReacts);///// هل محتاجين يرجع حاجة
        }
        /*
         [Route("removeLike")]
        [HttpPost]
        public IActionResult Removelike(int reviewId)///////////////////function جديدة
        {
            Review theReview = review.GetById(reviewId);
            int newPositiveReacts = theReview.PositiveReacts - 1;
            return Ok(newPositiveReacts);///// هل محتاجين يرجع حاجة
        }*/
        [Route("disLike")]
        [HttpPost]
        public IActionResult disLike(int reviewId)
        {
            Review theReview = review.GetById(reviewId);
            int newNegativeReacts = theReview.NegativeReacts + 1;
            return Ok(newNegativeReacts);///// هل محتاجين يرجع حاجة
        }

        /*
         [Route("removeDisLike")]
        [HttpPost]
        public IActionResult removeDisLike(int reviewId) ///function جديدة
        {
            Review theReview = review.GetById(reviewId);
            int newNegativeReacts = theReview.NegativeReacts - 1;
            return Ok(newNegativeReacts);///// هل محتاجين يرجع حاجة
        }*/

        [HttpGet] //Test
        public IActionResult GetAll()
        {
            var Reviews = review.GetAll();
            return Ok(Reviews);
        }
        [HttpGet("{id}")]//Test
        public IActionResult Get(int id)
        {
            var Review = review.GetById(id);

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
        public IActionResult Update(int id, ReviewUpdateDTO updatedReviewDTO) {


            var existingReview = review.GetById(id);

            if (existingReview == null)
            {
                return NotFound();
            }

            
            existingReview.Content = updatedReviewDTO.Content;
            existingReview.Rating = updatedReviewDTO.Rating;

            review.Update(id,existingReview);

            return NoContent();

        }

        [HttpDelete("{id}")]//test
        public IActionResult DeleteReview(int id)
        {
            var existingReview = review.GetById(id);

            if (existingReview == null)
            {
                return NotFound();
            }

            review.Delete(id);

            return NoContent();
        }
    }
}
