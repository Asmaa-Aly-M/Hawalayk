using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hawalayk_APP.Models
{
   
    public class Craftsman:ApplicationUser 
    { 
     
        public string PersonalImage  { get; set; }
        public string  NationalIDImage { get; set; }

        [ForeignKey("Craft")]
        public int? CraftId { get; set; }
        public Craft? Craft { get; set; }

        public double Rating { get; set; }
        public ICollection<Post> Portfolio { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public ICollection<JobApplication> JobApplications { get; set; }
    }
}
