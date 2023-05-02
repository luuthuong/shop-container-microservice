using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebShoping.Models;
using WebShoping.Services.Interfaces;

namespace WebShoping.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketGatewayController : ControllerBase
    {
        private readonly IBasketService _basketService;
        public BasketGatewayController(IBasketService basketService)
        {
            _basketService = basketService;
        }

        [HttpGet]
        public Task<BasketDataModel> GetBasketById(string id)
        {
            return _basketService.GetByIdAsync(id); 
        }
    }
}
