using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static System.Net.Mime.MediaTypeNames;

namespace Hawalayk_APP.Model
{
    public class Post
    {
        public int Id { get; set; }
        [Required]
        public Image Image { get; set; }

        [Required]
        public Craftsman Craftsman { get; set; }
        public string? Content { get; set; }
    }
}
