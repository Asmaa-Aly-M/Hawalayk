using Hawalayk_APP.DataTransferObject;
using Hawalayk_APP.Models;

namespace Hawalayk_APP.Services
{
    public interface IAppReportRepository
    {
        Task<int> Create(string id, AppReportDTO appreport);
        Task<int> Delete(AppReport appreport);
        Task<List<AppReport>> GetAll();
        Task<AppReport> GetById(int id);
        Task<int> Update(int id, AppReport appreport);
    }
}