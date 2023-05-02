using Microsoft.Extensions.Options;
using WebShoping.Services;
using WebShoping.Services.Interfaces;
using static Basket.Services.Basket;

namespace WebShoping
{
    public static class ServicesCollectionExtension
    {
        public static IServiceCollection AddGrpcServices(this IServiceCollection services)
        {
            services.AddScoped<IBasketService, BasketService>();
            services.AddGrpcClient<BasketClient>((serviceProvider, option) =>
            {
                var basketApi = "http://localhost:5001";
                option.Address = new Uri(basketApi);
            });
            return services;
        }
    }
}
