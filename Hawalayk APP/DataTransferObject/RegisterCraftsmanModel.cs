using Hawalayk_APP.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hawalayk_APP.DataTransferObject
{
    public class RegisterCraftsmanModel
    {
        [Required, StringLength(50)]
        public string FirstName { get; set; }

        [Required, StringLength(50)]
        public string LastName { get; set; }

        [Required, StringLength(30)]
        public string UserName { get; set; }

        [Required, RegularExpression(@"^\+201([0-2]|5){1}[0-9]{8}$", ErrorMessage = "Invalid phone number format.")]
        public string PhoneNumber { get; set; }

        [Required, StringLength(50)]
        public string Password { get; set; }
        [Compare("Password")]
        public string PasswordConfirmed { get; set; }

        public string CraftName { get; set; }
        //[Required]



        ///public Address Address { get; set; }
        [Required]
        public Gender Gender { get; set; }
        public DateTime BirthDate { get; set; }
        [NotMapped]
        public IFormFile ProfilePic { get; set; }
        [Required]
        public string Goveronrate { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Street { get; set; }
        [NotMapped]
        public IFormFile PersonalImage { get; set; }
        //[Required]
        [NotMapped]

        public IFormFile NationalIdImage { get; set; }


    }
}
