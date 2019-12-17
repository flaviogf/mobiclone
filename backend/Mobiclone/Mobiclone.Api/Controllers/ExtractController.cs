using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mobiclone.Api.Lib;
using Mobiclone.Api.Models;
using Mobiclone.Api.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mobiclone.Api.Controllers
{
    [ApiController]
    [Route("/extract")]
    [Authorize]
    public class ExtractController : Controller
    {
        private readonly IExtract _extract;

        public ExtractController(IExtract extract)
        {
            _extract = extract;
        }

        [HttpGet]
        [Route("")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseViewModel<IList<Transaction>>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Index(DateTime begin, DateTime end)
        {
            var transactions = await _extract.Read(begin, end);

            var response = new ResponseViewModel<IList<Transaction>>(transactions);

            return Ok(response);
        }
    }
}
