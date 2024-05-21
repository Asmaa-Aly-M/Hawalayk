namespace Hawalayk_APP.DataTransferObject
{
    public class RequestAcceptedForCustomrDTO
    { 
        public int ServiceRequestId { get; set; }
        public string CraftName { get; set; }
        public string ServiceContent { get; set;}
        public string CraftsmanFristName { get; set; }
        public string CraftsmanLastName { get; set; }
        public DateTime Date { get; set; }

    }
}
