using System.ComponentModel.DataAnnotations;
using static System.Net.Mime.MediaTypeNames;

namespace Hawalayk_APP.model
{
    public class ServiceRequest
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Content { get; set; }
        public Image? Image { get; set; }
        [Required]
        public Customer Customer { get; set; }
        public DateTime DatePosted { get; set; }
    }
}
