using Microsoft.IdentityModel.Tokens;
using Mobiclone.Api.Database;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Mobiclone.Api.Lib
{
    public class Jwt : IAuth
    {
        private readonly MobicloneContext _context;

        private readonly IHash _hash;

        private readonly string _issuer;

        private readonly string _audience;

        private readonly SecurityKey _secretKey;

        public Jwt(MobicloneContext context, IHash hash, string issuer, string audience, SecurityKey secretKey)
        {
            _context = context;
            _hash = hash;
            _issuer = issuer;
            _audience = audience;
            _secretKey = secretKey;
        }

        public async Task<string> Attempt(string email, string password)
        {
            var user = _context.Users.FirstOrDefault(it => it.Email == email);

            if (user == null || !(await _hash.Verify(password, user.PasswordHash)))
            {
                return null;
            }

            var credentials = new SigningCredentials(_secretKey, SecurityAlgorithms.HmacSha256);

            var claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString())
            };

            var token = new JwtSecurityToken(issuer: _issuer, audience: _audience, claims: claims, signingCredentials: credentials, expires: DateTime.Now.AddMinutes(1));

            var handler = new JwtSecurityTokenHandler();

            var raw = handler.WriteToken(token);

            return raw;
        }
    }
}
