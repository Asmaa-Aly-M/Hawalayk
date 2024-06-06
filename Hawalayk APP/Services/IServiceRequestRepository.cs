using Hawalayk_APP.DataTransferObject;
using Hawalayk_APP.Enums;
using Hawalayk_APP.Models;

namespace Hawalayk_APP.Services
{
    public interface IServiceRequestRepository
    {
        Task<int> countService();
        int CountUsersMakingRequestsLastMonth();
        int CountUsersMakingRequestsLastWeek();
        int CountUsersMakingRequestsToday();
        Task<int> CreateAsync(string customerId, ServiceRequestDTO newservice);
        Task<int> Delete(int id);
        Task<List<RequestAcceptedForCraftsmanDTO>> GetAcceptedServiceRequestsFromCustomersByACraftsman(string craftsmanID);
        Task<List<ServiceRequest>> GetAll();
        Task<ServiceRequest> GetById(int id);
        List<ServiceRequest> GetLatestServiceRequests();
        Task<List<RequestAcceptedForCustomrDTO>> GetServiceRequestsAcceptedCraftsmenForCustomer(string customerID);
        // Task<List<ServiceNeededRepalyDTO>> GetServiceRequestsAcceptedCraftsmenForCustomer(string customerId);
        Task<ServiceRequestSendDTO> GetServiceRequestSend(int id);
        Task<List<ServiceNeededRepalyForCustomerDTO>> GetServiceRequestsNeedToReplayByCraftsmenForCustomer(string customerId);
    }
}