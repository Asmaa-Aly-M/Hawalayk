using Hawalayk_APP.DataTransferObject;
using Hawalayk_APP.Models;

namespace Hawalayk_APP.Services
{
    public interface IServiceRequestRepository
    {
        Task<int> countService();
        Task<ServiceRequestSendDTO> GetServiceRequestSend(int id);
        Task<int> CreateAsync(string customerId, ServiceRequestDTO newservice);
        Task<int> Delete(int id);
        Task<List<ServiceRequest>> GetAll();
        Task<ServiceRequest> GetById(int id);
    }
}