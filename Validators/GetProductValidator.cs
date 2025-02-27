using FluentValidation;

namespace product_api.Validators;

public class GetProductValidator : AbstractValidator<int>
{
    public GetProductValidator()
    {
        RuleFor(x => x)
            .GreaterThan(0);
    }
}