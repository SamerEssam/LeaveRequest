using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace WebApplication5.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public virtual DbSet<Request> Requests { get; set; }
        public virtual DbSet<VacationType> VacationTypes { get; set; }
        public virtual DbSet<RequestStatus> RequestStatuses { get; set; }
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }


        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Request>()
             .HasRequired<ApplicationUser>(s => s.Requester)
             .WithMany(g => g.UserRequests);

            base.OnModelCreating(modelBuilder);
        }
    }
}