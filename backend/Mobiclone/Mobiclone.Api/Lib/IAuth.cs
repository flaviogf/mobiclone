using Mobiclone.Api.Models;
using System.Threading.Tasks;

namespace Mobiclone.Api.Lib
{
    public interface IAuth
    {
        Task<string> Attempt(string email, string password);

        Task<User> User();
    }
}
