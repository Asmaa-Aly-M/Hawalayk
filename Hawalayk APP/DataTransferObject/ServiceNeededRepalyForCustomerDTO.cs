namespace Hawalayk_APP.DataTransferObject
{
    public class ServiceNeededRepalyForCustomerDTO
    {
        public int ServiceRequestId { get; set; }
        public string ServiceCraftName { get; set; }
        public string ServiceContent { get; set; }
        public string CustomerId { get; set; }
        public string? OptionalImage { get; set; }
        public DateTime Date { get; set; }
    }
}
