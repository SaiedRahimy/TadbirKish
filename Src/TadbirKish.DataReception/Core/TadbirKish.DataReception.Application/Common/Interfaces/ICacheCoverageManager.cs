using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using TadbirKish.DataReception.Application.Dtos;
using TadbirKish.DataReception.Domain.Entities;

namespace TadbirKish.DataReception.Application.Common.Interfaces
{
    public interface ICacheCoverageManager
    {
        ConcurrentDictionary<int, CoverageInfoDto> GetCachedCoverages();
        void RegisterNewCoverages(CoverageInfoDto coverage);
        Task FillCoveragesCache();
    }
}
