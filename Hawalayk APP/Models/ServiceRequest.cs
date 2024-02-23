using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static System.Net.Mime.MediaTypeNames;

namespace Hawalayk_APP.Models
{
    public class ServiceRequest
    {
      
        public int Id { get; set; }
        
        public string Content { get; set; }
        [ForeignKey("Image")]
        public int? ImageId { get; set; }
        public Image? Image { get; set; }
     
        [ForeignKey("Customer")]
        public string CustomerId { get; set; }
        public Customer Customer { get; set; }
        public DateTime DatePosted { get; set; }
    }
}
