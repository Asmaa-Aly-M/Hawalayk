﻿using System.ComponentModel.DataAnnotations;

namespace Hawalayk_APP.DataTransferObject
{
    public class CraftsmanUpdatedAccountDTO
    {
        [Required, StringLength(50)]
        public string FirstName { get; set; }

        [Required, StringLength(50)]
        public string LastName { get; set; }

        [Required, StringLength(30)]
        public string UserName { get; set; }

        //[Required, StringLength(11)]
        //public string PhoneNumber { get; set; }

        // [Required, StringLength(50)]
        // public string Password { get; set; }

        [Required, RegularExpression(@"^\+201([0-2]|5){1}[0-9]{8}$", ErrorMessage = "Invalid phone number format.")]
        public string PhoneNumber { get; set; }
        [Required]
        public string CraftName { get; set; }

        public IFormFile ProfilePic { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }
        public string Governorate { get; set; }
        public string City { get; set; }
        public string StreetName { get; set; }
    }

}
