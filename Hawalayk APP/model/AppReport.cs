using System.ComponentModel.DataAnnotations;

namespace Hawalayk_APP.model
{
    public class AppReport
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public User Reporter { get; set; }
        public ReportedIssue ReportedIssue { get; set; }
        public string? Description { get; set; }
        public DateTime DatePosted { get; set; }
    }
}
