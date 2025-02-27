using product_api.DTO;
using product_api.Entities;

namespace product_api.Services;

public interface IProductService
{
    Task<IEnumerable<Product>> GetProductsAsync();
    Task<Product> GetProductAsync(int productId);
    Task CreateProductAsync(CreateProductDTO createProductDto);
    Task<bool> UpdateProductAsync(int productId, UpdateProductDTO updateProductDto);
    Task<bool> DeleteProductAsync(int productId);
}