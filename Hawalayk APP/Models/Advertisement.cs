using System.ComponentModel.DataAnnotations;
using static System.Net.Mime.MediaTypeNames;

namespace Hawalayk_APP.Models
{
    public class Advertisement
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }

        [Required]
        public Image Image { get; set; }
        [Required]
        public string ClickUrl { get; set; }
        [Required]
        public string Advertiser { get; set; }
        
        public string? Description { get; set; }
        public int NumOfClicks { get; set; }
        public DateTime StartDate { get; set; } 
        public DateTime EndDate { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
