using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Mobiclone.Api.Controllers;
using Mobiclone.Api.Lib;
using System.IO;
using Xunit;

namespace Mobiclone.Test.Integration
{
    public class AvatarControllerTests
    {
        [Fact]
        public async void Store_Should_Return_Status_201()
        {
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.Test.json").Build();

            var storage = new DiskStorage(configuration);

            var controller = new AvatarController(storage);

            var directory = Directory.GetCurrentDirectory();

            var filePath = Path.Join(directory, "Fixtures", "avatar.png");

            using var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);

            var fileForm = new FormFile(fileStream, 0, fileStream.Length, "file", "avatar.png")
            {
                Headers = new HeaderDictionary(),
                ContentDisposition = "form-data; name=\"file\"; filename=\"avatar.png\"",
                ContentType = "image/png",
            };

            var result = await controller.Store(fileForm);

            Assert.IsAssignableFrom<OkObjectResult>(result);
        }
    }
}
