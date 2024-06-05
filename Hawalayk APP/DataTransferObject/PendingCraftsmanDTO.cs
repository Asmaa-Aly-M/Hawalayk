using Hawalayk_APP.Enums;
using System.ComponentModel.DataAnnotations;

namespace Hawalayk_APP.DataTransferObject
{
    public class PendingCraftsmanDTO
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
        [Required, RegularExpression(@"^\+201([0-2]|5){1}[0-9]{8}$", ErrorMessage = "Invalid phone number format.")]
        public string PhoneNumber { get; set; }
        [Required]
        public Gender Gender { get; set; }
        [Required]
        public string Governorate { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string StreetName { get; set; }
        [Required]
        public string Craft { get; set; }
        [Required]
        public string PersonalImage { get; set; }
        [Required]
        public string NationalIDImage { get; set; }
    }
}
