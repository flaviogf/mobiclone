using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mobiclone.Api.Database;
using Mobiclone.Api.Lib;
using Mobiclone.Api.ViewModels;
using System.Linq;
using System.Threading.Tasks;

namespace Mobiclone.Api.Controllers
{
    public class CurrentBalancaController : Controller
    {
        private readonly MobicloneContext _context;

        private readonly IAuth _auth;

        public CurrentBalancaController(MobicloneContext context, IAuth auth)
        {
            _context = context;
            _auth = auth;
        }

        public async Task<IActionResult> Show()
        {
            var user = await _auth.User();

            var revenues = await (from current in _context.Revenues
                                  join accounts in _context.Accounts on current.AccountId equals accounts.Id
                                  join users in _context.Users on accounts.UserId equals users.Id
                                  where users.Id == user.Id
                                  select current.Value).SumAsync();

            var response = new ResponseViewModel<int>(revenues);

            return Ok(response);
        }
    }
}
