using static BCrypt.Net.BCrypt;
using System.Threading.Tasks;

namespace Mobiclone.Api.Lib
{
    public class Bcrypt: IHash
    {
        public Task<string> Make(string value)
        {
            var hash = HashPassword(value);

            return Task.FromResult(hash);
        }
    }
}
