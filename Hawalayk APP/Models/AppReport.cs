using Hawalayk_APP.Enums;
using System.ComponentModel.DataAnnotations;

namespace Hawalayk_APP.Models
{
    public class AppReport
    { 
        public int Id { get; set; }
        [Required]
        public ApplicationUser Reporter { get; set; }
        public ReportedIssue ReportedIssue { get; set; }
        public string? Description { get; set; }
        public DateTime DatePosted { get; set; }
    }
}
