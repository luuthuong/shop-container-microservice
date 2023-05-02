using Basket.Services;
using WebShoping.Models;

namespace WebShoping.Services.Interfaces
{
    public interface IBasketService
    {
        Task<BasketDataModel> GetByIdAsync(string id);
        Task UpdateBasketAsync(BasketDataModel basket);
    }
}
