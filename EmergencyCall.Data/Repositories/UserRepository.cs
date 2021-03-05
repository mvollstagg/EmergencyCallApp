using Microsoft.EntityFrameworkCore;
using EmergencyCall.Entities;
using EmergencyCall.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EmergencyCall.Data.DAL;
using EmergencyCall.Services.Helpers;

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

        public async Task<User> UserLogin(string email, string password)
        {
            var user = await ApplicationDbContext.Users
                .FirstOrDefaultAsync(x => x.Email == email.Trim().ToLower());
                                                        
            if(user != null)
            {
                if (HashHelper.VerifyPasswordHash(password, user.SecretKey, user.PasswordHash))
                {
                    return user;
                }
            }
            return null;
        }

        public ApplicationDbContext ApplicationDbContext
        {
            get { return _context as ApplicationDbContext; }
        }
    }
}