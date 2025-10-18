using TadbirKish.DataReception.Application.Dtos;
using TadbirKish.DataReception.Application.Entities.Coverage.Commands.CreateCoverage;
using TadbirKish.DataReception.Application.Entities.Coverage.Commands.DeleteCoverage;
using TadbirKish.DataReception.Application.Entities.Coverage.Commands.UpdateCoverage;
using TadbirKish.DataReception.Application.Entities.Coverage.Queries.GetCoverage;
using System.Threading.Tasks;

namespace TadbirKish.DataReception.Application.Common.Interfaces
{
    public interface ICoverageManager
    {
        Task<bool> CreateCoverage(CreateCoverageCommand request);
        Task<bool> UpdateCoverage(UpdateCoverageCommand request);
        Task<bool> DeleteCoverage(DeleteCoverageCommand request);
        Task<CoverageInfoDto> GetCoverage(GetCoverageQuery request);

    }
}
