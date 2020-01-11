using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Mobiclone.Api.Controllers;
using Mobiclone.Api.Database;
using Mobiclone.Api.Lib;
using System;
using System.IO;
using System.Security.Claims;
using Xunit;

namespace Mobiclone.Test.Integration
{
    public sealed class AvatarControllerTests : IDisposable
    {
        private readonly SqliteConnection _connection;

        private readonly MobicloneContext _context;

        private readonly HttpContextAccessor _accessor;

        private readonly AvatarController _controller;

        public AvatarControllerTests()
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

            var storage = new DiskStorage(configuration);

            _controller = new AvatarController(_context, auth, storage);

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

            var filePath = Path.Join(Directory.GetCurrentDirectory(), "Fixtures", "avatar.png");

            using var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);

            var fileForm = new FormFile(fileStream, 0, fileStream.Length, "file", "avatar.png")
            {
                Headers = new HeaderDictionary(),
                ContentDisposition = "form-data; name=\"file\"; filename=\"avatar.png\"",
                ContentType = "image/png",
            };

            var result = await _controller.Store(fileForm);

            Assert.IsAssignableFrom<OkObjectResult>(result);
        }

        [Fact]
        public async void Should_Exist_A_File()
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

            var filePath = Path.Join(Directory.GetCurrentDirectory(), "Fixtures", "avatar.png");

            using var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);

            var fileForm = new FormFile(fileStream, 0, fileStream.Length, "file", "avatar.png")
            {
                Headers = new HeaderDictionary(),
                ContentDisposition = "form-data; name=\"file\"; filename=\"avatar.png\"",
                ContentType = "image/png",
            };

            await _controller.Store(fileForm);

            await _context.Entry(user).ReloadAsync();

            Assert.Collection(_context.Files,
                (it) =>
                {
                    Assert.Equal(fileForm.FileName, it.Name);
                    Assert.NotNull(it.Path);
                    Assert.Equal(it.Id, user.FileId);
                });
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();

            _connection.Close();
        }
    }
}
