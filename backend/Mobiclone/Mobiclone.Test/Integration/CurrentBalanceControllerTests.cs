using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Mobiclone.Api.Controllers;
using Mobiclone.Api.Database;
using Mobiclone.Api.Lib;
using Mobiclone.Api.ViewModels;
using System;
using Xunit;

namespace Mobiclone.Test.Integration
{
    public class CurrentBalanceControllerTests : IDisposable
    {
        private readonly SqliteConnection _connection;

        private readonly MobicloneContext _context;

        private readonly CurrentBalancaController _controller;

        private readonly HttpContextAccessor _accessor;

        public CurrentBalanceControllerTests()
        {
            _connection = new SqliteConnection("Data Source=:memory:");

            _connection.Open();

            var options = new DbContextOptionsBuilder<MobicloneContext>().UseSqlite(_connection).Options;

            _context = new MobicloneContext(options);

            var hash = new Bcrypt();

            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.Test.json").Build();

            _accessor = new HttpContextAccessor
            {
                HttpContext = new DefaultHttpContext()
            };

            var auth = new Jwt(_context, hash, configuration, _accessor);

            _controller = new CurrentBalancaController(_context, auth);

            _context.Database.EnsureCreated();
        }

        [Fact]
        public async void Show_Should_Return_Status_200()
        {
            var user = await Factory.User();

            await _context.Users.AddAsync(user);

            await _context.SaveChangesAsync();

            var account = await Factory.Account(userId: user.Id);

            await _context.Accounts.AddAsync(account);

            await _context.SaveChangesAsync();

            var pay = await Factory.Revenue(accountId: account.Id, value: 280000);

            await _context.Revenues.AddAsync(pay);

            await _context.SaveChangesAsync();

            _accessor.HttpContext.User = await Factory.ClaimsPrincipal(userId: user.Id);

            var result = await _controller.Show();

            Assert.IsAssignableFrom<OkObjectResult>(result);
        }

        [Fact]
        public async void Show_Should_Current_Balance_Equal_To_280000_When_There_Is_A_Account_With_A_Revenue_With_The_Value_Equal_To_280000()
        {
            var user = await Factory.User();

            await _context.Users.AddAsync(user);

            await _context.SaveChangesAsync();

            var account = await Factory.Account(userId: user.Id);

            await _context.Accounts.AddAsync(account);

            await _context.SaveChangesAsync();

            var pay = await Factory.Revenue(accountId: account.Id, value: 280000);

            await _context.Revenues.AddAsync(pay);

            await _context.SaveChangesAsync();

            _accessor.HttpContext.User = await Factory.ClaimsPrincipal(userId: user.Id);

            var result = await _controller.Show();

            var response = Assert.IsAssignableFrom<OkObjectResult>(result);

            Assert.Equal(280000, ((ResponseViewModel<int>)response.Value).Data);
        }

        public void Dispose()
        {
            _connection.Close();

            _context.Database.EnsureDeleted();
        }
    }
}
