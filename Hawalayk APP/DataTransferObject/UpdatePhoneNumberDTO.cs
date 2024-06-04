using System.ComponentModel.DataAnnotations;

namespace Hawalayk_APP.DataTransferObject
{
    public class UpdatePhoneNumberDTO
    {
        [Required, RegularExpression(@"^\+201([0-2]|5){1}[0-9]{8}$", ErrorMessage = "Invalid phone number format.")]
        public string NewPhoneNumber { get; set; }

        [Required, StringLength(50)]
        public string Password { get; set; }
    }
}
