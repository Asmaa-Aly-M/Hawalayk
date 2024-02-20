using Hawalayk_APP.Enums;
using Microsoft.AspNetCore.Identity;
using static System.Net.Mime.MediaTypeNames;

namespace Hawalayk_APP.Models
{

    public class ApplicationUser:IdentityUser
    { 
       
        public string FirstName { get; set; }

        public string LastName { get; set; }
        
        public Gender Gender { get; set; }
        public Address Address { get; set; }

        public DateTime BirthDate { get; set; }

        public Image ProfilePicture { get; set; }

       
    }
}
