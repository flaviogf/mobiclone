using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mobiclone.Api.Controllers;
using Mobiclone.Api.Database;
using Mobiclone.Api.Lib;
using Mobiclone.Api.ViewModels.Session;
using System;
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

            _context = new MobicloneContext(builder.Options);

            var auth = new JwtAuth("xxxxxxxx");

            _controller = new SessionController(auth);

            _context.Database.EnsureCreated();
        }

        [Fact]
        public async void Should_Return_Status_200()
        {
            var viewModel = new StoreSessionViewModel
            {
                Email = "flavio@email.com",
                Password = "test"
            };

            var response = await _controller.Store(viewModel);

            Assert.IsAssignableFrom<OkObjectResult>(response);
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
        }
    }
}
