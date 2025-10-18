using FluentValidation;
using TadbirKish.DataReception.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace TadbirKish.DataReception.Application.Entities.Coverage.Commands.UpdateCoverage
{
    public class UpdateCoverageCommandValidator : AbstractValidator<UpdateCoverageCommand>
    {
        private readonly ICoverageManager _context;

        public UpdateCoverageCommandValidator(ICoverageManager context)
        {
            _context = context;

            RuleFor(v => v.Id)
               .NotNull().WithMessage("شناسه اجباری است.")
               .GreaterThan(0).WithMessage("شناسه باید بزرگتر از 0 باشد.");

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
