using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Mobiclone.Api.Lib
{
    public interface IStorage
    {
        Task<string> Move(IFormFile file);
    }
}
