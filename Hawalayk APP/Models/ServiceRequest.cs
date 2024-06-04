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
        [ForeignKey("craftName")]
        public int CraftId { get; set; }
        public Craft craft { get; set; }
        public string Governorate { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public DateTime DatePosted { get; set; } = DateTime.Now;

        public CraftName craftName { get; set; }
        public ICollection<JobApplication> JobApplications { get; set; }
        // any craft 
        public string governorate { get; set; }
        public string city { get; set; }
        public string street { get; set; }




    }
}
