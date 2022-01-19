using FluentValidation;

namespace ShortLinks.Application.LinkManagment;
public class CreateRequestValidator : AbstractValidator<CreateRequest>
{
    public CreateRequestValidator()
    {
        RuleFor(x => x.OriginalLink).NotEmpty();
    }
}
