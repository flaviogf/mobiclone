using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mobiclone.Api.Lib;
using Mobiclone.Api.ViewModels.Session;
using System.Threading.Tasks;

namespace Mobiclone.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SessionController : Controller
    {
        private readonly IAuth _auth;

        public SessionController(IAuth auth)
        {
            _auth = auth;
        }

        [HttpPost]
        [Route("")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Store(StoreSessionViewModel viewModel)
        {
            var token = await _auth.Attempt(viewModel.Email, viewModel.Password);

            return Ok(new { Data = token });
        }
    }
}
