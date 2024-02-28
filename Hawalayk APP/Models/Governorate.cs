using System.ComponentModel.DataAnnotations.Schema;

namespace Hawalayk_APP.Models
{
    [Table("governorates")]
    public class Governorate
    {
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        [Column("governorate_name_ar")]
        public string GovernorateNameAR { get; set; }
        [Column("governorate_name_en")]
        public string GovernorateNameEN { get; set; }
        public ICollection<City> Cities { get; set; }

    }
}
