using Hawalayk_APP.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hawalayk_APP.Models
{
    public class JobApplication
    {
        public int Id { get; set; }
        public string? Content { get; set; }
        
        public int InitialPrice { get; set; }
        public ResponseStatus ResponseStatus { get; set; }
        [ForeignKey("Craftsman")]
        public int CraftsmanId { get; set; }
        public Craftsman Craftsman { get; set; }
        public DateTime DatePosted { get; set; } 
}
}
   
