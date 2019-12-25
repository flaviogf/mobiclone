using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mobiclone.Api.Database;
using Mobiclone.Api.Lib;
using Mobiclone.Api.ViewModels;
using System.Linq;
using System.Threading.Tasks;

namespace Mobiclone.Api.Controllers
{
    [ApiController]
    [Route("/current-balance")]
    [Authorize]
    public class CurrentBalanceController : Controller
    {
        private readonly MobicloneContext _context;

        private readonly IAuth _auth;

        public CurrentBalanceController(MobicloneContext context, IAuth auth)
        {
            _context = context;
            _auth = auth;
        }

        [HttpGet]
        [Route("")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseViewModel<int>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Show()
        {
            var user = await _auth.User();

            var revenues = await (from current in _context.Revenues
                                  where current.Account.User == user
                                  select current.Value).SumAsync();

            var expenses = await (from current in _context.Expenses
                                  where current.Account.User == user
                                  select current.Value).SumAsync();

            var balance = revenues + expenses;

            var response = new ResponseViewModel<int>(balance);

            return Ok(response);
        }
    }
}
