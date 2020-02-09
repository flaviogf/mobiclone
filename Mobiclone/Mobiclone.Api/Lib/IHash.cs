using System.Threading.Tasks;

namespace Mobiclone.Api.Lib
{
    public interface IHash
    {
        Task<string> Make(string value);

        Task<bool> Verify(string value, string hash);
    }
}
