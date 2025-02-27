using FluentValidation;
using product_api.DTO;
using product_api.Validators;
using Xunit;

namespace product_api.Tests.Validators;

public class UpdateProductValidatorTests
{
    [Fact]
    public void UpdateProductValidator_InvalidUserId_ThrowsValidationException()
    {
        // Arrange
        var userId = -1;

        var dto = new UpdateProductDTO
        {
            Price = 1,
            Name = "Product 1",
            Stock = 10
        };

        // Act & Assert
        Assert.Throws<ValidationException>(() => new UpdateProductValidator().ValidateAndThrow((userId, dto)));
    }


    [Fact]
    public void UpdateProductValidator_InvalidPrice_ThrowsValidationException()
    {
        // Arrange
        var userId = 1;

        var dto = new UpdateProductDTO
        {
            Price = -1,
            Name = "Product 1",
            Stock = 10
        };

        // Act & Assert
        Assert.Throws<ValidationException>(() => new UpdateProductValidator().ValidateAndThrow((userId, dto)));
    }

    [Fact]
    public void UpdateProductValidator_InvalidName_ThrowsValidationException()
    {
        // Arrange
        var userId = 1;

        var dto = new UpdateProductDTO
        {
            Price = 1,
            Name = null,
            Stock = 10
        };

        // Act & Assert
        Assert.Throws<ValidationException>(() => new UpdateProductValidator().ValidateAndThrow((userId, dto)));
    }

    [Fact]
    public void UpdateProductValidator_InvalidStock_ThrowsValidationException()
    {
        // Arrange
        var userId = 1;

        var dto = new UpdateProductDTO
        {
            Price = 1,
            Name = "Product 1",
            Stock = -1
        };

        // Act & Assert
        Assert.Throws<ValidationException>(() => new UpdateProductValidator().ValidateAndThrow((userId, dto)));
    }

    [Fact]
    public void UpdateProductValidator_ValidUserIdAndDto_DoesNotThrow()
    {
        // Arrange
        var userId = 1;

        var dto = new UpdateProductDTO
        {
            Price = 1,
            Name = "Product 1",
            Stock = 10
        };

        // Act & Assert
        new UpdateProductValidator().ValidateAndThrow((userId, dto));
    }
}