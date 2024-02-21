using System.ComponentModel.DataAnnotations;

namespace Hawalayk_APP.Models
{
    public class RegisterCustomerModel
    {
        [Required, StringLength(100)]
        public string FirstName { get; set; }

        [Required, StringLength(100)]
        public string LastName { get; set; }

        [Required, StringLength(50)]
        public string Username { get; set; }

        [Required, StringLength(128)]
        public string PhoneNumber { get; set; }

        [Required, StringLength(256)]
        public string Password { get; set; }

        public DateTime BirthDate { get; set; }


    }
}
