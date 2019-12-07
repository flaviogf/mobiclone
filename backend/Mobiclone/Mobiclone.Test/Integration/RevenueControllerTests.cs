using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Mobiclone.Api.Controllers;
using Mobiclone.Api.Database;
using Mobiclone.Api.Lib;
using Mobiclone.Api.Models;
using Mobiclone.Api.ViewModels;
using Mobiclone.Api.ViewModels.Revenue;
using System;
using System.Security.Claims;
using Xunit;

namespace Mobiclone.Test.Integration
{
    public class RevenueControllerTests : IDisposable
    {
        private readonly MobicloneContext _context;

        private readonly HttpContextAccessor _accessor;

        private readonly RevenueController _controller;

        public RevenueControllerTests()
        {
            var builder = new DbContextOptionsBuilder<MobicloneContext>().UseInMemoryDatabase("revenue");

            _context = new MobicloneContext(builder.Options);

            var hash = new Bcrypt();

            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.Test.json").Build();

            _accessor = new HttpContextAccessor
            {
                HttpContext = new DefaultHttpContext()
            };

            var auth = new Jwt(_context, hash, configuration, _accessor);

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

        [Fact]
        public async void Show_Should_Return_Status_200()
        {
            var user = await Factory.User();

            await _context.Users.AddAsync(user);

            var account = await Factory.Account(userId: user.Id);

            await _context.Accounts.AddAsync(account);

            var revenue = await Factory.Revenue(accountId: account.Id);

            await _context.Revenues.AddAsync(revenue);

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

            var result = await _controller.Show(account.Id, revenue.Id);

            Assert.IsAssignableFrom<OkObjectResult>(result);
        }

        [Fact]
        public async void Show_Should_Return_Revenue()
        {
            var user = await Factory.User();

            await _context.Users.AddAsync(user);

            var account = await Factory.Account(userId: user.Id);

            await _context.Accounts.AddAsync(account);

            var revenue = await Factory.Revenue(accountId: account.Id);

            await _context.Revenues.AddAsync(revenue);

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

            var result = await _controller.Show(account.Id, revenue.Id);

            var response = Assert.IsAssignableFrom<OkObjectResult>(result);

            Assert.Equal(((ResponseViewModel<Revenue>)response.Value).Data.Id, revenue.Id);
        }

        [Fact]
        public async void Update_Should_Return_Status_200()
        {
            var user = await Factory.User();

            await _context.Users.AddAsync(user);

            var account = await Factory.Account(userId: user.Id);

            await _context.Accounts.AddAsync(account);

            var revenue = await Factory.Revenue(accountId: account.Id);

            await _context.Revenues.AddAsync(revenue);

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

            var viewModel = new UpdateRevenueViewModel
            {
                Description = "Supermarket",
                Value = 5000,
                Date = DateTime.Now.AddDays(-1)
            };

            var result = await _controller.Update(account.Id, revenue.Id, viewModel);

            Assert.IsAssignableFrom<OkObjectResult>(result);
        }

        [Fact]
        public async void Update_Should_Revenue_Has_Been_Updated()
        {
            var user = await Factory.User();

            await _context.Users.AddAsync(user);

            var account = await Factory.Account(userId: user.Id);

            await _context.Accounts.AddAsync(account);

            var revenue = await Factory.Revenue(accountId: account.Id);

            await _context.Revenues.AddAsync(revenue);

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

            var viewModel = new UpdateRevenueViewModel
            {
                Description = "Supermarket",
                Value = 5000,
                Date = DateTime.Now.AddDays(-1)
            };

            await _controller.Update(account.Id, revenue.Id, viewModel);

            await _context.Entry(revenue).ReloadAsync();

            Assert.Equal(viewModel.Description, revenue.Description);
            Assert.Equal(viewModel.Value, revenue.Value);
            Assert.Equal(viewModel.Date, revenue.Date);
        }

        [Fact]
        public async void Destroy_Should_Return_Status_200()
        {
            var user = await Factory.User();

            await _context.Users.AddAsync(user);

            var account = await Factory.Account(userId: user.Id);

            await _context.Accounts.AddAsync(account);

            var revenue = await Factory.Revenue(accountId: account.Id);

            await _context.Revenues.AddAsync(revenue);

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

            var result = await _controller.Destroy(account.Id, revenue.Id);

            Assert.IsAssignableFrom<OkObjectResult>(result);
        }

        [Fact]
        public async void Destroy_Should_Not_Exist_Revenue_Into_Database()
        {
            var user = await Factory.User();

            await _context.Users.AddAsync(user);

            var account = await Factory.Account(userId: user.Id);

            await _context.Accounts.AddAsync(account);

            var revenue = await Factory.Revenue(accountId: account.Id);

            await _context.Revenues.AddAsync(revenue);

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

            await _controller.Destroy(account.Id, revenue.Id);

            Assert.Empty(_context.Revenues);
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
        }
    }
}
