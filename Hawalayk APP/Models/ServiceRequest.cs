using Hawalayk_APP.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hawalayk_APP.Models
{
    public class ServiceRequest
    {

        public int Id { get; set; }

        public string Content { get; set; }

        public string OptionalImage { get; set; }

        [ForeignKey("Customer")]
        public string CustomerId { get; set; }
        public Customer Customer { get; set; }
        public DateTime DatePosted { get; set; } = DateTime.Now;
        public CraftName craftName { get; set; }
        public ICollection<JobApplication> jobApplications { get; set; }
        // any craft 
    }
}
