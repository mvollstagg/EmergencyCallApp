using Microsoft.EntityFrameworkCore;
using EmergencyCall.Entities;
using EmergencyCall.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EmergencyCall.Data.DAL;

namespace EmergencyCall.Data.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context) { }
        
        public async Task<IEnumerable<User>> GetAllWithRoleAsync(string role)
        {
            return await ApplicationDbContext.Users
                // .Include(a => a.UserRoles)
                .ToListAsync();
        }

        public ApplicationDbContext ApplicationDbContext
        {
            get { return _context as ApplicationDbContext; }
        }
    }
}