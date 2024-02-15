using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hawalayk_APP.Model
{
    public class JobApplication
    {
        public int Id { get; set; }
        public string? Content { get; set; }
        [Required]
        public int InitialPrice { get; set; }
        public ResponseStatus ResponseStatus { get; set; }
        [ForeignKey("Craftsman")]
        public int CraftsmanId { get; set; }
        public Craftsman Craftsman { get; set; }
        public DateTime DatePosted { get; set; } 
}
}
   
