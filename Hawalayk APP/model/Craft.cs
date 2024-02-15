using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hawalayk_APP.Model
{
    public class Craft
    {
        public int Id { get; set; }
        [Required]
        public CraftName Name { get; set; }

        public ICollection<Craftsman> Craftsmen { get; set; }
        public ICollection<Post> Gallery { get; set; }
    }
}

