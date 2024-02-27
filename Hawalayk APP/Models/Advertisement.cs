using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static System.Net.Mime.MediaTypeNames;

namespace Hawalayk_APP.Models
{
    public class Advertisement
    {
        
        public int Id { get; set; }
        
        public string Title { get; set; }

        public string Image { get; set; }
      
        public string ClickUrl { get; set; }
         
        public string Advertiser { get; set; }
        
        public string? Description { get; set; }
        public int NumOfClicks { get; set; }
        public DateTime StartDate { get; set; } 
        public DateTime EndDate { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
