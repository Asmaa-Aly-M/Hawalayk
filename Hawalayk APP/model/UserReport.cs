using System.ComponentModel.DataAnnotations;

namespace Hawalayk_APP.model
{
    public class UserReport
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public User Reporter { get; set; }
        [Required]
        public User ReportedUser { get; set; }
        public string Description { get; set; }
        public DateTime DatePosted { get; set; }
    }
}
