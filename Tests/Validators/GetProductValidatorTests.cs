using FluentValidation;
using product_api.Validators;
using Xunit;

namespace product_api.Tests.Validators;

public class GetProductValidatorTests
{
    [Fact]
    public void GetProductValidator_InvalidProductId_ThrowsValidationException()
    {
        // Arrange
        var productId = -1;

        // Act & Assert
        Assert.Throws<ValidationException>(() => new GetProductValidator().ValidateAndThrow(productId));
    }

    [Fact]
    public void GetProductValidator_ValidProductId_DoesNotThrow()
    {
        // Arrange
        var productId = 1;

        // Act & Assert
        new GetProductValidator().ValidateAndThrow(productId);
    }
}