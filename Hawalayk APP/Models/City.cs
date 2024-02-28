using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hawalayk_APP.Models
{
    [Table("cities")]
    public class City
    {
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        [Column("city_name_ar")]
        public string CityNameAR { get; set; }
        [Column("city_name_en")]
        public string CityNameEN { get; set; }
        public Governorate? Governorate { get; set; }
        [ForeignKey("Governorate")]
        [Column("governorate_id")]
        public int? GovernorateId { get; set; }

    }
}