namespace Hawalayk_APP.DataTransferObject
{
    public class RequestAcceptedForCraftsmanDTO
    {
        public string CustomerID { get; set; }
        public string CustomerFristName { get; set; }
        public string CustomerLastName { get; set; }
        public string CustomerProfilePicture { get; set; }
        public string Content { get; set; }
        public string? OptionalImage { get; set; }
    }
}
