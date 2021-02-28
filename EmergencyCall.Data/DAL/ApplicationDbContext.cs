using EmergencyCall.Data.DAL;
using EmergencyCall.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmergencyCall.Data.DAL
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Seed();
        }

        public DbSet<HelpRequest> HelpRequests { get; set; }
        public DbSet<HelpResponse> HelpResponses { get; set; }
        public DbSet<User> Users { get; set; }
        
    }
}