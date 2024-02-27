namespace Hawalayk_APP.Models
{
    public class Governorate
    {
        public int Id { get; set; }
        public string GovernorateNameAR { get; set; }
        public string GovernorateNameEN { get; set; }
        public ICollection<City> Cities { get; set; }

    }
}
