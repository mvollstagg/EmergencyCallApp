using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using EmergencyCall.Api.Models;
using EmergencyCall.Core.Services;
using EmergencyCall.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using EmergencyCall.Api.DTO.UserDTO;
using EmergencyCall.Api.Validators;
using AutoMapper;
using EmergencyCall.Services.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace EmergencyCall.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        private readonly ApiContext _context;
        

        public LoginController(ApiContext context,IUserService userService, IConfiguration config, IMapper mapper)
        {
            _context = context;
            this._userService = userService;
            this._config = config;
            this._mapper = mapper;
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
        
        [HttpPost]
        public async Task<IActionResult> Login(UserLoginModel user)
        {
            try
            {
                IActionResult response = Unauthorized();

                var data = await _context.Users
               .FirstOrDefaultAsync(x => x.Email == user.Email.Trim().ToLower());

                if (user != null)
                {
                    if (HashHelper.VerifyPasswordHash(user.Password, data.SecretKey, data.PasswordHash))
                    {
                        var StrToken = GenerateJSONWebToken(data);
                        response = Ok(new
                        {
                            token = StrToken,
                            message = "Giriş başarılı"
                        });
                        return response;
                    }
                }
                return BadRequest();

            }
            catch (Exception ex)
            {
                // LogsServices.Log("Login - GenerateJSONWebToken", ex.Message.ToString(), 3);
                return BadRequest();
            }
            
        }
        //
        [ApiExplorerSettings(IgnoreApi = true)]//swagger tarafından görüllmesini istemidiğimiz fonksiyonlara yazılmalıdır.
        private string GenerateJSONWebToken(User user)
        {
            try
            {
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                var claims = new []
                {
                    new Claim(ClaimTypes.Actor, user.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.Sub, user.FullName),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(ClaimTypes.Role, "user"),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                var token = new JwtSecurityToken(
                    issuer: _config["Jwt:Issuer"],
                    audience: _config["Jwt:Issuer"],
                    claims,
                    expires: DateTime.Now.AddDays(120),
                    signingCredentials: credentials);

                var encodetoken = new JwtSecurityTokenHandler().WriteToken(token);
                return encodetoken;
            }
            catch (Exception ex)
            {
                // LogsServices.Log("Login - GenerateJSONWebToken", ex.Message.ToString(), 3);
                return ex.ToString();
            }
           
        }
    }
}