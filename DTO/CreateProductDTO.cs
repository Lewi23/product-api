namespace product_api.DTO;

public class CreateProductDTO
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
}