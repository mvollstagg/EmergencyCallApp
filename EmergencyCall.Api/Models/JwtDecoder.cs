using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;

namespace EmergencyCall.Api.Models
{
    public class JwtDecoder
    {
        public LoginUser DecodeJwt(string jwt)
        {// Token içerisinde bir tek username var web tasa
            var token = new JwtSecurityToken(jwtEncodedString: jwt);
            var loginUser = new LoginUser()
            {
                Id = Convert.ToInt32(token.Claims.Where(x => x.Type == ClaimTypes.Actor).FirstOrDefault().Value),
                UserName = token.Claims.Where(x => x.Type == JwtRegisteredClaimNames.Sub).FirstOrDefault().Value,
                Role = token.Claims.Where(x => x.Type == ClaimTypes.Role).FirstOrDefault().Value,
                Email = token.Claims.Where(x => x.Type == JwtRegisteredClaimNames.Email).FirstOrDefault().Value
            };
            return loginUser;
        }
    }
}