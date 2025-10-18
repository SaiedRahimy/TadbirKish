using MediatR;
using TadbirKish.DataReception.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TadbirKish.DataReception.Application.Entities.Coverage.Commands.CreateCoverage
{
    public class CreateCoverageCommand : IRequest<bool>
    {
        public string Name { get; set; }
        public long Min { get; set; }
        public long Max { get; set; }
        public double Coefficient { get; set; }
    }

    public class CreateCoverageCommandHandler : IRequestHandler<CreateCoverageCommand, bool>
    {
        private readonly ICoverageManager _context;

        public CreateCoverageCommandHandler(ICoverageManager context)
        {
            _context = context;
        }

        public async Task<bool> Handle(CreateCoverageCommand request, CancellationToken cancellationToken)
        {
            return  await _context.CreateCoverage(request);
        }
    }
}
