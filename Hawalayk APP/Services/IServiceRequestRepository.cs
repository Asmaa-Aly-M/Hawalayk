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
        double GetPercentageOfUsersMakingRequestsToday();
        Task<int> CreateAsync(string craftName, string customerId, ServiceRequestDTO newservice);
        Task<int> Delete(int id);
        Task<List<ServiceRequest>> GetAll();
        Task<ServiceRequest> GetById(int id);
        List<RequestForDashBord> GetLatestServiceRequests();
        Task<List<RequestAcceptedForCustomrDTO>> GetServiceRequestsAcceptedCraftsmenForCustomer(string customerID);
        Task<ServiceRequestSendDTO> GetServiceRequestSend(int id);
        Task<List<ServiceNeededRepalyForCustomerDTO>> GetServiceRequestsNeedToReplayByCraftsmenForCustomer(string customerId);
        Task<List<JobApplication>> getAllJopapplicatoinForAServicceRequest(int serviceId);
        Task<List<AvailableServiceRequestDTO>> GetAvailableServiceRequestsByCraft(CraftName craftName, string craftsmanId);
    }
}