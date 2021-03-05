using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EmergencyCall.Core.Services;
using EmergencyCall.Entities;
using AutoMapper;
using EmergencyCall.Api.Validators;
using EmergencyCall.Api.DTO;
using EmergencyCall.Services.Helpers;
using EmergencyCall.Api.DTO.HelpRequestDTO;
using Microsoft.AspNetCore.Authorization;

namespace EmergencyCall.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(Roles = "user")]
    public class HelpRequestController : ControllerBase
    {
        private readonly IHelpRequestService _helpRequestService;
        private readonly IMapper _mapper;
        public HelpRequestController(IHelpRequestService helpRequestService, IMapper mapper)
        {
            this._helpRequestService = helpRequestService;
            this._mapper = mapper;
        }

        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<HelpRequestDTO>>> GetAllHelpRequests()
        {
            var helpRequests = await _helpRequestService.GetAllHelpRequests();
            var helpRequestsResources = _mapper.Map<IEnumerable<HelpRequest>, IEnumerable<HelpRequestDTO>>(helpRequests);

            return Ok(helpRequestsResources);
        }

        [HttpPost("")]
        public async Task<ActionResult<HelpRequestDTO>> CreateHelpRequest([FromBody] CreateHelpRequestDTO createHelpRequestResource)
        {
            var validator = new CreateHelpRequestResourceValidator();
            var validationResult = await validator.ValidateAsync(createHelpRequestResource);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors); // this needs refining, but for demo it is ok

            var helpRequestToCreate = _mapper.Map<CreateHelpRequestDTO, HelpRequest>(createHelpRequestResource);
            var newHelpRequest = await _helpRequestService.CreateHelpRequest(helpRequestToCreate);

            var helpRequest = await _helpRequestService.GetHelpRequestById(newHelpRequest.Id);

            var helpRequestResource = _mapper.Map<HelpRequest, HelpRequestDTO>(helpRequest);

            return Ok(helpRequestResource);
        }
    }
}