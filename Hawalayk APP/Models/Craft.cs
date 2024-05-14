using Hawalayk_APP.Enums;

namespace Hawalayk_APP.Models
{
    public class Craft
    {
        public int Id { get; set; }

        public CraftName Name { get; set; }

        public ICollection<Craftsman> Craftsmen { get; set; }
        public ICollection<ServiceRequest> ServiceRequest { get; set; }
        public ICollection<Post> Gallery { get; set; }
    }
}//craft -> request 

