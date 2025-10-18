using MediatR;
using TadbirKish.DataReception.Application.Common.Interfaces;
using TadbirKish.DataReception.Application.Dtos;
using TadbirKish.DataReception.Application.Entities.Coverage.Queries.GetCoverage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TadbirKish.DataReception.Application.Entities.Coverage.Queries.GetCoverage
{
    public class GetCoverageQueryHandler : IRequestHandler<GetCoverageQuery, CoverageInfoDto>
    {
        private readonly ICoverageManager _context;

        public GetCoverageQueryHandler(ICoverageManager context)
        {
            _context = context;
        }

        public async Task<CoverageInfoDto> Handle(GetCoverageQuery request, CancellationToken cancellationToken)
        {
            return await _context.GetCoverage(request);
        }
    }
}
