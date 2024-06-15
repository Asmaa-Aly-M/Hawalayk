using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hawalayk_APP.Models
{
    public class Review
    {
       
        public int Id { get; set; }
        
        public int Rating { get; set; }
        [Required]
        public string Content { get; set; }
        public int? PositiveReacts { get; set; }
        public int? NegativeReacts { get; set; }
        public DateTime DatePosted { get; set; }

        [ForeignKey("Craftsman")]
        public string CraftsmanId { get; set; }
        public Craftsman Craftsman { get; set; }

        [ForeignKey("Customer")]
        public string CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}
