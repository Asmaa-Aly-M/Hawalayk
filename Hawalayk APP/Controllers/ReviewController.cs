using Hawalayk_APP.DataTransferObject;
using Hawalayk_APP.Models;
using Hawalayk_APP.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult writeReview(ReviewDTO newreview) 
        {
            review.Create(newreview);
            return Ok();
        }

        [Route("like")]
        [HttpPost]
        public IActionResult like(int reviewId)   
        {
            Review theReview = review.GetById(reviewId);
            int newPositiveReacts = theReview.PositiveReacts + 1;
            return Ok(newPositiveReacts);
        }
        
        [Route("removeLike")]
        [HttpPost]
        public IActionResult Removelike(int reviewId)
        {
            Review theReview = review.GetById(reviewId);
            int newPositiveReacts = theReview.PositiveReacts - 1;
            return Ok(newPositiveReacts);
        }

        [Route("disLike")]
        [HttpPost]
        public IActionResult disLike(int reviewId)
        {
            Review theReview = review.GetById(reviewId);
            int newNegativeReacts = theReview.NegativeReacts + 1;
            return Ok(newNegativeReacts);///// هل محتاجين يرجع حاجة
        }

       
        [Route("removeDisLike")]
        [HttpPost]
        public IActionResult removeDisLike(int reviewId) 
        {
            Review theReview = review.GetById(reviewId);
            int newNegativeReacts = theReview.NegativeReacts - 1;
            return Ok(newNegativeReacts);
        }


    }
}
