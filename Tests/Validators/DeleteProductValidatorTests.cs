using FluentValidation;
using product_api.Validators;
using Xunit;

namespace product_api.Tests.Validators;

public class DeleteProductValidatorTests
{
    [Fact]
    public void DeleteProductValidator_InvalidProductId_ThrowsValidationException()
    {
        // Arrange
        var productId = -1;

        // Act & Assert
        Assert.Throws<ValidationException>(() => new DeleteProductValidator().ValidateAndThrow(productId));
    }

    [Fact]
    public void DeleteProductValidator_ValidProductId_DoesNotThrow()
    {
        // Arrange
        var productId = 1;

        // Act & Assert
        new DeleteProductValidator().ValidateAndThrow(productId);
    }
}