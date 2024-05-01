using System.ComponentModel.DataAnnotations;

namespace Hawalayk_APP.DataTransferObject
{
    public class AddressDTO
    {
        [Required]
        public string GovernorateName { get; set; }

        [Required]
        public string CityName { get; set; }

        [Required]
        public string StreetName { get; set; }
    }
}
