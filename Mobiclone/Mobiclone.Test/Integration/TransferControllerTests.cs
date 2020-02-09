using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Mobiclone.Api.Controllers;
using Mobiclone.Api.Database;
using Mobiclone.Api.Lib;
using Mobiclone.Api.ViewModels;
using Mobiclone.Api.ViewModels.Transfer;
using System;
using Xunit;

namespace Mobiclone.Test.Integration
{
    public sealed class TransferControllerTests : IDisposable
    {
        private readonly SqliteConnection _connection;

        private readonly MobicloneContext _context;

        private readonly HttpContextAccessor _accessor;

        private readonly TransferController _controller;

        public TransferControllerTests()
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

            _controller = new TransferController(_context, auth);

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

            _accessor.HttpContext.User = await Factory.ClaimsPrincipal(userId: user.Id);

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

            _accessor.HttpContext.User = await Factory.ClaimsPrincipal(userId: user.Id);

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

            _accessor.HttpContext.User = await Factory.ClaimsPrincipal(userId: user.Id);

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

        [Fact]
        public async void Show_Should_Return_Status_200()
        {
            var user = await Factory.User();

            await _context.Users.AddAsync(user);

            await _context.SaveChangesAsync();

            var itau = await Factory.Account(userId: user.Id);

            await _context.Accounts.AddAsync(itau);

            await _context.SaveChangesAsync();

            var nubank = await Factory.Account(userId: user.Id);

            await _context.Accounts.AddAsync(nubank);

            await _context.SaveChangesAsync();

            var output = await Factory.Output(toId: nubank.Id, fromId: itau.Id);

            await _context.Outputs.AddAsync(output);

            await _context.SaveChangesAsync();

            var input = await Factory.Input(toId: nubank.Id, fromId: itau.Id);

            await _context.Inputs.AddAsync(input);

            await _context.SaveChangesAsync();

            _accessor.HttpContext.User = await Factory.ClaimsPrincipal(userId: user.Id);

            var result = await _controller.Show(output.Id);

            Assert.IsAssignableFrom<OkObjectResult>(result);
        }

        [Fact]
        public async void Show_Should_Return_Output_With_Its_Input()
        {
            var user = await Factory.User();

            await _context.Users.AddAsync(user);

            await _context.SaveChangesAsync();

            var itau = await Factory.Account(userId: user.Id);

            await _context.Accounts.AddAsync(itau);

            await _context.SaveChangesAsync();

            var nubank = await Factory.Account(userId: user.Id);

            await _context.Accounts.AddAsync(nubank);

            await _context.SaveChangesAsync();

            var output = await Factory.Output(toId: nubank.Id, fromId: itau.Id);

            await _context.Outputs.AddAsync(output);

            await _context.SaveChangesAsync();

            var input = await Factory.Input(toId: nubank.Id, fromId: itau.Id);

            await _context.Inputs.AddAsync(input);

            await _context.SaveChangesAsync();

            _accessor.HttpContext.User = await Factory.ClaimsPrincipal(userId: user.Id);

            var result = await _controller.Show(output.Id);

            var response = Assert.IsAssignableFrom<OkObjectResult>(result);

            var data = ((ResponseViewModel<ShowTransferViewModel>)response.Value).Data;

            Assert.Equal(output.Description, data.Output.Description);
            Assert.Equal(output.Date, data.Output.Date);
            Assert.Equal(output.Value, data.Output.Value);
            Assert.Equal(output.ToId, data.Output.ToId);
            Assert.Equal(output.FromId, data.Output.FromId);

            Assert.Equal(input.Description, data.Input.Description);
            Assert.Equal(input.Date, data.Input.Date);
            Assert.Equal(input.Value, data.Input.Value);
            Assert.Equal(input.ToId, data.Input.ToId);
            Assert.Equal(input.FromId, data.Input.FromId);
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();

            _connection.Close();
        }
    }
}
