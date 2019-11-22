using System.Threading.Tasks;

namespace Mobiclone.Api.Lib
{
    public interface IHash
    {
        Task<string> Make(string value);
    }
}
