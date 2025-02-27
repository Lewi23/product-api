namespace product_api.Services;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddProductService(this IServiceCollection services)
    {
        return services.AddScoped<IProductService, ProductService>();
    }
}