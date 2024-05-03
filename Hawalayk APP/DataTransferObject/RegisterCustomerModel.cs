using Hawalayk_APP.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        [Required, RegularExpression(@"^\+201[0-2]{1}[0-9]{8}$", ErrorMessage = "Invalid phone number format.")]
        public string PhoneNumber { get; set; }

        [Required, StringLength(50)]
        public string Password { get; set; }
        [Compare("Password")]
        public string PasswordConfirmed { get; set; }
        //مش لازم يحطها في التسجيل عادي تتساب فاضية ويحطها بعدين او منطلبهاش منه اصلا
        //public Image ProfilePic { get; set; }
        //public Address Address { get; set; }
        public string Goveronrate { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public DateTime BirthDate { get; set; }
        [NotMapped]
        public IFormFile ProfilePic { get; set; }



    }
}
