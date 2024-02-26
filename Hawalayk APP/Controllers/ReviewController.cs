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
        [HttpPost]
        public IActionResult writeReview(Review newreview) /////هل محتاجين يرجع حاجة معينة
        {
            review.Create(newreview);
            return Ok();
        }
        [HttpPost]
        public IActionResult like(int reviewId)   
        {
            Review theReview = review.GetById(reviewId);
            int newPositiveReacts = theReview.PositiveReacts + 1;
            return Ok(newPositiveReacts);///// هل محتاجين يرجع حاجة
        }
        /*
        [HttpPost]
        public IActionResult Removelike(int reviewId)///////////////////function جديدة
        {
            Review theReview = review.GetById(reviewId);
            int newPositiveReacts = theReview.PositiveReacts - 1;
            return Ok(newPositiveReacts);///// هل محتاجين يرجع حاجة
        }*/

        [HttpPost]
        public IActionResult disLike(int reviewId)
        {
            Review theReview = review.GetById(reviewId);
            int newNegativeReacts = theReview.NegativeReacts + 1;
            return Ok(newNegativeReacts);///// هل محتاجين يرجع حاجة
        }

        /*
        [HttpPost]
        public IActionResult removeDisLike(int reviewId) ///function جديدة
        {
            Review theReview = review.GetById(reviewId);
            int newNegativeReacts = theReview.NegativeReacts - 1;
            return Ok(newNegativeReacts);///// هل محتاجين يرجع حاجة
        }*/


    }
}
