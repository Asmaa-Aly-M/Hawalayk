using System.ComponentModel.DataAnnotations;

namespace Hawalayk_APP.DataTransferObject
{
    public class UpdateUserDTO
    {
        public bool IsUpdated { get; set; }
        public string Message { get; set; }
        public string FirstName { get; set; }

    
        public string LastName { get; set; }

 
        public string UserName { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }

    }
}
