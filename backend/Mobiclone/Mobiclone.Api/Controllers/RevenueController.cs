﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mobiclone.Api.Database;
using Mobiclone.Api.Lib;
using Mobiclone.Api.Models;
using Mobiclone.Api.ViewModels;
using Mobiclone.Api.ViewModels.Revenue;
using System.Linq;
using System.Threading.Tasks;

namespace Mobiclone.Api.Controllers
{
    [ApiController]
    [Route("account/{accountId}/revenue")]
    [Authorize]
    public class RevenueController : Controller
    {
        private readonly MobicloneContext _context;

        private readonly IAuth _auth;

        public RevenueController(MobicloneContext context, IAuth auth)
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
        public async Task<IActionResult> Store([FromRoute]int accountId, [FromBody]StoreRevenueViewModel viewModel)
        {
            var user = await _auth.User();

            var account = await (from current in _context.Accounts
                                 where current.Id == accountId && current.UserId == user.Id
                                 select current).FirstAsync();

            var revenue = new Revenue
            {
                Account = account,
                Description = viewModel.Description,
                Value = viewModel.Value,
                Date = viewModel.Date
            };

            await _context.Revenues.AddAsync(revenue);

            await _context.SaveChangesAsync();

            var response = new ResponseViewModel<int>(revenue.Id);

            return Ok(response);
        }

        [HttpGet]
        [Route("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Show([FromRoute] int accountId, [FromRoute] int id)
        {
            var user = await _auth.User();

            var revenue = await (from current in _context.Revenues
                                 where current.AccountId == accountId && current.Id == id
                                 select current).FirstAsync();

            var response = new ResponseViewModel<Revenue>(revenue);

            return Ok(response);
        }
    }
}