using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Mobiclone.Api.Controllers;
using Mobiclone.Api.Database;
using Mobiclone.Api.Lib;
using Mobiclone.Api.ViewModels.Account;
using System;
using System.Security.Claims;
using Xunit;

namespace Mobiclone.Test.Integration
{
    public class AccountControllerTests : IDisposable
    {
        private readonly MobicloneContext _context;

        private readonly HttpContextAccessor _accessor;

        private readonly AccountController _controller;

        public AccountControllerTests()
        {
            var builder = new DbContextOptionsBuilder<MobicloneContext>().UseInMemoryDatabase("account");

            _context = new MobicloneContext(builder.Options);

            var hash = new Bcrypt();

            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.Test.json").Build();

            _accessor = new HttpContextAccessor
            {
                HttpContext = new DefaultHttpContext()
            };

            var auth = new Jwt(_context, hash, configuration, _accessor);

            _controller = new AccountController(_context, auth);

            _context.Database.EnsureCreated();
        }

        [Fact]
        public async void Store_Should_Return_Status_201()
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

            var viewModel = new StoreAccountViewModel
            {
                Name = "Caixa",
                Type = "Corrente"
            };

            var result = await _controller.Store(viewModel);

            Assert.IsAssignableFrom<CreatedResult>(result);
        }

        [Fact]
        public async void Store_Should_Exist_An_Account_Into_Database()
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

            var viewModel = new StoreAccountViewModel
            {
                Name = "Caixa",
                Type = "Corrente"
            };

            await _controller.Store(viewModel);

            Assert.Collection(_context.Accounts,
                (account) =>
                {
                    Assert.Equal(1, account.Id);
                    Assert.Equal(viewModel.Name, account.Name);
                    Assert.Equal(viewModel.Type, account.Type);
                    Assert.Equal(user.Id, account.UserId);
                });
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
        }
    }
}
