using Hawalayk_APP.DataTransferObject;
using Hawalayk_APP.Models;

namespace Hawalayk_APP.Services
{
    public interface IAppReportRepository
    {
        int Create(string id, AppReportDTO appreport);
        int Delete(AppReport appreport);
        List<AppReport> GetAll();
        AppReport GetById(int id);
        int Update(int id, AppReport appreport);
    }
}