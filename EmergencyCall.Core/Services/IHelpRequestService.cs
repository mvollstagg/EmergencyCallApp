using System.Collections.Generic;
using System.Threading.Tasks;
using EmergencyCall.Entities;

namespace EmergencyCall.Core.Services
{
    public interface IHelpRequestService
    {
        Task<IEnumerable<HelpRequest>> GetAllHelpRequests();
        Task<HelpRequest> GetHelpRequestById(int id);
        HelpRequest GetLastRequestByUser(int id);
        Task<HelpRequest> CreateHelpRequest(HelpRequest newHelpRequest);
        Task UpdateHelpRequest(HelpRequest helpRequestToBeUpdated, HelpRequest helpRequest);
        Task DeleteHelpRequest(HelpRequest helpRequest);
    }
}