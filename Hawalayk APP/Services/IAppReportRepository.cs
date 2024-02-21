using Hawalayk_APP.Models;

namespace Hawalayk_APP.Services
{
    public interface IAppReportRepository
    {
        int Create(AppReport appreport);
        int Delete(Customer customer);
        List<AppReport> GetAll();
        AppReport GetById(int id);
        int Update(int id, AppReport appreport);
    }
}