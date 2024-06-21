namespace Hawalayk_APP.DataTransferObject
{
    public class CraftsmanAccountDTO
    {
        public string CraftsmanId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Governorate { get; set; }
        public string City { get; set; }
        public string street { get; set; }
        public string PhoneNumber { get; set; }
        public string CraftName { get; set; }
        public string ProfilePic { get; set; }
        public double Rating { get; set; }
        public bool isBlocked { get; set; } = false;
    }
}
