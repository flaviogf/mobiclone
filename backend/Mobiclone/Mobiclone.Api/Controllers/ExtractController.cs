using Microsoft.AspNetCore.Mvc;
using Mobiclone.Api.Lib;
using Mobiclone.Api.Models;
using Mobiclone.Api.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mobiclone.Api.Controllers
{
    public class ExtractController : Controller
    {
        private readonly IExtract _extract;

        public ExtractController(IExtract extract)
        {
            _extract = extract;
        }

        public async Task<IActionResult> Index(DateTime begin, DateTime end)
        {
            var transactions = await _extract.Read(begin, end);

            var response = new ResponseViewModel<IList<Transaction>>(transactions);

            return Ok(response);
        }
    }
}
