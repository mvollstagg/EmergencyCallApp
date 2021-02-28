using System.Threading.Tasks;
using EmergencyCall.Core;
using EmergencyCall.Core.Repositories;
using EmergencyCall.Data.DAL;
using EmergencyCall.Data.Repositories;

namespace EmergencyCall.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private UserRepository _userRepository;
        private HelpRequestRepository _helpRequestRepository;

        public UnitOfWork(ApplicationDbContext context)
        {
            this._context = context;
        }

        public IUserRepository Users => _userRepository = _userRepository ?? new UserRepository(_context);
        public IHelpRequestRepository HelpRequests => _helpRequestRepository = _helpRequestRepository ?? new HelpRequestRepository(_context);

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}