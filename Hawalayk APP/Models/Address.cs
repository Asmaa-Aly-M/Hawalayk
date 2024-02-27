using Microsoft.EntityFrameworkCore;

namespace Hawalayk_APP.Models
{
    public class Address
    {
        public int Id { get; set; }
        public Governorate  Governorate { get; set; }
        public City City { get; set; }
        public string StreetName { get; set; }

    }
}
