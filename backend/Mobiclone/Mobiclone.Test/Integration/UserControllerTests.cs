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
using Mobiclone.Api.ViewModels.User;
using System;
using Xunit;

namespace Mobiclone.Test.Integration
{
    public class UserControllerTests : IDisposable
    {
        private readonly MobicloneContext _context;

        private readonly UserController _controller;

        private readonly SqliteConnection _connection;

        private readonly HttpContextAccessor _accessor;

        public UserControllerTests()
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

            _controller = new UserController(_context, auth, hash);

            _context.Database.EnsureCreated();
        }

        [Fact]
        public async void Store_Should_Return_Status_201()
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
        public async void Store_Should_Exist_A_User_Into_Database()
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

        [Fact]
        public async void Show_Should_Return_Status_200()
        {
            var user = await Factory.User();

            await _context.Users.AddAsync(user);

            await _context.SaveChangesAsync();

            _accessor.HttpContext.User = await Factory.ClaimsPrincipal(userId: user.Id);

            var result = await _controller.Show();

            Assert.IsAssignableFrom<OkObjectResult>(result);
        }

        [Fact]
        public async void Show_Should_Return_User()
        {
            var user = await Factory.User();

            await _context.Users.AddAsync(user);

            await _context.SaveChangesAsync();

            _accessor.HttpContext.User = await Factory.ClaimsPrincipal(userId: user.Id);

            var result = await _controller.Show();

            var response = Assert.IsAssignableFrom<OkObjectResult>(result);

            var data = ((ResponseViewModel<User>)response.Value).Data;

            Assert.Equal(user, data);
        }

        [Fact]
        public async void Show_Should_Return_User_With_File()
        {
            var file = await Factory.File();

            await _context.Files.AddAsync(file);

            await _context.SaveChangesAsync();

            var user = await Factory.User(fileId: file.Id);

            await _context.Users.AddAsync(user);

            await _context.SaveChangesAsync();

            _accessor.HttpContext.User = await Factory.ClaimsPrincipal(userId: user.Id);

            var result = await _controller.Show();

            var response = Assert.IsAssignableFrom<OkObjectResult>(result);

            var data = ((ResponseViewModel<User>)response.Value).Data;

            Assert.Equal(file, data.File);
        }

        public void Dispose()
        {
            _connection.Close();

            _context.Database.EnsureDeleted();
        }
    }
}
