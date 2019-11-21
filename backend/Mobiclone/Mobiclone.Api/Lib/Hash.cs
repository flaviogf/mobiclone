using System.Threading.Tasks;

namespace Mobiclone.Api.Lib
{
    public class Hash
    {
        public static Task<string> Make(string value)
        {
            var hash = BCrypt.Net.BCrypt.HashPassword(value);

            return Task.FromResult(hash);
        }
    }
}
