using MediatR;
using TadbirKish.DataReception.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TadbirKish.DataReception.Application.Entities.Coverage.Queries.GetCoverage
{
    public class GetCoverageQuery : IRequest<CoverageInfoDto>
    {
        public int Id { get; set; }
    }
}
