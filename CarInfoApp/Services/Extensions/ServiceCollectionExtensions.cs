namespace CarInfoApp.Services.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddNhtsaApiService(this IServiceCollection services)
    {
        services.AddHttpClient<INhtsaService, NhtsaService>();
        return services;
    }
}
