using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hawalayk_APP.Models
{
   
    public class Craftsman:ApplicationUser 
    { 
     
        [ForeignKey("PersonalImage")]
        public int? PersonalImageId { get; set; }
        public Image? PersonalImage { get; set; }

        [ForeignKey("NationalIDImage")]
        public int?NationalIDImageId { get; set; }
        public Image? NationalIDImage { get; set; }

        [ForeignKey("Craft")]
        public int CraftId { get; set; }
        public Craft Craft { get; set; }

        public double Rating { get; set; }
        public ICollection<Post> Portfolio { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public ICollection<JobApplication> JobApplications { get; set; }
    }
}
