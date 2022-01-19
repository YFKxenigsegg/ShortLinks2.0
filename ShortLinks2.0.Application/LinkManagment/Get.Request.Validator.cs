using FluentValidation;

namespace ShortLinks.Application.LinkManagment;
public class GetRequestValidator : AbstractValidator<GetRequest>
{
    public GetRequestValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
    }
}
