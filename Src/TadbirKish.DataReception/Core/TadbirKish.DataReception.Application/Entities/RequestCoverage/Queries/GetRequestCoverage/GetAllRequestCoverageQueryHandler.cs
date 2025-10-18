using MediatR;
using TadbirKish.DataReception.Application.Common.Interfaces;
using TadbirKish.DataReception.Application.Dtos;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace TadbirKish.DataReception.Application.Entities.RequestCoverage.Queries.GetCoverage
{
    public class GetAllRequestCoverageQueryHandler : IRequestHandler<GetAllRequestCoverageQuery, List<RequestCoverageInfoDto>>
    {
        private readonly IRequestCoverageManager _context;

        public GetAllRequestCoverageQueryHandler(IRequestCoverageManager context)
        {
            _context = context;
        }

        public async Task<List<RequestCoverageInfoDto>> Handle(GetAllRequestCoverageQuery request, CancellationToken cancellationToken)
        {
            return await _context.GetAllRequestCoverages(request);
        }
    }
}
