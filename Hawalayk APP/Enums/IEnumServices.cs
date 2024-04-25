namespace Hawalayk_APP.Enums
{
    public interface IEnumServices
    {
        Task<ReportedIssue> getReportedIssue(string reportedIssue);
    }
}
