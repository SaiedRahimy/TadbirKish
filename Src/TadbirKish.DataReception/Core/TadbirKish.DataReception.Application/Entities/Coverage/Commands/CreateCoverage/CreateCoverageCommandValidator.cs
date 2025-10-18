using FluentValidation;
using TadbirKish.DataReception.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace TadbirKish.DataReception.Application.Entities.Coverage.Commands.CreateCoverage
{
    public class CreateCoverageCommandValidator : AbstractValidator<CreateCoverageCommand>
    {
        private readonly ICoverageManager _context;

        public CreateCoverageCommandValidator(ICoverageManager context)
        {
            _context = context;


            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("نام پوشش الزامی است.");

            RuleFor(x => x.Min)
                .GreaterThan(0)
                .WithMessage("حداقل مقدار باید بزرگتر از صفر باشد.");

            RuleFor(x => x.Max)
                .GreaterThan(x => x.Min)
                .WithMessage("حداکثر مقدار باید بزرگتر از حداقل مقدار باشد.");

            RuleFor(x => x.Coefficient)
                .GreaterThan(0)
                .WithMessage("ضریب باید بزرگتر از صفر باشد.");

        }
    }
}
