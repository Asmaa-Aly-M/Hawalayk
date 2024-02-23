using Hawalayk_APP.Enums;
using Hawalayk_APP.Models;
using System.ComponentModel.DataAnnotations;

namespace Hawalayk_APP.DataTransferObject
{
    public class RegisterCustomerModel
    {
        [Required, StringLength(50)]
        public string FirstName { get; set; }

        [Required, StringLength(50)]
        public string LastName { get; set; }

        [Required, StringLength(30)]
        public string UserName { get; set; }

        [Required]
        public Gender Gender { get; set; }
        [Required, RegularExpression(@"^(010|011|012|015)\d{8}$", ErrorMessage = "Invalid phone number format.")]
         public string PhoneNumber { get; set; }

        [Required, StringLength(50)]
        public string Password { get; set; }
        [Compare("Password")]
        public string PasswordConfirmed { get; set; }
        //مش لازم يحطها في التسجيل عادي تتساب فاضية ويحطها بعدين او منطلبهاش منه اصلا
        //public Image ProfilePic { get; set; }
        //public Address Address { get; set; }
        public DateTime BirthDate { get; set; }



    }
}
