using FluentValidation;
using product_api.DTO;
using product_api.Validators;
using Xunit;

namespace product_api.Tests.Validators;

public class CreateProductValidatorTests
{
    // TODO: Create Test case for testing product name min/max length - would normally use a faker lib to generate the strings

    [Fact]
    public void CreateProductValidator_InvalidPrice_ThrowsValidationException()
    {
        // Arrange
        var dto = new CreateProductDTO
        {
            Price = -1,
            Name = "Product 1",
            Stock = 10
        };

        // Act & Assert
        Assert.Throws<ValidationException>(() => new CreateProductValidator().ValidateAndThrow(dto));
    }

    [Fact]
    public void CreateProductValidator_InvalidName_ThrowsValidationException()
    {
        // Arrange
        var dto = new CreateProductDTO
        {
            Price = 1,
            Name = null,
            Stock = 10
        };

        // Act & Assert
        Assert.Throws<ValidationException>(() => new CreateProductValidator().ValidateAndThrow(dto));
    }

    [Fact]
    public void CreateProductValidator_InvalidStock_ThrowsValidationException()
    {
        // Arrange
        var dto = new CreateProductDTO
        {
            Price = 1,
            Name = "Product 1",
            Stock = -1
        };

        // Act & Assert
        Assert.Throws<ValidationException>(() => new CreateProductValidator().ValidateAndThrow(dto));
    }

    [Fact]
    public void CreateProductValidator_ValidDto_DoesNotThrow()
    {
        // Arrange
        var dto = new CreateProductDTO
        {
            Price = 1,
            Name = "Product 1",
            Stock = 10
        };

        // Act & Assert
        new CreateProductValidator().ValidateAndThrow(dto);
    }
}