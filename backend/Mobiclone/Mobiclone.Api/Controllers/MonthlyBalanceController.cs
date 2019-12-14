using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mobiclone.Api.Database;
using Mobiclone.Api.Lib;
using Mobiclone.Api.ViewModels;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Mobiclone.Api.Controllers
{
    [ApiController]
    [Route("/monthly-balance")]
    [Authorize]
    public class MonthlyBalanceController : Controller
    {
        private readonly MobicloneContext _context;

        private readonly IAuth _auth;

        public MonthlyBalanceController(MobicloneContext context, IAuth auth)
        {
            _context = context;
            _auth = auth;
        }

        [HttpGet]
        [Route("")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Show(DateTime start, DateTime end)
        {
            var user = await _auth.User();

            var revenues = (from current in _context.Revenues
                            join accounts in _context.Accounts on current.AccountId equals accounts.Id
                            join users in _context.Users on accounts.UserId equals users.Id
                            where users.Id == user.Id && current.Date >= start && current.Date <= end
                            select current.Value).Sum();

            var expenses = (from current in _context.Expenses
                            join accounts in _context.Accounts on current.AccountId equals accounts.Id
                            join users in _context.Users on accounts.UserId equals users.Id
                            where user.Id == user.Id && current.Date >= start && current.Date <= end
                            select current.Value).Sum();

            var monthlyBalance = revenues + expenses;

            var response = new ResponseViewModel<int>(monthlyBalance);

            return Ok(response);
        }
    }
}
