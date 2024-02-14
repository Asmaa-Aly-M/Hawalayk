using System.ComponentModel.DataAnnotations;
using static System.Net.Mime.MediaTypeNames;

namespace Hawalayk_APP.model
{
    public class Advertisement
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public Image Image { get; set; }
        public string ClickUrl { get; set; }
        public string Advertiser { get; set; }
        public string Description { get; set; }
        public int NumOfClicks { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
