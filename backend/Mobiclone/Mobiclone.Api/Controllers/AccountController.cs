using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mobiclone.Api.Database;
using Mobiclone.Api.Lib;
using Mobiclone.Api.Models;
using Mobiclone.Api.ViewModels;
using Mobiclone.Api.ViewModels.Account;
using System.Collections.Generic;
using System.Linq;
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

        [HttpGet]
        [Route("")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Index()
        {
            var user = await _auth.User();

            var accounts = from account in _context.Accounts
                           where account.UserId == user.Id
                           select account;

            var response = new ResponseViewModel<List<Account>>(accounts.ToList());

            return Ok(response);
        }

        [HttpGet]
        [Route("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Show([FromRoute] int id)
        {
            var user = await _auth.User();

            var account = await (from current in _context.Accounts
                                 where current.Id == id && current.UserId == user.Id
                                 select current).FirstAsync();

            var response = new ResponseViewModel<Account>(account);

            return Ok(response);
        }

        [HttpPut]
        [Route("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateAccountViewModel viewModel)
        {
            var user = await _auth.User();

            var account = await (from current in _context.Accounts
                                 where current.Id == id && current.UserId == user.Id
                                 select current).FirstAsync();

            account.Name = viewModel.Name;
            account.Type = viewModel.Type;

            await _context.SaveChangesAsync();

            var response = new ResponseViewModel<int>(account.Id);

            return Ok(response);
        }

        [HttpDelete]
        [Route("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Destroy([FromRoute] int id)
        {
            var user = await _auth.User();

            var account = await (from current in _context.Accounts
                                 where current.Id == id && current.UserId == user.Id
                                 select current).FirstAsync();

            _context.Remove(account);

            await _context.SaveChangesAsync();

            var response = new ResponseViewModel<int>(account.Id);

            return Ok(response);
        }
    }
}
