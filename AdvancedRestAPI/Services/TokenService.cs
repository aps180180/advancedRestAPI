using AdvancedRestAPI.DTOs;
using AdvancedRestAPI.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AdvancedRestAPI.Services
{
    public class TokenService
    {
        private readonly JwtSettings _jwtSettings;
        public TokenService(IOptions<JwtSettings> jwtSettings)
        {
            _jwtSettings = jwtSettings.Value;                
        }
        public string Create(UserDTO userDTO)
        {
                      
            var handler = new JwtSecurityTokenHandler();

            var key = Encoding.UTF8.GetBytes(_jwtSettings.PrivateKey);  
            
            var credentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                 SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                SigningCredentials = credentials,
                Expires = DateTime.UtcNow.AddHours(2),
                Subject = GenerateClaims(userDTO)


            };

            var token = handler.CreateToken(tokenDescriptor);
            return handler.WriteToken(token);

        }

        private static ClaimsIdentity GenerateClaims(UserDTO userDTO)
        {
            var ci = new ClaimsIdentity();
            ci.AddClaim(new Claim("id", userDTO.Id.ToString()));
            ci.AddClaim(new Claim(ClaimTypes.Name, userDTO.Email));
            ci.AddClaim(new Claim(ClaimTypes.Email, userDTO.Email));
            ci.AddClaim(new Claim(ClaimTypes.GivenName, userDTO.Email));

            foreach (var role in userDTO.Roles)
            {
                ci.AddClaim(new Claim(ClaimTypes.Role, role));
            }
            return ci;
        }
    }
}
