using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mobiclone.Api.Database;
using Mobiclone.Api.Lib;
using Mobiclone.Api.Models;
using Mobiclone.Api.ViewModels;
using Mobiclone.Api.ViewModels.User;

namespace Mobiclone.Api.Controllers
{
    [ApiController]
    [Route("user")]
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
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ResponseViewModel<int>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Store([FromBody] StoreUserViewModel viewModel)
        {
            var user = new User
            {
                Name = viewModel.Name,
                Email = viewModel.Email,
                PasswordHash = await _hash.Make(viewModel.Password)
            };

            await _context.Users.AddAsync(user);

            await _context.SaveChangesAsync();

            var response = new ResponseViewModel<int>(user.Id);

            return Created($"/user/{user.Id}", response);
        }
    }
}
