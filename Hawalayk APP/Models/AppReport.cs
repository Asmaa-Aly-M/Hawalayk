using Hawalayk_APP.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hawalayk_APP.Models
{
    public class AppReport
    {
        public int Id { get; set; }
        [ForeignKey("Reporter")]
        public string ReporerId { get; set; }
        public ApplicationUser Reporter { get; set; }
        public ReportedIssue ReportedIssue { get; set; }
        public string? Description { get; set; }
        public DateTime DatePosted { get; set; } = DateTime.Now;
    }
}
