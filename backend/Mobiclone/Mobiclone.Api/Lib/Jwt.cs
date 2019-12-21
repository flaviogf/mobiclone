using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Mobiclone.Api.Database;
using Mobiclone.Api.Models;
using System;
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
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _accessor;

        public Jwt(MobicloneContext context, IHash hash, IConfiguration configuration, IHttpContextAccessor accessor)
        {
            _context = context;
            _hash = hash;
            _configuration = configuration;
            _accessor = accessor;
        }

        public async Task<string> Attempt(string email, string password)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Auth:Key"]));

            var issuer = _configuration["Auth:Issuer"];

            var audience = _configuration["Auth:Audience"];

            var user = await (from u in _context.Users
                              where u.Email == email
                              select u).FirstAsync();

            if (user == null || !(await _hash.Verify(password, user.PasswordHash)))
            {
                return null;
            }

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString())
            };

            var token = new JwtSecurityToken(issuer, audience, claims, signingCredentials: credentials, expires: DateTime.Now.AddMonths(1));

            var handler = new JwtSecurityTokenHandler();

            var raw = handler.WriteToken(token);

            return raw;
        }

        public async Task<User> User()
        {
            int.TryParse(_accessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value, out var id);

            var user = await (from u in _context.Users
                              .Include(it => it.File)
                              where u.Id == id
                              select u).FirstAsync();

            return user;
        }
    }
}
