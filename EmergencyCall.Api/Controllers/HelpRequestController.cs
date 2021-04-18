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
using System.Security.Claims;
using System.Drawing;
using Microsoft.EntityFrameworkCore;

namespace EmergencyCall.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme, Roles = "user")]
    public class HelpRequestController : ControllerBase
    {
        private readonly IHelpRequestService _helpRequestService;
        private readonly IMapper _mapper;
        private readonly ApiContext _context;
        public HelpRequestController(ApiContext context, IHelpRequestService helpRequestService, IMapper mapper)
        {
            _context = context;
            this._helpRequestService = helpRequestService;
            this._mapper = mapper;
        }

        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<HelpRequestDTO>>> GetAllHelpRequests( double lat, double lon)
        {
            var userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Actor))?.Value);

            var user =await _context.Users.FindAsync(userId);

            var helpRequests = await _context.HelpRequests
                .AsNoTracking()
                .Include(e=>e.User)
                .ToListAsync();
           var result = helpRequests.Select(e => new { 
               Model=e,
                Distance = DistanceTo(lat,lon,double.Parse(e.Latitude.ToString()),double.Parse(e.Longtitute.ToString()))
            })
                .OrderBy(e=>e.Distance)
                .ToList();
            result.RemoveAll(e => e.Distance > 15);
            foreach (var item in result)
            {
                item.Model.HelpResponses.Clear();
                item.Model.User.HelpResponses.Clear();
                item.Model.User.HelpRequests.Clear();
            }

            user.Latitude = lat;
            user.Longtitude = lon;
            _context.Users.Update(user);
            _context.SaveChanges();
            return Ok(result.Take(5));
        }
        private double DistanceTo(double lat1, double lon1, double lat2, double lon2)
        {
            double rlat1 = Math.PI * lat1 / 180;
            double rlat2 = Math.PI * lat2 / 180;
            double theta = lon1 - lon2;
            double rtheta = Math.PI * theta / 180;
            double dist =
                Math.Sin(rlat1) * Math.Sin(rlat2) + Math.Cos(rlat1) *
                Math.Cos(rlat2) * Math.Cos(rtheta);
            dist = Math.Acos(dist);
            dist = dist * 180 / Math.PI;
            dist = dist * 60 * 1.1515;

            return dist * 1.609344;


        }

        [HttpPost("")]
        public async Task<ActionResult<HelpRequestDTO>> CreateHelpRequest([FromBody] CreateHelpRequestDTO createHelpRequestResource)
        {
            //En yakýndakileri bul bildirim yolla
            //Claims userId
            createHelpRequestResource.UserId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Actor))?.Value);

            var tmp =  _helpRequestService.GetLastRequestByUser(createHelpRequestResource.UserId);
            if (tmp != null)
                return BadRequest();

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