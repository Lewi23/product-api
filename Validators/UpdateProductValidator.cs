using FluentValidation;
using product_api.DTO;

namespace product_api.Validators;

public class UpdateProductValidator : AbstractValidator<(int, UpdateProductDTO)>
{
    // TODO: Create Test case for testing product name min/max length - would normally use a faker lib to generate the strings

    public UpdateProductValidator()
    {
        RuleFor(x => x.Item1)
            .GreaterThan(0);

        RuleFor(x => x.Item2.Name)
            .NotNull()
            .MinimumLength(3) // products must have a name
            .MaximumLength(255);

        RuleFor(x => x.Item2.Stock)
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.Item2.Price)
            .GreaterThan(0);
    }
}