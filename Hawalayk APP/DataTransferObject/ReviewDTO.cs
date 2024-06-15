using System.ComponentModel.DataAnnotations;

namespace Hawalayk_APP.DataTransferObject
{
    public class ReviewDTO
    {
        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5.")]
        public int Rating { get; set; }
        [Required(ErrorMessage = "Content is required.")]
        public string Content { get; set; }
    }
}
