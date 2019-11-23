using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Mobiclone.Api.Lib
{
    public class JwtAuth : IAuth
    {
        private readonly string _secret;

        public JwtAuth(string secret)
        {
            _secret = secret;
        }

        public Task<string> Attempt(string email, string password)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secret));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, email)
            };

            var token = new JwtSecurityToken(issuer: "", audience: "", claims: claims, signingCredentials: credentials);

            var handler = new JwtSecurityTokenHandler();

            var raw = handler.WriteToken(token);

            return Task.FromResult(raw);
        }
    }
}
