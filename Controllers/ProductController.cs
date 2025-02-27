using Microsoft.AspNetCore.Mvc;
using product_api.DTO;
using product_api.Services;

namespace product_api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase // Could introduce custom base controller to handle responses / exceptions
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    /// <summary>
    /// Retrieve a list of products
    /// </summary>
    /// <returns>A list of all products</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]  // No products found could be a 404? Assumed 200
    public async Task<IActionResult> GetProducts() // Could consider paging here if data set was large
    {
        var products = await _productService.GetProductsAsync();
        return Ok(products);
    }

    /// <summary>
    /// Retrieve a single product
    /// </summary>
    /// <param name="id">The id of the product to be retrieved</param>
    /// <returns>A single product</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetProduct(int id)
    {
        var product = await _productService.GetProductAsync(id);
        if (product is null)
        {
            return NotFound($"Product not found with id: {id}");
        }

        return Ok(product);
    }

    /// <summary>
    /// Creates a new product
    /// </summary>
    /// <param name="createProductDto">A DTO containing the properties of the new product</param>
    /// <returns>201 Created</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateProduct([FromQuery] CreateProductDTO createProductDto)
    {
        await _productService.CreateProductAsync(createProductDto);
        return Created();
    }

    /// <summary>
    /// Update an existing product
    /// </summary>
    /// <param name="id">The id of the product to be updated</param>
    /// <param name="updateProductDto">A DTO containing the properties to be used to update the product</param>
    /// <returns></returns>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateProduct(int id, [FromBody] UpdateProductDTO updateProductDto)
    {
        var success = await _productService.UpdateProductAsync(id, updateProductDto);
        if (success is false)
        {
            return NotFound($"Product not found with id: {id}");
        }

        return NoContent();
    }

    /// <summary>
    /// Delete a product
    /// </summary>
    /// <param name="id">The id of the product to be deleted</param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var success = await _productService.DeleteProductAsync(id);
        if (success is false)
        {
            return NotFound($"Product not found with id: {id}");
        }

        return NoContent();
    }
}