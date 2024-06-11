using System.ComponentModel.DataAnnotations.Schema;

namespace Hawalayk_APP.DataTransferObject
{
    public class ServiceRequestDTO
    {
        public string governorate { get; set; }
        public string city { get; set; }
        public string street { get; set; }
        // public int Id { get; set; }
        public string craftName { get; set; }
        public string content { get; set; }
        [NotMapped]
        public IFormFile? optionalImage { get; set; }

    }
}
