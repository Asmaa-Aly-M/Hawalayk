﻿namespace Hawalayk_APP.DataTransferObject
{
    public class CraftsmanAccountDTO
    {
        public string CraftsmanId { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string UserName { get; set; }

        //[Required, StringLength(11)]
        //public string PhoneNumber { get; set; }

        // [Required, StringLength(50)]
        // public string Password { get; set; }

        public string Governorate { get; set; }
        public string City { get; set; }
        public string street { get; set; }
        public string PhoneNumber { get; set; }
        public string CraftName { get; set; }

        public string ProfilePic { get; set; }
        //[Required]
        //public Image NationalIdImage { get; set; }
        //public Address Address { get; set; }


        public DateTime BirthDate { get; set; }
        public double Rating { get; set; }
    }
}
