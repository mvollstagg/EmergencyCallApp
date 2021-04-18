using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EmergencyCall.Core.Services;
using EmergencyCall.Entities;
using AutoMapper;
using EmergencyCall.Api.DTO.UserDTO;
using EmergencyCall.Api.Validators;
using EmergencyCall.Api.DTO;
using EmergencyCall.Services.Helpers;
using Microsoft.AspNetCore.Authorization;
using EmergencyCall.Api.Models;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace EmergencyCall.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme, Roles = "user")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly LoginUser loginUser;
        private readonly ApiContext _context;

        public UsersController(ApiContext context, IUserService userService, IMapper mapper, IHttpContextAccessor httpContext)
        {
            _context = context;
            this._userService = userService;
            this._mapper = mapper;

            var token = httpContext.HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if (token != null)
            {
                JwtDecoder jwtHelper = new JwtDecoder();
                loginUser = jwtHelper.DecodeJwt(token);
            }
        }

        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetAllUsers()
        {
            var users = await _userService.GetAllUsers();
            var usersResources = _mapper.Map<IEnumerable<User>, IEnumerable<UserDTO>>(users);

            return Ok(usersResources);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> GetUserById(int id)
        {
            var user = await _userService.GetUserById(id);
            var userResource = _mapper.Map<User, UserDTO>(user);

            return Ok(userResource);
        }

        [HttpGet("")]
        public async Task<ActionResult<UserDTO>> GetCurrentUser()
        {
            var user = await _userService.GetUserById(loginUser.Id);
            return Ok(user);
        }

        [HttpPost("")]
        public async Task<ActionResult<UserDTO>> UpdateLocation(double lat,double lon)
        {
           int userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Actor))?.Value);
            var oldUser = await _userService.GetUserById(userId);

            if (oldUser == null)
                return NotFound();
            var newUser = oldUser;
            newUser.Latitude = lat;
            newUser.Longtitude = lon;

            await _userService.UpdateUser(newUser, oldUser);

            var updatedUser = await _userService.GetUserById(userId);

            var updatedUserResource = _mapper.Map<User, UserDTO>(updatedUser);
            var log = new UserLocationLog();
            log.UserId = userId;
            log.Latitude = lat;
            log.Longtitude = lon;
            _context.UserLocationLogs.Add(log);
            _context.SaveChanges();

            return Ok(updatedUserResource);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<UserDTO>> UpdateUser(int id, [FromBody] SaveUserDTO saveUserResource)
        {
            /*
                TODO Validator yazılacak
            */
            // var validator = new CreateUserResourceValidator();
            // var validationResult = await validator.ValidateAsync(saveUserResource);

            // if (!validationResult.IsValid)
            //     return BadRequest(validationResult.Errors); // this needs refining, but for demo it is ok

            var userToBeUpdated = await _userService.GetUserById(id);

            if (userToBeUpdated == null)
                return NotFound();

            var user = _mapper.Map<SaveUserDTO, User>(saveUserResource);
            if(!String.IsNullOrEmpty(saveUserResource.Password) && saveUserResource.Password == saveUserResource.PasswordConfirm)
            {
                user.PasswordHash = HashHelper.CreatePasswordHash(saveUserResource.Password, userToBeUpdated.SecretKey);
            }
            
            await _userService.UpdateUser(userToBeUpdated, user);

            var updatedUser = await _userService.GetUserById(id);

            var updatedUserResource = _mapper.Map<User, UserDTO>(updatedUser);

            return Ok(updatedUserResource);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _userService.GetUserById(id);

            await _userService.DeleteUser(user);

            return NoContent();
        }
    }
}