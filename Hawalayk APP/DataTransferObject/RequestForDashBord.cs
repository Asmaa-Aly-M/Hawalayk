using Hawalayk_APP.Enums;

namespace Hawalayk_APP.DataTransferObject
{
    public class RequestForDashBord
    {
        public int id { get; set; }
        public string Content { get; set; }
        public string CustomerId { get; set; }
        public string CustomerFristName { get; set; }
        public string CustomerLastName { get; set; }

        public string CraftsmanFristName { get; set; }
        public string CraftsmanLastName { get; set; }
        public DateTime DatePosted { get; set; }
        public ResponseStatus? ResponseStatus { get; set; }
    }
}
