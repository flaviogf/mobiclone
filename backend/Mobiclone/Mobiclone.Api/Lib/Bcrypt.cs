using System.Threading.Tasks;

namespace Mobiclone.Api.Lib
{
    public class Bcrypt : IHash
    {
        public Task<string> Make(string value)
        {
            var hash = BCrypt.Net.BCrypt.HashPassword(value);

            return Task.FromResult(hash);
        }

        public Task<bool> Verify(string value, string hash)
        {
            var result = BCrypt.Net.BCrypt.Verify(value, hash);

            return Task.FromResult(result);
        }
    }
}
