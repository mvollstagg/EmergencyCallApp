using EmergencyCall.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace EmergencyCall.Data.DAL
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            //optionsBuilder.UseSqlServer("Server=emergencyapp.mssql.somee.com;Database=emergencyapp;User Id=emergencyapp_SQLLogin_1;Password=spbikjfh4m;MultipleActiveResultSets=true");

            optionsBuilder.UseSqlServer("Server=MALI-PC;Database=emergencyapp;User Id=sa;Password=1234;MultipleActiveResultSets=true");
            return new ApplicationDbContext(optionsBuilder.Options);
        }
        
    }
}