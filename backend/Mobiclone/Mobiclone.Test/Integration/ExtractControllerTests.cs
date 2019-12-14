using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
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
    public class ExtractControllerTests : IDisposable
    {
        private readonly SqliteConnection _connection;

        private readonly MobicloneContext _context;

        private readonly HttpContextAccessor _accessor;

        private readonly ExtractController _controller;

        public ExtractControllerTests()
        {
            _connection = new SqliteConnection("Data Source=:memory:");

            _connection.Open();

            var options = new DbContextOptionsBuilder<MobicloneContext>().UseSqlite(_connection).Options;

            _context = new MobicloneContext(options);

            _accessor = new HttpContextAccessor
            {
                HttpContext = new DefaultHttpContext()
            };

            var hash = new Bcrypt();

            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.Test.json").Build();

            var auth = new Jwt(_context, hash, configuration, _accessor);

            var extract = new DefaultExtract(_connection, auth);

            _controller = new ExtractController(extract);

            _context.Database.EnsureCreated();
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

            var begin = new DateTime(year: 2019, month: 12, day: 1);

            var end = new DateTime(year: 2019, month: 12, day: 31);

            var result = await _controller.Index(begin, end);

            Assert.IsAssignableFrom<OkObjectResult>(result);
        }

        [Fact]
        public async void Index_Should_Return_Only_One_Transaction()
        {
            var user = await Factory.User();

            await _context.Users.AddAsync(user);

            await _context.SaveChangesAsync();

            var account = await Factory.Account(userId: user.Id);

            await _context.Accounts.AddAsync(account);

            await _context.SaveChangesAsync();

            var pay = await Factory.Revenue(accountId: account.Id, date: new DateTime(year: 2019, month: 12, day: 1));

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

            var begin = new DateTime(year: 2019, month: 12, day: 1);

            var end = new DateTime(year: 2019, month: 12, day: 31);

            var result = await _controller.Index(begin, end);

            var response = Assert.IsAssignableFrom<OkObjectResult>(result);

            var transactions = ((ResponseViewModel<IList<Transaction>>)response.Value).Data;

            Assert.Collection(transactions,
                (it) =>
                {
                    Assert.Equal(pay.Id, it.Id);
                    Assert.Equal(pay.Description, it.Description);
                    Assert.Equal(pay.Value, it.Value);
                    Assert.Equal(pay.Date, it.Date);
                });
        }

        [Fact]
        public async void Show_Should_Return_Two_Transactions_When_The_Month_Has_One_Expense_And_One_Revenue()
        {
            var user = await Factory.User();

            await _context.Users.AddAsync(user);

            await _context.SaveChangesAsync();

            var account = await Factory.Account(userId: user.Id);

            await _context.Accounts.AddAsync(account);

            await _context.SaveChangesAsync();

            var pay = await Factory.Revenue(accountId: account.Id, date: new DateTime(year: 2019, month: 12, day: 1));

            await _context.Revenues.AddAsync(pay);

            await _context.SaveChangesAsync();

            var gym = await Factory.Expense(accountId: account.Id, date: new DateTime(year: 2019, month: 12, day: 15));

            await _context.Expenses.AddAsync(gym);

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

            var begin = new DateTime(year: 2019, month: 12, day: 1);

            var end = new DateTime(year: 2019, month: 12, day: 31);

            var result = await _controller.Index(begin, end);

            var response = Assert.IsAssignableFrom<OkObjectResult>(result);

            var transactions = ((ResponseViewModel<IList<Transaction>>)response.Value).Data;

            Assert.Collection(transactions,
                (it) =>
                {
                    Assert.Equal(gym.Id, it.Id);
                    Assert.Equal(gym.Description, it.Description);
                    Assert.Equal(gym.Value, it.Value);
                    Assert.Equal(gym.Date, it.Date);
                },
                (it) =>
                {
                    Assert.Equal(pay.Id, it.Id);
                    Assert.Equal(pay.Description, it.Description);
                    Assert.Equal(pay.Value, it.Value);
                    Assert.Equal(pay.Date, it.Date);
                });
        }

        [Fact]
        public async void Show_Should_Return_Only_One_Transaction_When_The_Month_Has_One_Transaction_And_Another_Month_Has_One_Transaction()
        {
            var user = await Factory.User();

            await _context.Users.AddAsync(user);

            await _context.SaveChangesAsync();

            var account = await Factory.Account(userId: user.Id);

            await _context.Accounts.AddAsync(account);

            await _context.SaveChangesAsync();

            var pay = await Factory.Revenue(accountId: account.Id, date: new DateTime(year: 2019, month: 12, day: 1));

            await _context.Revenues.AddAsync(pay);

            await _context.SaveChangesAsync();

            var gym = await Factory.Expense(accountId: account.Id, date: new DateTime(year: 2020, month: 1, day: 1));

            await _context.Expenses.AddAsync(gym);

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

            var begin = new DateTime(year: 2019, month: 12, day: 1);

            var end = new DateTime(year: 2019, month: 12, day: 31);

            var result = await _controller.Index(begin, end);

            var response = Assert.IsAssignableFrom<OkObjectResult>(result);

            var transactions = ((ResponseViewModel<IList<Transaction>>)response.Value).Data;

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

            _connection.Close();
        }
    }
}
