using System.Collections.Generic;

namespace Mobiclone.Api.ViewModels
{
    public class ResponseViewModel<T>
    {
        public ResponseViewModel(T data, IEnumerable<string> errors = null)
        {
            Data = data;
            Errors = errors ?? new string[0];
        }

        public T Data { get; }
        public IEnumerable<string> Errors { get; }
    }
}
