using FluentValidation;

namespace product_api.Validators;

public class DeleteProductValidator : AbstractValidator<int>
{
    public DeleteProductValidator()
    {
        RuleFor(x => x)
            .GreaterThan(0);
    }
}