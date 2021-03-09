using System.Collections.Generic;
using System.Threading.Tasks;
using EmergencyCall.Entities;

namespace EmergencyCall.Core.Services
{
    public interface IHelpResponseService
    {
        Task<HelpResponse> CreateHelpResponse(HelpResponse newHelpResponse);
        Task DeleteHelpResponse(HelpResponse helpResponse);
    }
}