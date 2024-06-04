using Hawalayk_APP.Enums;

namespace Hawalayk_APP.Services
{
    public class EnumRepository : IEnumRepository
    {
        public async Task<ReportedIssue> getReportedIssue(string reportedIssue)
        {

            ReportedIssue Reported_Issue;

            if (Enum.TryParse(reportedIssue, out Reported_Issue))
            {
                if (Reported_Issue != null) return Reported_Issue;
            }//parsing : enum 
            return ReportedIssue.BUG;

        }

    }

}
