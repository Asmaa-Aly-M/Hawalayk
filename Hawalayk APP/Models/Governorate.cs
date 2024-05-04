using System.ComponentModel.DataAnnotations.Schema;

namespace Hawalayk_APP.Models
{
    public class Governorate
    {
        [key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]

        public int id { get; set; }
        public string governorate_name_ar { get; set; }
        public string governorate_name_en { get; set; }
        public ICollection<City> Cities { get; set; }
    }
}
