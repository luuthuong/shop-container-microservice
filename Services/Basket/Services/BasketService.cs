using Basket.Model;
using Grpc.Core;

namespace Basket.Services
{
    public class BasketService : Basket.BasketBase
    {
        private readonly ILogger<BasketService> _logger;

        public BasketService(ILogger<BasketService> logger)
        {
            _logger = logger;
        }

        public override async Task<CustomerBasketResponse> GetBasketById(BasketRequest request, ServerCallContext context)
        {
            _logger.LogInformation($"Begin grpc call from method {context.Method} for basket id {request.Id}");
            var customerBasketResponse = new CustomerBasketResponse()
            {
                BuyerId = "0"
            };
            for(int i =0; i <= 10; i++)
            {
                customerBasketResponse.Items.Add(new BasketItemResponse
                {
                    Id = i.ToString(),
                    Productid = i,
                    Productname = $"Product name {i}",
                    Quantity = i* 10,
                    Unitprice = i * 100
                });
            }
            return customerBasketResponse;
        }
    }
}
