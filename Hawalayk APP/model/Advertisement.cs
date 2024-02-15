using System.ComponentModel.DataAnnotations;
using static System.Net.Mime.MediaTypeNames;

namespace Hawalayk_APP.model
{
    public class Advertisement
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public Image ?Image { get; set; }
        [Required]
        public string ClickUrl { get; set; }
        [Required]
        public string Advertiser { get; set; }
        [Required]
        public string Description { get; set; }
        public int? NumOfClicks { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        [Required]
        public DateTime DateCreated { get; set; }
    }
}
