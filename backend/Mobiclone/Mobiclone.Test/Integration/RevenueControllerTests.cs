using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Mobiclone.Api.Controllers;
using Mobiclone.Api.Database;
using Mobiclone.Api.Lib;
using Mobiclone.Api.ViewModels.Revenue;
using System;
using Xunit;

namespace Mobiclone.Test.Integration
{
    public class RevenueControllerTests : IDisposable
    {
        private readonly MobicloneContext _context;

        private readonly RevenueController _controller;

        public RevenueControllerTests()
        {
            var builder = new DbContextOptionsBuilder<MobicloneContext>().UseInMemoryDatabase("revenue");

            _context = new MobicloneContext(builder.Options);

            var hash = new Bcrypt();

            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.Test.json").Build();

            var accessor = new HttpContextAccessor
            {
                HttpContext = new DefaultHttpContext()
            };

            var auth = new Jwt(_context, hash, configuration, accessor);

            _controller = new RevenueController(_context, auth);

            _context.Database.EnsureCreated();
        }

        [Fact]
        public async void Store_Should_Return_Status_201()
        {
            var user = await Factory.User();

            await _context.Users.AddAsync(user);

            var account = await Factory.Account(userId: user.Id);

            await _context.Accounts.AddAsync(account);

            await _context.SaveChangesAsync();

            var viewModel = new StoreRevenueViewModel
            {
                Description = "Supermarket",
                Value = 500,
                Date = DateTime.Now.AddDays(-2)
            };

            var result = await _controller.Store(account.Id, viewModel);

            Assert.IsAssignableFrom<OkObjectResult>(result);
        }

        [Fact]
        public async void Store_Should_Exist_Revenue_Into_Database()
        {
            var user = await Factory.User();

            await _context.Users.AddAsync(user);

            var account = await Factory.Account(userId: user.Id);

            await _context.Accounts.AddAsync(account);

            await _context.SaveChangesAsync();

            var viewModel = new StoreRevenueViewModel
            {
                Description = "Supermarket",
                Value = 500,
                Date = DateTime.Now.AddDays(-2)
            };

            await _controller.Store(account.Id, viewModel);

            Assert.Collection(_context.Revenues,
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
