namespace Hawalayk_APP.model
{
    public class AppReport
    {
        public int Id { get; set; }
        public User Reporter { get; set; }
        public ReportedIssue ReportedIssue { get; set; }
        public string Description { get; set; }
        public DateTime DatePosted { get; set; }
    }
}
