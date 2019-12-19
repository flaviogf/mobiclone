using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Mobiclone.Api.Controllers
{
    public class MonthlyExpenseController : Controller
    {
        public async Task<IActionResult> Show(DateTime start, DateTime end)
        {
            return Ok("");
        }
    }
}
