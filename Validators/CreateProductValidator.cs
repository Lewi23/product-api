using FluentValidation;
using product_api.DTO;

namespace product_api.Validators;

public class CreateProductValidator : AbstractValidator<CreateProductDTO>
{
    public CreateProductValidator()
    {
        RuleFor(x => x.Name)
            .NotNull()
            .MinimumLength(3) // products must have a name
            .MaximumLength(255);

        RuleFor(x => x.Stock)
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.Price)
            .GreaterThan(0);
    }
}