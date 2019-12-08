using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Mobiclone.Api.Controllers;
using Mobiclone.Api.Database;
using Mobiclone.Api.Lib;
using Mobiclone.Api.ViewModels.Expense;
using System;
using System.Security.Claims;
using Xunit;

namespace Mobiclone.Test.Integration
{
    public class ExpenseControllerTests : IDisposable
    {
        private readonly MobicloneContext _context;

        private readonly ExpenseController _controller;

        private readonly HttpContextAccessor _accessor;

        public ExpenseControllerTests()
        {
            var builder = new DbContextOptionsBuilder<MobicloneContext>().UseInMemoryDatabase("expense");

            _context = new MobicloneContext(builder.Options);

            var hash = new Bcrypt();

            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.Test.json").Build();

            _accessor = new HttpContextAccessor
            {
                HttpContext = new DefaultHttpContext()
            };

            var auth = new Jwt(_context, hash, configuration, _accessor);

            _controller = new ExpenseController(_context, auth);
        }

        [Fact]
        public async void Store_Should_Return_Status_200()
        {
            var user = await Factory.User();

            await _context.Users.AddAsync(user);

            var account = await Factory.Account(userId: user.Id);

            await _context.Accounts.AddAsync(account);

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

            var viewModel = new StoreExpenseViewModel
            {
                Description = "Nintendo Switch",
                Value = 180000,
                Date = DateTime.Now.AddDays(-1)
            };

            var result = await _controller.Store(account.Id, viewModel);

            Assert.IsAssignableFrom<OkObjectResult>(result);
        }

        [Fact]
        public async void Should_Exist_A_Expense()
        {
            var user = await Factory.User();

            await _context.Users.AddAsync(user);

            var account = await Factory.Account(userId: user.Id);

            await _context.Accounts.AddAsync(account);

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

            var viewModel = new StoreExpenseViewModel
            {
                Description = "Nintendo Switch",
                Value = 180000,
                Date = DateTime.Now.AddDays(-1)
            };

            await _controller.Store(account.Id, viewModel);

            Assert.Collection(_context.Expenses,
                (it) =>
                {
                    Assert.Equal(viewModel.Description, it.Description);
                    Assert.Equal(viewModel.Value, it.Value);
                    Assert.Equal(viewModel.Date, it.Date);
                });
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
        }
    }
}
