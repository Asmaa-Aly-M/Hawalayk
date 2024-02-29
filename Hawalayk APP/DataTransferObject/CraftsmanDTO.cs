using System.ComponentModel.DataAnnotations;

namespace Hawalayk_APP.DataTransferObject
{
    public class CraftsmanDTO
    {
        public string CraftName { get; set; }
        public string FirstName { get; set; }

       
        public string LastName { get; set; }

      
        public string UserName { get; set; }
        public double Rating { get; set; }
    }
}
