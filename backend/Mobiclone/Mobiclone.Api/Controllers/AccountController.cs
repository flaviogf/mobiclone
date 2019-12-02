using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mobiclone.Api.Database;
using Mobiclone.Api.Lib;
using Mobiclone.Api.Models;
using Mobiclone.Api.ViewModels;
using Mobiclone.Api.ViewModels.Account;
using System.Threading.Tasks;

namespace Mobiclone.Api.Controllers
{
    [ApiController]
    [Route("account")]
    [Authorize]
    public class AccountController : Controller
    {
        private readonly MobicloneContext _context;

        private readonly IAuth _auth;

        public AccountController(MobicloneContext context, IAuth auth)
        {
            _context = context;
            _auth = auth;
        }

        [HttpPost]
        [Route("")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Store([FromBody]StoreAccountViewModel viewModel)
        {
            var user = await _auth.User();

            var account = new Account
            {
                Name = viewModel.Name,
                Type = viewModel.Type,
                User = user
            };

            await _context.Accounts.AddAsync(account);

            await _context.SaveChangesAsync();

            var response = new ResponseViewModel<int>(account.Id);

            return Created($"/account/{account.Id}", response);
        }
    }
}
