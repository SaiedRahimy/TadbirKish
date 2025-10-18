using FluentValidation;
using TadbirKish.DataReception.Application.Common.Interfaces;
using TadbirKish.DataReception.Application.Dtos;

namespace TadbirKish.DataReception.Application.Entities.RequestCoverage.Commands.CreateCoverage
{
    public class CreateRequestCoverageCommandValidator : AbstractValidator<CreateRequestCoverageCommand>
    {
        private readonly ICoverageManager _context;

        public CreateRequestCoverageCommandValidator(ICoverageManager context, ICacheCoverageManager cacheCoverageManager)
        {
            _context = context;

            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("وارد کردن نام الزامی است.");

            var getCachedCoveragesCount = cacheCoverageManager.GetCachedCoverages().Count;
            RuleFor(x => x.RequestCoverageDetails)
                .NotNull()
                .WithMessage("لیست پوشش‌ ها نباید خالی باشد.")
                .Must(list => list != null && list.Count >= 1 && list.Count <= getCachedCoveragesCount)
                .WithMessage($"تعداد پوشش‌ ها باید بین 1 تا {getCachedCoveragesCount} مورد باشد.");

            RuleForEach(x => x.RequestCoverageDetails).SetValidator(new RequestCoverageDetailsDtoValidator(cacheCoverageManager));

        }
    }
    public class RequestCoverageDetailsDtoValidator : AbstractValidator<RequestCoverageDetailsDto>
    {
        private readonly ICacheCoverageManager _context;

        public RequestCoverageDetailsDtoValidator(ICacheCoverageManager context)
        {
            _context = context;


            RuleFor(x => x.CoverageId)
                .GreaterThan(0)
                .WithMessage("شناسه پوشش باید وارد شود.")
                .Must(id =>
                {
                    var allCached = _context.GetCachedCoverages();
                    return allCached != null && allCached.ContainsKey(id);
                })
            .WithMessage("شناسه پوشش نامعتبر است.");

            var allCachedCoverages=_context.GetCachedCoverages();


            RuleFor(x => x.GrossPremium)
                .GreaterThan(0)
                .WithMessage("حق بیمه ناخالص باید بزرگتر از 0 باشد.");

          

            When(x => allCachedCoverages != null && allCachedCoverages.ContainsKey(x.CoverageId), () =>
            {
                RuleFor(x => x.GrossPremium)
                    .GreaterThan(x => allCachedCoverages[x.CoverageId].Min)
                    .WithMessage(x => $"حق بیمه ناخالص باید بزرگ‌تر از {allCachedCoverages[x.CoverageId].Min} باشد.");

                RuleFor(x => x.GrossPremium)
                    .LessThan(x => allCachedCoverages[x.CoverageId].Max)
                    .WithMessage(x => $"حق بیمه ناخالص باید کمتر از {allCachedCoverages[x.CoverageId].Max} باشد.");
            });

        }
    }
}
