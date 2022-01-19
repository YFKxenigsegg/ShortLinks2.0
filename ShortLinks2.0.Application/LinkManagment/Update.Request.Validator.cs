using FluentValidation;

namespace ShortLinks.Application.LinkManagment;
public class UpdateRequestValidator : AbstractValidator<UpdateRequest>
{
    public UpdateRequestValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
    }
}
