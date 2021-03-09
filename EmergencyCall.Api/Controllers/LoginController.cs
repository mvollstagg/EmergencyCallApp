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

namespace EmergencyCall.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        

        public LoginController(IUserService userService, IConfiguration config, IMapper mapper)
        {
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
                User data = await _userService.UserLogin(user.Email, user.Password);
                if (data!=null)
                {
                    var StrToken = GenerateJSONWebToken(data);
                        response = Ok( new
                        {
                            token = StrToken,
                            message = "Giriş başarılı"
                        });
                }
                return response;
            }
            catch (Exception ex)
            {
                // LogsServices.Log("Login - GenerateJSONWebToken", ex.Message.ToString(), 3);
                return null;
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
                    new Claim(JwtRegisteredClaimNames.Sub, user.FullName),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(ClaimTypes.Role, "user"),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                var token = new JwtSecurityToken(
                    issuer: _config["Jwt:Issuer"],
                    audience: _config["Jwt:Issuer"],
                    claims,
                    expires: DateTime.Now.AddMinutes(120),
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