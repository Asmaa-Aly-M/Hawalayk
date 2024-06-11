namespace Hawalayk_APP.DataTransferObject
{
    public class AcceptedJobApplicationDTO
    {
        public string CraftsmanID { get; set; }
        public string CraftsmanFristName { get; set; }
        public string CraftsmanLastName { get; set; }
        public string CraftsmanProfilePicture { get; set; }
        public string? Content { get; set; }
        public int InitialPrice { get; set; }
        public double Rating { get; set; }
    }
}
