using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static System.Net.Mime.MediaTypeNames;

namespace Hawalayk_APP.Models
{
    public class Post
    {
        public int Id { get; set; }
        [Required]
        public Image Image { get; set; }

        [Required]
        [ForeignKey("Craftsman")]
        public int CraftsmanId { get; set; }
        public Craftsman Craftsman { get; set; }
        public string? Content { get; set; }
    }
}
