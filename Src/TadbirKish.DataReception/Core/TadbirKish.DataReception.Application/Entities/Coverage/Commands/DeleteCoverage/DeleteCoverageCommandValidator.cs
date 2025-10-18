using FluentValidation;
using TadbirKish.DataReception.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace TadbirKish.DataReception.Application.Entities.Coverage.Commands.DeleteCoverage
{
    public class DeleteCoverageCommandValidator : AbstractValidator<DeleteCoverageCommand>
    {
        private readonly ICoverageManager _context;

        public DeleteCoverageCommandValidator(ICoverageManager context)
        {
            _context = context;

            RuleFor(v => v.Id)
              .NotNull().WithMessage("شناسه اجباری است.")
              .GreaterThan(0).WithMessage("شناسه باید بزرگتر از 0 باشد.");



        }
    }
}
