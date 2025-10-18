using MediatR;
using TadbirKish.DataReception.Application.Common.Interfaces;
using TadbirKish.DataReception.Application.Dtos;
using System.Threading;
using System.Threading.Tasks;

namespace TadbirKish.DataReception.Application.Entities.RequestCoverage.Queries.GetCoverage
{
    public class GetRequestCoverageQueryHandler : IRequestHandler<GetRequestCoverageQuery, RequestCoverageInfoDto>
    {
        private readonly IRequestCoverageManager _context;

        public GetRequestCoverageQueryHandler(IRequestCoverageManager context)
        {
            _context = context;
        }

        public async Task<RequestCoverageInfoDto> Handle(GetRequestCoverageQuery request, CancellationToken cancellationToken)
        {
            return await _context.GetRequestCoverages(request);
        }
    }
}
