using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Mobiclone.Api.Controllers;
using Mobiclone.Api.Database;
using Mobiclone.Api.Lib;
using Mobiclone.Api.ViewModels;
using Mobiclone.Api.ViewModels.Session;
using System;
using Xunit;

namespace Mobiclone.Test.Integration
{
    public sealed class SessionControllerTests : IDisposable
    {
        private readonly SessionController _controller;

        private readonly MobicloneContext _context;

        public SessionControllerTests()
        {
            var builder = new DbContextOptionsBuilder<MobicloneContext>().UseInMemoryDatabase("session");

            _context = new MobicloneContext(builder.Options);

            var hash = new Bcrypt();

            var httpContextAcessor = new HttpContextAccessor();

            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.Test.json").Build();

            var auth = new Jwt(
                _context,
                hash,
                configuration,
                httpContextAcessor
            );

            _controller = new SessionController(auth);

            _context.Database.EnsureCreated();
        }

        [Fact]
        public async void Store_Should_Return_Status_200()
        {
            var user = await Factory.User(email: "flavio@email.com", password: "test");

            await _context.Users.AddAsync(user);

            await _context.SaveChangesAsync();

            var viewModel = new StoreSessionViewModel
            {
                Email = "flavio@email.com",
                Password = "test"
            };

            var result = await _controller.Store(viewModel);

            Assert.IsAssignableFrom<OkObjectResult>(result);
        }

        [Fact]
        public async void Store_Should_Return_A_Jwt_Token()
        {
            var user = await Factory.User(email: "flavio@email.com", password: "test");

            await _context.Users.AddAsync(user);

            await _context.SaveChangesAsync();

            var viewModel = new StoreSessionViewModel
            {
                Email = "flavio@email.com",
                Password = "test"
            };

            var result = await _controller.Store(viewModel);

            var response = Assert.IsAssignableFrom<OkObjectResult>(result);

            Assert.NotNull(((ResponseViewModel<string>)response.Value).Data);
        }

        [Fact]
        public async void Store_Should_Return_Status_401_When_The_Password_That_Was_Passed_Is_Wrong()
        {
            var user = await Factory.User(email: "flavio@email.com", password: "test");

            await _context.Users.AddAsync(user);

            await _context.SaveChangesAsync();

            var viewModel = new StoreSessionViewModel
            {
                Email = "flavio@email.com",
                Password = "wrong"
            };

            var result = await _controller.Store(viewModel);

            Assert.IsAssignableFrom<UnauthorizedResult>(result);
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
        }
    }
}
