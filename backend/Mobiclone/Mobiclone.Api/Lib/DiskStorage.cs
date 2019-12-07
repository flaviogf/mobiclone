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

        public async Task<string> Move(IFormFile file)
        {
            var storagePath = _configuration["Storage:Path"];

            var fileName = $"{Path.GetRandomFileName()}{Path.GetExtension(file.FileName)}";

            var filePath = Path.Join(storagePath, fileName);

            using var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write);

            await file.CopyToAsync(fileStream);

            return fileName;
        }
    }
}
