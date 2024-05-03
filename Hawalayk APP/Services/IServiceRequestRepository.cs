using Hawalayk_APP.DataTransferObject;
using Hawalayk_APP.Models;

namespace Hawalayk_APP.Services
{
    public interface IServiceRequestRepository
    {
        int countService();
        Task<int> CreateAsync(string customerId, ServiceRequestDTO newservice);
        int Delete(int id);
        List<ServiceRequest> GetAll();
        ServiceRequest GetById(int id);
    }
}