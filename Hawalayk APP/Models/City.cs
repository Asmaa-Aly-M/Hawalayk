using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hawalayk_APP.Models
{
    public class City
    {
        [key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }
        public string city_name_ar { get; set; }
        public string city_name_en { get; set; }
        public Governorate? Governorate { get; set; }
        [ForeignKey("Governorate")]
        public int? governorate_id { get; set; }

    }
}