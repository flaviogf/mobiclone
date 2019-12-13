using Mobiclone.Api.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mobiclone.Api.Lib
{
    public interface IExtract
    {
        Task<IList<Transaction>> Read(DateTime begin, DateTime end);
    }
}
