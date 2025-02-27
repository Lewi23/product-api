using Microsoft.EntityFrameworkCore;
using product_api.DTO;
using product_api.Entities;
using product_api.Persistence;

namespace product_api.Services;

public class ProductService : IProductService
{
    private readonly AppDbContext _dbContext;

    public ProductService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Product>> GetProductsAsync()
    {
        return await _dbContext.Products
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Product> GetProductAsync(int productId)
    {
        return await _dbContext.Products
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == productId);
    }

    // Method could return created product
    public async Task CreateProductAsync(CreateProductDTO createProductDto)
    {
        _dbContext.Products.Add(new Product
        {
            Name = createProductDto.Name,
            Price = createProductDto.Price,
            Stock = createProductDto.Stock
        });

        await _dbContext.SaveChangesAsync();
    }

    // Using bool for simplicity, could use result pattern etc
    public async Task<bool> UpdateProductAsync(int productId, UpdateProductDTO updateProductDTO)
    {
        var productToUpdate = await _dbContext.Products
            .FirstOrDefaultAsync(x => x.Id == productId);

        if (productToUpdate is null)
        {
            return false;
        }

        productToUpdate.Name = updateProductDTO.Name;
        productToUpdate.Price = updateProductDTO.Price;
        productToUpdate.Stock = updateProductDTO.Stock;

        await _dbContext.SaveChangesAsync();
        return true;
    }

    // Using bool for simplicity, could use result pattern etc
    public async Task<bool> DeleteProductAsync(int productId)
    {
        var productToDelete = await _dbContext.Products
            .FirstOrDefaultAsync(x => x.Id == productId);

        if (productToDelete is null)
        {
            return false;
        }

        _dbContext.Products.Remove(productToDelete);
        await _dbContext.SaveChangesAsync();
        return true;
    }
}