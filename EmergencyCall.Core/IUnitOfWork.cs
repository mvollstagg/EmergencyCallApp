using System;
using System.Threading.Tasks;
using EmergencyCall.Core.Repositories;
namespace EmergencyCall.Core
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
        IHelpRequestRepository HelpRequests { get; }
        IHelpResponseRepository HelpResponses { get; }
        Task<int> CommitAsync();
    }
}