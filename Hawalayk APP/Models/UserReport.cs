using System.ComponentModel.DataAnnotations;

namespace Hawalayk_APP.Models
{
    public class UserReport
    {
         
        public int Id { get; set; }

        
        public ApplicationUser Reporter { get; set; }
        
        public ApplicationUser ReportedUser { get; set; }
        [Required]
        public string Description { get; set; }
        public DateTime DatePosted { get; set; }
    }
}
