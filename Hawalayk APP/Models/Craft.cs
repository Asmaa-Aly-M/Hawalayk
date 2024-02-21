using Hawalayk_APP.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hawalayk_APP.Models
{
    public class Craft
    {
        public int Id { get; set; }
       
        public CraftName Name { get; set; }

        public ICollection<Craftsmen> Craftsmen { get; set; }
        public ICollection<Post> Gallery { get; set; }
    }
}

