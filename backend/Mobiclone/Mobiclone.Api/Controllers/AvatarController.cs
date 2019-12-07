using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mobiclone.Api.Database;
using Mobiclone.Api.Lib;
using Mobiclone.Api.Models;
using Mobiclone.Api.ViewModels;
using System.Threading.Tasks;

namespace Mobiclone.Api.Controllers
{
    [ApiController]
    [Route("user/avatar")]
    [Authorize]
    public class AvatarController : Controller
    {
        private readonly MobicloneContext _context;

        private readonly IAuth _auth;

        private readonly IStorage _storage;

        public AvatarController(MobicloneContext context, IAuth auth, IStorage storage)
        {
            _context = context;
            _auth = auth;
            _storage = storage;
        }

        [HttpPost]
        [Route("")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Store(IFormFile formFile)
        {
            var user = await _auth.User();

            var path = await _storage.Move(formFile);

            var file = new File
            {
                Name = formFile.FileName,
                Path = path
            };

            await _context.Files.AddAsync(file);

            user.File = file;

            await _context.SaveChangesAsync();

            var response = new ResponseViewModel<string>(path);

            return Ok(response);
        }
    }
}
