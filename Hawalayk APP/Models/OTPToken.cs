namespace Hawalayk_APP.Models
{
    public class OTPToken
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string PhoneNumber { get; set; }
        public string Token { get; set; }
        public DateTime ExpirationTime { get; set; }
    }
}
