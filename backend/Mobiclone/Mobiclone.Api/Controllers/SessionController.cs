using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mobiclone.Api.Lib;
using Mobiclone.Api.ViewModels;
using Mobiclone.Api.ViewModels.Session;
using System.Threading.Tasks;

namespace Mobiclone.Api.Controllers
{
    [ApiController]
    [Route("session")]
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseViewModel<string>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Store([FromBody] StoreSessionViewModel viewModel)
        {
            var token = await _auth.Attempt(viewModel.Email, viewModel.Password);

            if (token == null)
            {
                return Unauthorized();
            }

            var response = new ResponseViewModel<string>(token);

            return Ok(response);
        }
    }
}
