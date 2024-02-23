using Hawalayk_APP.Enums;
using Hawalayk_APP.Models;
using System.ComponentModel.DataAnnotations;

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

        [Required, StringLength(11)]
        public string PhoneNumber { get; set; }

        [Required, StringLength(50)]
        public string Password { get; set; }
        [Compare("Password")]
        public string PasswordConfirmed { get; set; }

        public CraftName CraftName { get; set; }
        //[Required]
        //public Image PersonalImage { get; set; }
        //[Required]
        //public Image NationalIdImage { get; set; }
        //public Address Address { get; set; }
        [Required]
        public Gender Gender { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
