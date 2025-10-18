using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TadbirKish.DataReception.Application.Dtos;
using TadbirKish.DataReception.Application.Entities.Coverage.Commands.CreateCoverage;
using TadbirKish.DataReception.Application.Entities.Coverage.Commands.DeleteCoverage;
using TadbirKish.DataReception.Application.Entities.Coverage.Commands.UpdateCoverage;
using TadbirKish.DataReception.Application.Entities.Coverage.Queries.GetCoverage;
using TadbirKish.DataReception.Application.Entities.RequestCoverage.Commands.CreateCoverage;
using TadbirKish.DataReception.Application.Entities.RequestCoverage.Queries.GetCoverage;

namespace TadbirKish.DataReception.Application.Common.Interfaces
{
    public interface IRequestCoverageManager
    {
        Task<bool> CreateRequestCoverage(CreateRequestCoverageCommand request);
       
        Task<RequestCoverageInfoDto> GetRequestCoverages(GetRequestCoverageQuery request);
        Task<List<RequestCoverageInfoDto>> GetAllRequestCoverages(GetAllRequestCoverageQuery request);

       
    }
}
