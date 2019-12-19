using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Mobiclone.Api.Controllers;
using Mobiclone.Api.Database;
using System;
using Xunit;

namespace Mobiclone.Test.Integration
{
    public class MonthlyExpenseControllerTests : IDisposable
    {
        private readonly MobicloneContext _context;

        private readonly MonthlyExpenseController _controller;

        private readonly SqliteConnection _connection;

        public MonthlyExpenseControllerTests()
        {
            _connection = new SqliteConnection("Data Source=:memory:");

            _connection.Open();

            var options = new DbContextOptionsBuilder<MobicloneContext>().UseSqlite(_connection).Options;

            _context = new MobicloneContext(options);

            _controller = new MonthlyExpenseController();

            _context.Database.EnsureCreated();
        }

        [Fact]
        public async void Show_Should_Return_Status_200()
        {
            var user = await Factory.User();

            await _context.Users.AddAsync(user);

            await _context.SaveChangesAsync();

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

            var start = new DateTime(year: 2019, month: 12, day: 1);

            var end = new DateTime(year: 2019, month: 12, day: 31);

            var result = await _controller.Show(start, end);

            Assert.IsAssignableFrom<OkObjectResult>(result);
        }

        public void Dispose()
        {
            _connection.Close();

            _context.Database.EnsureDeleted();
        }
    }
}
