using Hawalayk_APP.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Hawalayk_APP.Context
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public ApplicationDbContext()
        {

        }

        #region Tabels
        public virtual DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public virtual DbSet<Ban> Bans { get; set; }
        public virtual DbSet<Craftsman> Craftsmen { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<Advertisement> Advertisements { get; set; }
        public virtual DbSet<AppReport> AppReports { get; set; }
        public virtual DbSet<Craft> Crafts { get; set; }
        public virtual DbSet<JobApplication> JobApplications { get; set; }
        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<Review> Reviews { get; set; }
        public virtual DbSet<ServiceRequest> ServiceRequests { get; set; }
        public virtual DbSet<UserReport> UserReports { get; set; }
        public virtual DbSet<OTPToken> OTPTokens { get; set; }
        public virtual DbSet<Governorate> governorates { get; set; }
        public virtual DbSet<City> cities { get; set; }
        public virtual DbSet<Block> Blocks { get; set; }
        #endregion


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes()
       .SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Cascade;
            }



            modelBuilder.Entity<Customer>().ToTable("Customer");
            modelBuilder.Entity<Craftsman>().ToTable("CraftsMan");
            modelBuilder.Entity<Admin>().ToTable("Admin");

            modelBuilder.Entity<ApplicationUser>()
                .Property(u => u.Gender)
                .HasConversion<string>();

            modelBuilder.Entity<Craft>()
               .Property(c => c.Name)
               .HasConversion<string>();

            modelBuilder.Entity<AppReport>()
               .Property(r => r.ReportedIssue)
               .HasConversion<string>();

            modelBuilder.Entity<JobApplication>()
               .Property(a => a.ResponseStatus)
               .HasConversion<string>();





            base.OnModelCreating(modelBuilder);
        }



    }
}
