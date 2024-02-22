using Hawalayk_APP.Context;
using Hawalayk_APP.Models;

namespace Hawalayk_APP.Services
{
    public class ServiceRequestRepository : IServiceRequestRepository
    {
        ApplicationDbContext Context;
        public ServiceRequestRepository(ApplicationDbContext _Context)
        {
            Context = _Context;
        }




        public ServiceRequest GetById(int id)
        {
            ServiceRequest service = Context.ServiceRequests.FirstOrDefault(s => s.Id == id);
            return service;
        }
        public List<ServiceRequest> GetAll()
        {
            return Context.ServiceRequests.ToList();
        }

        public int Create(ServiceRequest newservice)
        {
            Context.ServiceRequests.Add(newservice);
            int row = Context.SaveChanges();
            return row;
        }
        public int Update(int id, ServiceRequest newservice)////////هل محتاجينها؟؟؟
        {
            ServiceRequest Oldservice = Context.ServiceRequests.FirstOrDefault(s => s.Id == id);
            Oldservice.Content = newservice.Content;
            Oldservice.Image = newservice.Image;
            Oldservice.DatePosted = newservice.DatePosted;

            int row = Context.SaveChanges();
            return row;
        }
        public int Delete(int id)
        {
            ServiceRequest Oldservice = Context.ServiceRequests.FirstOrDefault(s => s.Id == id);
            Context.ServiceRequests.Remove(Oldservice);
            int row = Context.SaveChanges();
            return row;
        }
    }
}
