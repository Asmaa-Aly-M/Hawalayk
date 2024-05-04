using System.ComponentModel.DataAnnotations.Schema;
namespace Hawalayk_APP.Models
{
    public class Ban
    {
        public int Id { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public DateTime BanStartTime { get; set; } = DateTime.UtcNow;//5/2/2024 12:29:02 PM
        public int BanDurationInMinutes { get; set; } // 
    }
}
