using Microsoft.EntityFrameworkCore;
using product_api.DTO;
using product_api.Entities;
using product_api.Persistence;
using product_api.Services;
using Xunit;

namespace product_api.Tests.Services;

public class ProductServiceTests
{
    private readonly AppDbContext _dbContext;
    private readonly IProductService _productService;

    public ProductServiceTests()
    {
        // Limitations when using an in memory db
        var options = new DbContextOptionsBuilder<AppDbContext>().
            UseInMemoryDatabase("TestDB")
            .Options;

        _dbContext = new AppDbContext(options);
        _productService = new ProductService(_dbContext);
    }

    [Fact]
    public async Task GetProductsAsync_ReturnsProducts()
    {
        // Arrange
        var products = new List<Product>
        {
            new("Product 1", 1M, 10),
            new("Product 2", 10M, 100)
        };

        await _dbContext.Database.EnsureDeletedAsync();

        _dbContext.Products.AddRange(products);
        await _dbContext.SaveChangesAsync();

        // Act
        var result = await _productService.GetProductsAsync();

        // Assert
        Assert.IsType<IEnumerable<Product>>(result, exactMatch: false);
        Assert.Equal(products.Count, result.Count());
    }

    [Fact]
    public async Task GetProductAsync_ProductDoesNotExist_ReturnsNull()
    {
        // Arrange
        var productId = 1;

        await _dbContext.Database.EnsureDeletedAsync();

        // Act
        var result = await _productService.GetProductAsync(productId);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetProductAsync_ProductExists_ReturnsProduct()
    {
        // Arrange
        var productId = 1;

        var product = new Product("Product 1", 1M, 10);

        await _dbContext.Database.EnsureDeletedAsync();

        _dbContext.Products.AddRange(product);
        await _dbContext.SaveChangesAsync();

        // Act
        var result = await _productService.GetProductAsync(productId);

        // Assert
        Assert.IsType<Product>(result);
    }

    [Fact]
    public async Task CreateProductAsync_CreatesEntry()
    {
        // Arrange
        var dto = new CreateProductDTO
        {
            Name = "Product 1",
            Price = 10M,
            Stock = 1
        };

        await _dbContext.Database.EnsureDeletedAsync();

        // Act
        var task = _productService.CreateProductAsync(dto);
        await task;

        // Assert
        Assert.True(task.IsCompleted);

        var createdProduct = await _dbContext.Products
            .FirstOrDefaultAsync(x => x.Id == 1);

        Assert.IsType<Product>(createdProduct);
    }

    [Fact]
    public async Task UpdateProductAsync_ProductDoesNotExist_ReturnsFalse()
    {
        // Arrange
        var productId = 1;
        var dto = new UpdateProductDTO();

        await _dbContext.Database.EnsureDeletedAsync();

        // Act
        var result = await _productService.UpdateProductAsync(productId, dto);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task UpdateProductAsync_ProductExist_UpdatesProductAndReturnsTrue()
    {
        // Arrange
        var productId = 1;
        var dto = new UpdateProductDTO
        {
            Name = "Product 2",
            Price = 2M,
            Stock = 5
        };

        var product = new Product("Product 1", 1M, 10);

        await _dbContext.Database.EnsureDeletedAsync();

        _dbContext.Products.AddRange(product);
        await _dbContext.SaveChangesAsync();

        // Act
        var result = await _productService.UpdateProductAsync(productId, dto);

        // Assert
        Assert.True(result);

        var updatedProduct = await _dbContext.Products
            .FirstOrDefaultAsync(x => x.Id == productId);

        Assert.Equal(dto.Name, product.Name);
        Assert.Equal(dto.Price, product.Price);
        Assert.Equal(dto.Stock, product.Stock);
    }

    [Fact]
    public async Task DeleteProductAsync_ProductDoesNotExist_ReturnsFalse()
    {
        // Arrange
        var productId = 1;

        await _dbContext.Database.EnsureDeletedAsync();

        // Act
        var result = await _productService.DeleteProductAsync(productId);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task DeleteProductAsync_ProductExists_DeletesProductAndReturnsTrue()
    {
        // Arrange
        var productId = 1;

        var product = new Product("Product 1", 1M, 10);

        await _dbContext.Database.EnsureDeletedAsync();

        _dbContext.Products.AddRange(product);
        await _dbContext.SaveChangesAsync();

        // Act
        var result = await _productService.DeleteProductAsync(productId);

        // Assert
        Assert.True(result);

        var productExists = await _dbContext.Products
            .AnyAsync(x => x.Id == productId);

        Assert.False(productExists);
    }
}