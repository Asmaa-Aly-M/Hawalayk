namespace Hawalayk_APP.DataTransferObject
{
    public class JobApplicationSendDTO
    {
        public int JobApplicationId { get; set; }
        public string CraftsmanId { get; set; }
        public string CraftsmanFirstName { get; set; }
        public string CraftsmanLastName { get; set; }
        public string CraftsmanUserName { get; set; }
        public string Content { get; set; }
        public int InitialPrice { get; set; }
        public string CraftsmanImg { get; set; }
        public double Rating { get; set; }
    }
}
