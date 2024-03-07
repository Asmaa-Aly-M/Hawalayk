using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hawalayk_APP.Models
{
    public class Block
    {
        public int Id { get; set; }

        public ApplicationUser BlockingUser { get; set; }

        [ForeignKey("BlockingUser")]
        public string BlockingUserId { get; set; }

        public ApplicationUser BlockedUser { get; set; }

        [ForeignKey("BlockedUser")]
        public string BlockedUserId { get; set; }
    }
}
