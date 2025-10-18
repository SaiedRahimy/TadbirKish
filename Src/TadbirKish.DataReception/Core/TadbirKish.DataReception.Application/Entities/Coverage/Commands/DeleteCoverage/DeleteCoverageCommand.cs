using MediatR;
using TadbirKish.DataReception.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TadbirKish.DataReception.Application.Entities.Coverage.Commands.DeleteCoverage
{
    public class DeleteCoverageCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }

    public class DeleteCoverageCommandHandler : IRequestHandler<DeleteCoverageCommand, bool>
    {
        private readonly ICoverageManager _context;

        public DeleteCoverageCommandHandler(ICoverageManager context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteCoverageCommand request, CancellationToken cancellationToken)
        {
            return await _context.DeleteCoverage(request);
        }
    }
}
