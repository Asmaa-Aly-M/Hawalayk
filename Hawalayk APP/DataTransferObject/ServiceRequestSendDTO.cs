
namespace Hawalayk_APP.DataTransferObject
{
    public class ServiceRequestSendDTO
    {
        public int ServiceRequestId { get; set; }
        public string CustomerId { get; set; }
        public string CustomerFirstName { get; set; }
        public string CustomerLastName { get; set; }
        public string CustomerUserName { get; set; }
        public string Content { get; set; }
        public string ServiceRequestImg { get; set; }
        public string CustomerImg { get; set; }

    }
}
