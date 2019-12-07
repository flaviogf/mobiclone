using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mobiclone.Api.Lib;
using Mobiclone.Api.ViewModels;
using System.Threading.Tasks;

namespace Mobiclone.Api.Controllers
{
    [ApiController]
    [Route("user/avatar")]
    public class AvatarController : Controller
    {
        private readonly IStorage _storage;

        public AvatarController(IStorage storage)
        {
            _storage = storage;
        }

        [HttpPost]
        [Route("")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Store(IFormFile file)
        {
            var path = await _storage.Move(file);

            var response = new ResponseViewModel<string>(path);

            return Ok(response);
        }
    }
}
