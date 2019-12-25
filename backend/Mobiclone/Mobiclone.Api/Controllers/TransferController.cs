using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mobiclone.Api.Database;
using Mobiclone.Api.Models;
using Mobiclone.Api.ViewModels;
using Mobiclone.Api.ViewModels.Transfer;
using System.Threading.Tasks;

namespace Mobiclone.Api.Controllers
{
    [ApiController]
    [Route("/transfer")]
    [Authorize]
    public class TransferController : Controller
    {
        private readonly MobicloneContext _context;

        public TransferController(MobicloneContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ResponseViewModel<int>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Store([FromBody]StoreTransferViewModel viewModel)
        {
            var output = new Output
            {
                Description = viewModel.Description,
                Value = -viewModel.Value,
                Date = viewModel.Date,
                ToId = viewModel.ToId,
                FromId = viewModel.FromId
            };

            var input = new Input
            {

                Description = viewModel.Description,
                Value = viewModel.Value,
                Date = viewModel.Date,
                ToId = viewModel.ToId,
                FromId = viewModel.FromId
            };

            await _context.Outputs.AddAsync(output);

            await _context.Inputs.AddAsync(input);

            await _context.SaveChangesAsync();

            var response = new ResponseViewModel<int>(output.Id);

            return Created($"transfer/{output.Id}", response);
        }
    }
}
