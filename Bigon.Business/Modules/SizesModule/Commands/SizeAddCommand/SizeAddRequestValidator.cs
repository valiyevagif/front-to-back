using Bigon.Infrastructure.Localize.Sizes;
using FluentValidation;

namespace Bigon.Business.Modules.SizesModule.Commands.SizeAddCommand
{
    internal class SizeAddRequestValidator : AbstractValidator<SizeAddRequest>
    {
        public SizeAddRequestValidator()
        {
            RuleFor(m => m.Name)
            .NotNull().WithMessage(SizesResource.SIZE_NAME_CANT_BE_NULL)
            .NotEmpty().WithMessage(SizesResource.SIZE_NAME_CANT_BE_EMPTY);

            RuleFor(m => m.ShortName)
            .NotNull().WithMessage(SizesResource.SIZE_SHORTNAME_CANT_BE_NULL)
            .NotEmpty().WithMessage(SizesResource.SIZE_SHORTNAME_CANT_BE_EMPTY)
            .MaximumLength(5).WithMessage(SizesResource.SIZE_SHORTNAME_MAX_CHARACTER_5)
            .MinimumLength(2).WithMessage(SizesResource.SIZE_SHORTNAME_MIN_CHARACTER_1);
        }
    }
}
