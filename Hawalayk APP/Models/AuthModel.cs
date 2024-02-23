using Hawalayk_APP.Enums;

namespace Hawalayk_APP.Models
{
    public class AuthModel
    {
        public string Message { get; set; }
        public bool IsAuthenticated { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public List<string> Roles { get; set; }
        public Gender Gender { get; set; }
        public string Token { get; set; }
        public string Email { get; set; }
        public Address Address { get; set; }
        
        public DateTime ExpiresOn { get; set; }
    }
}
