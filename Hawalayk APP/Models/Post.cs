using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static System.Net.Mime.MediaTypeNames;

namespace Hawalayk_APP.Models
{
    public class Post
    {
        public int Id { get; set; }
       
       
        public string Image { get; set; }

        
        [ForeignKey("Craftsman")]
        public string CraftsmanId { get; set; }
        public Craftsman Craftsman { get; set; }

        [ForeignKey("craft")]
        public string CraftId { get; set; }
        public Craft craft { get; set; }

        public string? Content { get; set; }
    }
}