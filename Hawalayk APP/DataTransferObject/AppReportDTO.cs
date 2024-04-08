using Hawalayk_APP.Enums;

namespace Hawalayk_APP.DataTransferObject
{
    public class AppReportDTO
    {
        public int Id { get; set; }
        public ReportedIssue ReportedIssue { get; set; }
        public string? Description { get; set; }
    }
}
