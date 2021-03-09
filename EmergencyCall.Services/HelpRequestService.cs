using EmergencyCall.Core;
using EmergencyCall.Entities;
using EmergencyCall.Core.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EmergencyCall.Services
{
    public class HelpRequestService : IHelpRequestService
    {
        private readonly IUnitOfWork _unitOfWork;
        public HelpRequestService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<HelpRequest> CreateHelpRequest(HelpRequest newHelpRequest)
        {
            await _unitOfWork.HelpRequests.AddAsync(newHelpRequest);
            await _unitOfWork.CommitAsync();
            return newHelpRequest;
        }


        public async Task DeleteHelpRequest(HelpRequest helpRequest)
        {
            _unitOfWork.HelpRequests.Remove(helpRequest);
            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<HelpRequest>> GetAllHelpRequests()
        {
            return await _unitOfWork.HelpRequests.GetAllHelpRequests();
        }

        public async Task<HelpRequest> GetHelpRequestById(int id)
        {
            return await _unitOfWork.HelpRequests.GetByIdAsync(id);
        }

        public async Task UpdateHelpRequest(HelpRequest helpRequestToBeUpdated, HelpRequest helpRequest)
        {
            helpRequestToBeUpdated = helpRequest;
            await _unitOfWork.CommitAsync();
        }
    }
}