using Basket.Model;
using System.ComponentModel.DataAnnotations;

namespace WebShoping.Models
{
    public class BasketDataModel
    {
        public string BuyerId { get; set; } = string.Empty;
        public IEnumerable<BasketItem> Items { get; set; } 
        public BasketDataModel(string buyerId) => (BuyerId, Items) = (buyerId ?? string.Empty, new List<BasketItem>());
    }
}