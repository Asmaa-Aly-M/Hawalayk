using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hawalayk_APP.Models
{
    public class UserReport
    {

        public int Id { get; set; }
        [ForeignKey("Reporter")]
        public string ReporerId { get; set; }

        public ApplicationUser? Reporter { get; set; }
        [ForeignKey("ReportedUser")]
        public string ReporedId { get; set; }

        public ApplicationUser ReportedUser { get; set; }

        public string Description { get; set; }
        public DateTime DatePosted { get; set; }
    }
}
