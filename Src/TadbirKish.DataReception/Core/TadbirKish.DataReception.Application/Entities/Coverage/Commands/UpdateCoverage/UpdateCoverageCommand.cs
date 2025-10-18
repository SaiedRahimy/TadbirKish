using MediatR;
using TadbirKish.DataReception.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TadbirKish.DataReception.Application.Entities.Coverage.Commands.UpdateCoverage
{
    public class UpdateCoverageCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public long Min { get; set; }
        public long Max { get; set; }
        public double Coefficient { get; set; }
    }

    public class UpdateCoverageCommandHandler : IRequestHandler<UpdateCoverageCommand, bool>
    {
        private readonly ICoverageManager _context;

        public UpdateCoverageCommandHandler(ICoverageManager context)
        {
            _context = context;
        }

        public async Task<bool> Handle(UpdateCoverageCommand request, CancellationToken cancellationToken)
        {
            return await _context.UpdateCoverage(request);
        }
    }
}
