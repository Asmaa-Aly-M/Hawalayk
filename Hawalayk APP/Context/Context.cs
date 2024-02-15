using Hawalayk_APP.model;
using Hawalayk_APP.Model;
using Microsoft.EntityFrameworkCore;

namespace Hawalayk_APP.Context
{
    public class Context:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=ZSC\\SQLEXPRESS;Database=Hawalayk;Trust-Connection=True;TrustCertificate=True");
            base.OnConfiguring(optionsBuilder);
        }
        public virtual DbSet<Address>Addresses { get; set; }
        public virtual DbSet<Advertisement> Advertisements { get; set; }
        public virtual DbSet<AppReport> AppReports { get; set; }
        public virtual DbSet<Craft> Crafts { get; set; }
        public virtual DbSet<JobApplication> JobApplications{ get; set; }
        public virtual DbSet<Post>Posts{ get; set; }
        public virtual DbSet<Review>Reviews{ get; set; }
        public virtual DbSet<ServiceRequest> ServiceRequests { get; set; }
        public virtual DbSet<UserReport>UserReports{ get; set; }


    }
}
