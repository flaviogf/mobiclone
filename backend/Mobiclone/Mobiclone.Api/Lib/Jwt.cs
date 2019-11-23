using Microsoft.IdentityModel.Tokens;
using Mobiclone.Api.Database;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Mobiclone.Api.Lib
{
    public class Jwt : IAuth
    {
        private readonly MobicloneContext _context;

        private readonly IHash _hash;

        private readonly string _issuer;

        private readonly string _audience;

        private readonly string _secret;

        public Jwt(MobicloneContext context, IHash hash, string issuer, string audience, string secret)
        {
            _context = context;
            _hash = hash;
            _issuer = issuer;
            _audience = audience;
            _secret = secret;
        }

        public async Task<string> Attempt(string email, string password)
        {
            var user = _context.Users.FirstOrDefault(it => it.Email == email);

            if (user == null || !(await _hash.Verify(password, user.PasswordHash)))
            {
                return null;
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secret));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString())
            };

            var token = new JwtSecurityToken(issuer: _issuer, audience: _audience, claims: claims, signingCredentials: credentials);

            var handler = new JwtSecurityTokenHandler();

            var raw = handler.WriteToken(token);

            return raw;
        }
    }
}
