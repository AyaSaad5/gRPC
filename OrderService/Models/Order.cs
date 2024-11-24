using System.Collections;
using System.Collections.Generic;

namespace OrderService.Models
{
    public class Order
    {
        public string OrderId { get; set; }
        public string UserId { get; set; }
        public IList<Items> items { get; set; }

        
    }
    public class Items
    {
        public string ItemId { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
    }
    public class OrderResponse
    {
        public string Status { get; set;}
        public string Message { get; set;}
    }
}
