using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mobiclone.Api.Database;
using Mobiclone.Api.Lib;
using Mobiclone.Api.Models;
using Mobiclone.Api.ViewModels;
using Mobiclone.Api.ViewModels.Expense;
using System.Linq;
using System.Threading.Tasks;

namespace Mobiclone.Api.Controllers
{
    [ApiController]
    [Route("account/{accountId}/expense")]
    [Authorize]
    public class ExpenseController : Controller
    {
        private readonly MobicloneContext _context;

        private readonly IAuth _auth;

        public ExpenseController(MobicloneContext context, IAuth auth)
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
        public async Task<IActionResult> Store(int accountId, StoreExpenseViewModel viewModel)
        {
            var user = await _auth.User();

            var account = await (from current in _context.Accounts
                                 where current.Id == accountId && current.UserId == user.Id
                                 select current).FirstAsync();

            var expense = new Expense
            {
                Description = viewModel.Description,
                Value = viewModel.Value,
                Date = viewModel.Date,
                Account = account
            };

            await _context.Expenses.AddAsync(expense);

            await _context.SaveChangesAsync();

            var response = new ResponseViewModel<int>(expense.Id);

            return Ok(response);
        }
    }
}
