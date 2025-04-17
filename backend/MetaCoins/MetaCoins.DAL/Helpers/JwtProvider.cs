using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MetaCoins.Core.Entities.Identity;
using MetaCoins.Core.Interfaces.Auth;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace MetaCoins.DAL.Helpers
{
    public class JwtProvider : IJwtProvider
    {
        private readonly JwtConfiguration _configuration;
        public JwtProvider(IOptions<JwtConfiguration> configuration)
        {
            _configuration = configuration.Value;
        }
        public string GenerateToken(UserEntity user, IList<string> roles)
        {
            var claims = new List<Claim>
            {
                new Claim("userId", user.Id.ToString())
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.SecretKey));

            var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                issuer: _configuration.Issuer,
                audience: _configuration.Audience,
                signingCredentials: signingCredentials,
                expires: DateTime.UtcNow.AddHours(_configuration.ExpiresHours)
            );

            var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenValue;
        }
    }
}