using Basket.Model;
using Basket.Services;
using System.Linq;
using WebShoping.Models;
using WebShoping.Services.Interfaces;
using static Basket.Services.Basket;

namespace WebShoping.Services
{
    public class BasketService : IBasketService
    {
        private readonly BasketClient _basketClient;

        public BasketService(BasketClient basketClient)
        {
            _basketClient = basketClient;
        }

        public async Task<BasketDataModel> GetByIdAsync(string id)
        {
            var response = await _basketClient.GetBasketByIdAsync(new BasketRequest { Id = id });
            return MapToBasketDataModel(response);
        }
        public Task UpdateBasketAsync(BasketDataModel basket)
        {
            throw new NotImplementedException();
        }

        private BasketDataModel MapToBasketDataModel(CustomerBasketResponse response)
        {
            if (response == null)
                return default;
            var basketDataModel = new BasketDataModel(response.BuyerId);
            if(basketDataModel.Items != null)
            {
                foreach (var item in response.Items)
                {
                    basketDataModel.Items.Concat(new[] {
                        new BasketItem
                        {
                            Id = item.Id,
                            ProductId = item.Productid,
                            ProductName = item.Productname,
                            Quantity = item.Quantity,
                            UnitPrice = item.Unitprice
                        }
                    }) ;
                }
            }
            return basketDataModel;
        }
    }
}
