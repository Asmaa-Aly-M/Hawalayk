using System.ComponentModel.DataAnnotations;
using static System.Net.Mime.MediaTypeNames;

namespace Hawalayk_APP.model
{
    public class ServiceRequest
    {
        [Key]
        public int Id { get; set; }
        public string Content { get; set; }
        public Image Image { get; set; }
        public Customer Customer { get; set; }
        public DateTime DatePosted { get; set; }
    }
}
