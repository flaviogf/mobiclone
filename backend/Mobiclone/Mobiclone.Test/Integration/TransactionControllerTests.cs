using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Mobiclone.Api.Controllers;
using Mobiclone.Api.Database;
using Mobiclone.Api.Lib;
using Mobiclone.Api.Models;
using Mobiclone.Api.ViewModels;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using Xunit;

namespace Mobiclone.Test.Integration
{
    public class TransactionControllerTests : IDisposable
    {
        private readonly MobicloneContext _context;

        private readonly HttpContextAccessor _accessor;

        private readonly TransactionController _controller;

        public TransactionControllerTests()
        {
            var builder = new DbContextOptionsBuilder<MobicloneContext>().UseInMemoryDatabase("transactions");

            _context = new MobicloneContext(builder.Options);

            _accessor = new HttpContextAccessor
            {
                HttpContext = new DefaultHttpContext()
            };

            var hash = new Bcrypt();

            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.Test.json").Build();

            var auth = new Jwt(_context, hash, configuration, _accessor);

            _controller = new TransactionController(_context, auth);
        }

        [Fact]
        public async void Index_Should_Return_Status_200()
        {
            var user = await Factory.User();

            await _context.Users.AddAsync(user);

            await _context.SaveChangesAsync();

            _accessor.HttpContext.User = new ClaimsPrincipal
            (
                new ClaimsIdentity
                (
                    new Claim[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                    }
                )
            );

            var result = await _controller.Index();

            Assert.IsAssignableFrom<OkObjectResult>(result);
        }

        [Fact]
        public async void Index_Should_Return_Only_One_Transaction()
        {
            var user = await Factory.User();

            await _context.Users.AddAsync(user);

            var account = await Factory.Account(userId: user.Id);

            await _context.Accounts.AddAsync(account);

            var pay = await Factory.Revenue(accountId: account.Id);

            await _context.Revenues.AddAsync(pay);

            await _context.SaveChangesAsync();

            _accessor.HttpContext.User = new ClaimsPrincipal
            (
                new ClaimsIdentity
                (
                    new Claim[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                    }
                )
            );

            var result = await _controller.Index();

            var response = Assert.IsAssignableFrom<OkObjectResult>(result);

            var transactions = ((ResponseViewModel<IEnumerable<Transaction>>)response.Value).Data;

            Assert.Collection(transactions,
                (it) =>
                {
                    Assert.Equal(pay.Id, it.Id);
                    Assert.Equal(pay.Description, it.Description);
                    Assert.Equal(pay.Value, it.Value);
                    Assert.Equal(pay.Date, it.Date);
                });
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
        }
    }
}
