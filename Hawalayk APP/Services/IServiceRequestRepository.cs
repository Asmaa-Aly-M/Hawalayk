using Hawalayk_APP.Models;

namespace Hawalayk_APP.Services
{
    public interface IServiceRequestRepository
    {
        int Create(ServiceRequest newservice);
        int Delete(int id);
        List<ServiceRequest> GetAll();
        ServiceRequest GetById(int id);
        int Update(int id, ServiceRequest newservice);
    }
}