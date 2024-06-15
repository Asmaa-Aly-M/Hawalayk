namespace Hawalayk_APP.DataTransferObject
{
    public class RequestAcceptedForCraftsmanDTO
    {
        public string CustomerID { get; set; }
        public string CraftsmanID { get; set; }
        public string CustomerFristName { get; set; }
        public string CustomerLastName { get; set; }
        public string CustomerProfilePicture { get; set; }
        public string Content { get; set; }
        public string? OptionalImage { get; set; }
        public int ServiceRequestId { get; set; }
        public DateTime DatePosted { get; set; }
        public int JobApplicationId { get; set; }
    }
}
