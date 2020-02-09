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
    [Route("monthly-revenue")]
    [Authorize]
    public class MonthlyRevenueController : Controller
    {
        private readonly MobicloneContext _context;

        private readonly IAuth _auth;

        public MonthlyRevenueController(MobicloneContext context, IAuth auth)
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
        public async Task<IActionResult> Show([FromQuery] DateTime start, [FromQuery] DateTime end)
        {
            var user = await _auth.User();

            var revenues = await (from revenue in _context.Revenues
                                  where revenue.Account.User == user && revenue.Date >= start && revenue.Date <= end
                                  select revenue.Value).SumAsync();

            var response = new ResponseViewModel<int>(revenues);

            return Ok(response);
        }
    }
}
