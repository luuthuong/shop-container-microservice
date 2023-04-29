namespace Basket.Services
{
    public class BasketService : Basket.BasketBase
    {
        private readonly ILogger<BasketService> _logger;

        public BasketService(ILogger<BasketService> logger)
        {
            _logger = logger;
        }

        public void GetBasketById(CustomerBasketRequest request)
        {

        }
    }
}
