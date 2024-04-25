using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hawalayk_APP.Models
{
    public class Address
    {
        public int Id { get; set; }
        public Governorate  Governorate { get; set; }
        [ForeignKey("Governorate")]
        public int? GovernorateId { get; set; }
        public City City { get; set; }
        [ForeignKey("City")]
        public int? CityId { get; set; }
        public string StreetName { get; set; }
    }
}
