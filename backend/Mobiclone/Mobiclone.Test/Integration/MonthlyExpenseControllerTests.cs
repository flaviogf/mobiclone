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
    public sealed class MonthlyExpenseControllerTests : IDisposable
    {
        private readonly MobicloneContext _context;

        private readonly MonthlyExpenseController _controller;

        private readonly SqliteConnection _connection;

        private readonly HttpContextAccessor _accessor;

        public MonthlyExpenseControllerTests()
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

            _controller = new MonthlyExpenseController(_context, auth);

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

            var expense = await Factory.Expense(accountId: account.Id, date: new DateTime(year: 2019, month: 12, day: 15), value: 180000);

            await _context.Expenses.AddAsync(expense);

            await _context.SaveChangesAsync();

            _accessor.HttpContext.User = await Factory.ClaimsPrincipal(userId: user.Id);

            var start = new DateTime(year: 2019, month: 12, day: 1);

            var end = new DateTime(year: 2019, month: 12, day: 31);

            var result = await _controller.Show(start, end);

            Assert.IsAssignableFrom<OkObjectResult>(result);
        }

        [Fact]
        public async void Show_Should_Monthly_Expense_Equal_To_180000_When_The_Current_Month_Has_A_Expense_With_The_Value_Equal_To_180000()
        {
            var user = await Factory.User();

            await _context.Users.AddAsync(user);

            await _context.SaveChangesAsync();

            var account = await Factory.Account(userId: user.Id);

            await _context.Accounts.AddAsync(account);

            await _context.SaveChangesAsync();

            var expense = await Factory.Expense(accountId: account.Id, date: new DateTime(year: 2019, month: 12, day: 15), value: 180000);

            await _context.Expenses.AddAsync(expense);

            await _context.SaveChangesAsync();

            _accessor.HttpContext.User = await Factory.ClaimsPrincipal(userId: user.Id);

            var start = new DateTime(year: 2019, month: 12, day: 1);

            var end = new DateTime(year: 2019, month: 12, day: 31);

            var result = await _controller.Show(start, end);

            var response = Assert.IsAssignableFrom<OkObjectResult>(result);

            Assert.Equal(180000, ((ResponseViewModel<int>)response.Value).Data);
        }

        public void Dispose()
        {
            _connection.Close();

            _context.Database.EnsureDeleted();
        }
    }
}
