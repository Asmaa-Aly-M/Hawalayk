﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hawalayk_APP.DataTransferObject
{
    public class CraftsmanUpdatedAccountDTO
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string UserName { get; set; }
        public string Governorate { get; set; }
        public string City { get; set; }

        
        //[Required, RegularExpression(@"^\+201([0-2]|5){1}[0-9]{8}$", ErrorMessage = "Invalid phone number format.")]
        //public string PhoneNumber { get; set; }

        public string CraftName { get; set; }

        public string StreetName { get; set; }
    }

}
