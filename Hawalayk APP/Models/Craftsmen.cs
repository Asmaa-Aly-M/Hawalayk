using System.ComponentModel.DataAnnotations.Schema;

namespace Hawalayk_APP.Models
{

    public class Craftsmen:ApplicationUser
    {
        
        public Image PersonalImage { get; set; }
        public Image NationalIDImage { get; set; }
        [ForeignKey("Craft")]
        public int CraftId { get; set; }
        public Craft Craft { get; set; }
        public double Rating { get; set; }
        public ICollection<Post> Portfolio { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public ICollection<JobApplication> JobAapplications { get; set; }

    }
}
