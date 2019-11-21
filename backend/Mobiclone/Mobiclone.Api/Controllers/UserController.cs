using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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

        public UserController(MobicloneContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Store(StoreUserViewModel viewModel)
        {
            var user = new User
            {
                Name = viewModel.Name,
                Email = viewModel.Email,
                Password = await Hash.Make(viewModel.Password)
            };

            await _context.Users.AddAsync(user);

            await _context.SaveChangesAsync();

            return Ok(user.Id);
        }
    }
}
