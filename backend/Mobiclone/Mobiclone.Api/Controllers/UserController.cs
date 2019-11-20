using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Mobiclone.Api.Controllers
{
    public class UserController: Controller
    {
        public async Task<IActionResult> Store()
        {
            return Ok();
        }
    }
}
