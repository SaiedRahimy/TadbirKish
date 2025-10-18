using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TadbirKish.DataReception.Application.Common.Interfaces;
using TadbirKish.DataReception.Application.Dtos;

namespace TadbirKish.DataReception.Application.Entities.RequestCoverage.Commands.CreateCoverage
{
    public class CreateRequestCoverageCommand : IRequest<bool>
    {
        public string Name { get; set; }    
        public List<RequestCoverageDetailsDto> RequestCoverageDetails { get; set; }
    }

    public class CreateCoverageCommandHandler : IRequestHandler<CreateRequestCoverageCommand, bool>
    {
        private readonly IRequestCoverageManager _context;

        public CreateCoverageCommandHandler(IRequestCoverageManager context)
        {
            _context = context;
        }

        public async Task<bool> Handle(CreateRequestCoverageCommand request, CancellationToken cancellationToken)
        {
            return  await _context.CreateRequestCoverage(request);
        }
    }
}
