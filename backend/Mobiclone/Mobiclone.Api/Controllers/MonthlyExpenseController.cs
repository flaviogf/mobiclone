using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mobiclone.Api.Database;
using Mobiclone.Api.Lib;
using Mobiclone.Api.ViewModels;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Mobiclone.Api.Controllers
{
    [ApiController]
    [Route("monthly-expense")]
    [Authorize]
    public class MonthlyExpenseController : Controller
    {
        private readonly MobicloneContext _context;

        private readonly IAuth _auth;

        public MonthlyExpenseController(MobicloneContext context, IAuth auth)
        {
            _context = context;
            _auth = auth;
        }

        [HttpGet]
        [Route("")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseViewModel<int>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Show(DateTime start, DateTime end)
        {
            var user = await _auth.User();

            var expenses = await (from expense in _context.Expenses
                                  where expense.Account.User == user && expense.Date >= start && expense.Date <= end
                                  select expense.Value).SumAsync();

            var response = new ResponseViewModel<int>(expenses);

            return Ok(response);
        }
    }
}
