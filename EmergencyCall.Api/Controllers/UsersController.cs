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

namespace EmergencyCall.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        public UsersController(IUserService userService, IMapper mapper)
        {
            this._userService = userService;
            this._mapper = mapper;
        }

        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetAllUsers()
        {
            var users = await _userService.GetAllUsers();
            var usersResources = _mapper.Map<IEnumerable<User>, IEnumerable<UserDTO>>(users);

            return Ok(usersResources);
        }

        [HttpPost("")]
        public async Task<ActionResult<UserDTO>> CreateUser([FromBody] CreateUserDTO createUserResource)
        {
            var validator = new CreateUserResourceValidator();
            var validationResult = await validator.ValidateAsync(createUserResource);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors); // this needs refining, but for demo it is ok

            var userToCreate = _mapper.Map<CreateUserDTO, User>(createUserResource);
            userToCreate.PasswordHash = HashHelper.CreatePasswordHash(createUserResource.Password, userToCreate.SecretKey);
            var newUser = await _userService.CreateUser(userToCreate);

            var user = await _userService.GetUserById(newUser.Id);

            var userResource = _mapper.Map<User, UserDTO>(user);

            return Ok(userResource);
        }
    }
}