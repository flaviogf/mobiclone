using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Mobiclone.Api.Lib
{
    public class DiskStorage : IStorage
    {
        private readonly IConfiguration _configuration;

        public DiskStorage(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<string> Move(IFormFile fileForm)
        {
            var storagePath = _configuration["Storage:Path"];

            var fileName = $"{DateTime.Now:yyyy-MM-dd-HH-mm-ss}-{fileForm.FileName}";

            var filePath = Path.Join(storagePath, fileName);

            using var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write);

            await fileForm.CopyToAsync(fileStream);

            return fileName;
        }
    }
}
