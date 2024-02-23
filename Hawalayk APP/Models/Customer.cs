using System.ComponentModel.DataAnnotations.Schema;

namespace Hawalayk_APP.Models
{
     


    public class Customer :ApplicationUser
    {

       public ICollection<ServiceRequest> ServiceRequests;

        public ICollection<Review> Reviews;

      

    }
}
