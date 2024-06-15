namespace Hawalayk_APP.DataTransferObject
{
    public class AvailableServiceRequestDTO
    {
        public string CustomerID { get; set; }
        public string CustomerFirstName { get; set; }
        public string CustomerLastName { get; set; }
        public string CustomerProfilePicture { get; set; }
        public string Content { get; set; }
        public string? OptionalImage { get; set; }
        public string Governorate { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public int ServiceRequestId { get; set; }
        public DateTime DatePosted { get; set; }

    }
}
