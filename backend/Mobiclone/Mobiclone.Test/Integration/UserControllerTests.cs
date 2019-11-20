using Microsoft.AspNetCore.Mvc;
using Mobiclone.Api.Controllers;
using Xunit;

namespace Mobiclone.Test.Integration
{
    public class UserControllerTests
    {
        [Fact]
        public async void Store_Should_Return_Status_201()
        {
            var controller = new UserController();

            var response = await controller.Store();

            Assert.IsAssignableFrom<OkResult>(response);
        }
    }
}
