using System.ComponentModel.DataAnnotations;

namespace Hawalayk_APP.Models
{
    public class Review
    {
       
        public int Id { get; set; }
        
        public int Rating { get; set; }
        public string Headline { get; set; }
        public string Content { get; set; }
        public int PositiveReacts { get; set; }
        public int NegativeReacts { get; set; }
        public DateTime DatePosted { get; set; }
    }
}
