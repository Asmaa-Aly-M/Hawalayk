using System.ComponentModel.DataAnnotations;

namespace Hawalayk_APP.DataTransferObject
{
    public class UpdateCustomerAccountDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public IFormFile ProfilePic { get; set; }
        public string Governorate { get; set; }
        public string City { get; set; }
        public string StreetName { get; set; }
    }
}
