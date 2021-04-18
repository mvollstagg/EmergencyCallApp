using EmergencyCall.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmergencyCall.Api
{
    public class ApiContext:DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<HelpRequest> HelpRequests { get; set; }
        public DbSet<HelpResponse> HelpResponses { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserLocationLog> UserLocationLogs { get; set; }
    }
}
