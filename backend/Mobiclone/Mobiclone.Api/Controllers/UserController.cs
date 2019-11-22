using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Mobiclone.Api.Database;
using Mobiclone.Api.Lib;
using Mobiclone.Api.Models;
using Mobiclone.Api.ViewModels.User;

namespace Mobiclone.Api.Controllers
{
    [ApiController]
    [Route("{controller}")]
    public class UserController : Controller
    {
        private readonly MobicloneContext _context;

        private readonly IHash _hash;

        public UserController(MobicloneContext context, IHash hash)
        {
            _context = context;
            _hash = hash;
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Store(StoreUserViewModel viewModel)
        {
            var user = new User
            {
                Name = viewModel.Name,
                Email = viewModel.Email,
                PasswordHash = await _hash.Make(viewModel.Password)
            };

            await _context.Users.AddAsync(user);

            await _context.SaveChangesAsync();

            return Ok(new { Data = user.Id });
        }
    }
}
