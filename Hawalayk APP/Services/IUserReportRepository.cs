using Hawalayk_APP.DataTransferObject;
using Hawalayk_APP.Models;

namespace Hawalayk_APP.Services
{
    public interface IUserReportRepository
    {
        Task<int> Create(string id, string reportedUserId, UserReportDTO UserRepo);
        Task<int> Delete(int id);
        Task<List<UserReportForAdminDashBoard>> GetAllUserReports();
        Task<UserReport> GetById(int id);
        Task<int> Update(int id, UserReport UserRepo);
    }
}