using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;

namespace EmergencyCall.Api.Models
{
    public class JwtDecoder
    {
        public LoginUser DecodeJwt(string jwt)
        {
            var token = new JwtSecurityToken(jwtEncodedString: jwt);
            var loginUser = new LoginUser()
            {
                Id = Convert.ToInt32(token.Claims.Where(x => x.Type == ClaimTypes.Actor).FirstOrDefault().Value),
                Role = token.Claims.Where(x => x.Type == ClaimTypes.Role).FirstOrDefault().Value,
                Email = token.Claims.Where(x => x.Type == ClaimTypes.Email).FirstOrDefault().Value
            };
            return loginUser;
        }
    }
}