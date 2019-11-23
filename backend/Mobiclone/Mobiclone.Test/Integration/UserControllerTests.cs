using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mobiclone.Api.Controllers;
using Mobiclone.Api.Database;
using Mobiclone.Api.Lib;
using Mobiclone.Api.ViewModels.User;
using System;
using Xunit;

namespace Mobiclone.Test.Integration
{
    public class UserControllerTests : IDisposable
    {
        private readonly MobicloneContext _context;

        private readonly UserController _controller;

        public UserControllerTests()
        {
            var builder = new DbContextOptionsBuilder<MobicloneContext>().UseInMemoryDatabase("user");

            _context = new MobicloneContext(builder.Options);

            var hash = new Bcrypt();

            _controller = new UserController(_context, hash);

            _context.Database.EnsureCreated();
        }

        [Fact]
        public async void Should_Return_Status_201()
        {
            var viewModel = new StoreUserViewModel
            {
                Name = "Flavio",
                Email = "flavio@smn.com.br",
                Password = "test"
            };

            var response = await _controller.Store(viewModel);

            Assert.IsAssignableFrom<CreatedResult>(response);
        }

        [Fact]
        public async void Should_Exist_A_User_Into_Database()
        {
            var viewModel = new StoreUserViewModel
            {
                Name = "Flavio",
                Email = "flavio@smn.com.br",
                Password = "test"
            };

            await _controller.Store(viewModel);

            Assert.Collection(_context.Users,
                (user) =>
                {
                    Assert.Equal(1, user.Id);
                    Assert.Equal(viewModel.Name, user.Name);
                    Assert.Equal(viewModel.Email, user.Email);
                });
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
        }
    }
}
