using Mobiclone.Api.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mobiclone.Api.Lib
{
    public interface IExtract
    {
        Task<IList<Transition>> Read(DateTime begin, DateTime end);
    }
}
