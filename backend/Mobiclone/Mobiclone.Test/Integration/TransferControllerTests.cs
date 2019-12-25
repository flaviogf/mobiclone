using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Mobiclone.Api.Controllers;
using Mobiclone.Api.Database;
using Mobiclone.Api.ViewModels.Transfer;
using System;
using Xunit;

namespace Mobiclone.Test.Integration
{
    public class TransferControllerTests : IDisposable
    {
        private readonly SqliteConnection _connection;

        private readonly MobicloneContext _context;

        private readonly TransferController _controller;

        public TransferControllerTests()
        {
            _connection = new SqliteConnection("Data Source=:memory:");

            _connection.Open();

            var options = new DbContextOptionsBuilder<MobicloneContext>().UseSqlite(_connection).Options;

            _context = new MobicloneContext(options);

            _controller = new TransferController(_context);

            _context.Database.EnsureCreated();
        }

        [Fact]
        public async void Store_Should_Return_Status_201()
        {
            var user = await Factory.User();

            await _context.Users.AddAsync(user);

            await _context.SaveChangesAsync();

            var nubank = await Factory.Account(userId: user.Id);

            await _context.Accounts.AddAsync(nubank);

            await _context.SaveChangesAsync();

            var itau = await Factory.Account(userId: user.Id);

            await _context.Accounts.AddAsync(itau);

            await _context.SaveChangesAsync();

            var viewModel = new StoreTransferViewModel
            {
                Description = "Save money",
                Date = new DateTime(year: 2019, month: 12, day: 25),
                Value = 25000,
                ToId = nubank.Id,
                FromId = itau.Id
            };

            var result = await _controller.Store(viewModel);

            Assert.IsAssignableFrom<CreatedResult>(result);
        }

        [Fact]
        public async void Store_Should_Have_An_Output()
        {
            var user = await Factory.User();

            await _context.Users.AddAsync(user);

            await _context.SaveChangesAsync();

            var nubank = await Factory.Account(userId: user.Id);

            await _context.Accounts.AddAsync(nubank);

            await _context.SaveChangesAsync();

            var itau = await Factory.Account(userId: user.Id);

            await _context.Accounts.AddAsync(itau);

            await _context.SaveChangesAsync();

            var viewModel = new StoreTransferViewModel
            {
                Description = "Save money",
                Date = new DateTime(year: 2019, month: 12, day: 25),
                Value = 25000,
                ToId = nubank.Id,
                FromId = itau.Id
            };

            var result = await _controller.Store(viewModel);

            Assert.Collection(_context.Outputs,
                (it) =>
                {
                    Assert.Equal(viewModel.Description, it.Description);
                    Assert.Equal(viewModel.Date, it.Date);
                    Assert.Equal(-viewModel.Value, it.Value);
                    Assert.Equal(viewModel.ToId, it.ToId);
                    Assert.Equal(viewModel.FromId, it.FromId);
                });
        }

        [Fact]
        public async void Store_Should_Have_An_Input()
        {
            var user = await Factory.User();

            await _context.Users.AddAsync(user);

            await _context.SaveChangesAsync();

            var nubank = await Factory.Account(userId: user.Id);

            await _context.Accounts.AddAsync(nubank);

            await _context.SaveChangesAsync();

            var itau = await Factory.Account(userId: user.Id);

            await _context.Accounts.AddAsync(itau);

            await _context.SaveChangesAsync();

            var viewModel = new StoreTransferViewModel
            {
                Description = "Save money",
                Date = new DateTime(year: 2019, month: 12, day: 25),
                Value = 25000,
                ToId = nubank.Id,
                FromId = itau.Id
            };

            var result = await _controller.Store(viewModel);

            Assert.Collection(_context.Inputs,
                (it) =>
                {
                    Assert.Equal(viewModel.Description, it.Description);
                    Assert.Equal(viewModel.Date, it.Date);
                    Assert.Equal(viewModel.Value, it.Value);
                    Assert.Equal(viewModel.ToId, it.ToId);
                    Assert.Equal(viewModel.FromId, it.FromId);
                });
        }

        public void Dispose()
        {
            _connection.Close();

            _context.Database.EnsureDeleted();
        }
    }
}
