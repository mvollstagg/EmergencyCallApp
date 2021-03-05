using System.Collections.Generic;
using System.Threading.Tasks;
using EmergencyCall.Entities;

namespace EmergencyCall.Core.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> UserLogin(string email, string password);
    }
}