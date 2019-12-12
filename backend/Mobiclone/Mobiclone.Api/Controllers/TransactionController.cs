using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mobiclone.Api.Database;
using Mobiclone.Api.Lib;
using Mobiclone.Api.Models;
using Mobiclone.Api.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Mobiclone.Api.Controllers
{
    public class TransactionController : Controller
    {
        private readonly MobicloneContext _context;

        private readonly IAuth _auth;

        public TransactionController(MobicloneContext context, IAuth auth)
        {
            _context = context;
            _auth = auth;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _auth.User();

            var revenues = await (from revenue in _context.Revenues
                                  .Include(it => it.Account)
                                  .ThenInclude(it => it.User)
                                  where revenue.Account.User == user
                                  select revenue).ToListAsync();

            var response = new ResponseViewModel<IEnumerable<Transaction>>(revenues);

            return Ok(response);
        }
    }
}
