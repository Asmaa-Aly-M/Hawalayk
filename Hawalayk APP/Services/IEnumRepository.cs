using Hawalayk_APP.Enums;

namespace Hawalayk_APP.Services
{
    public interface IEnumRepository
    {
        Task<ReportedIssue> getReportedIssue(string reportedIssue);
    }
}
