using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Mobiclone.Api.Controllers;
using Mobiclone.Api.Database;
using Mobiclone.Api.Lib;
using Mobiclone.Api.ViewModels;
using Mobiclone.Api.ViewModels.Session;
using System;
using System.Text;
using Xunit;

namespace Mobiclone.Test.Integration
{
    public class SessionControllerTests : IDisposable
    {
        private readonly SessionController _controller;

        private readonly MobicloneContext _context;

        public SessionControllerTests()
        {
            var builder = new DbContextOptionsBuilder<MobicloneContext>().UseInMemoryDatabase("session");

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("XXXXXXXXXXXXXXXXXXXXXXXX"));

            _context = new MobicloneContext(builder.Options);

            var hash = new Bcrypt();

            var auth = new Jwt(
                context: _context,
                hash: hash,
                issuer: "test",
                audience: "test",
                secretKey: secretKey
            );

            _controller = new SessionController(auth);

            _context.Database.EnsureCreated();
        }

        [Fact]
        public async void Should_Return_Status_200()
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
        public async void Should_Return_A_Jwt_Token()
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

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
        }
    }
}
