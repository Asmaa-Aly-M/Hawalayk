using System.ComponentModel.DataAnnotations;

namespace Hawalayk_APP.DataTransferObject
{
    public class ResetPasswordModel
    {
        [Required]
        public string NewPassword { get; set; }
        [Required, Compare("Password")]
        public string ConfirmPassword { get; set; }
        [Required]
        public string OTPToken { get; set;}
    }
}
