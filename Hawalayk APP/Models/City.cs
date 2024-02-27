using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hawalayk_APP.Models
{
    public class City
    {
        public int Id { get; set; }
        public string CityNameAR { get; set; }
        public string CityNameEN { get; set; }
        public Governorate? Governorate { get; set; }
        [ForeignKey("Governorate")]
        public int? GovernorateId { get; set; }


    }
}