using InventoryService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderService.Models;
using PaymentService;
using System.Linq;
using System.Threading.Tasks;


namespace OrderService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly PaymentService.PaymentService.PaymentServiceClient paymentClient;
        private readonly InventoryService.InventoryService.InventoryServiceClient inventoryClient;

        public OrderController(PaymentService.PaymentService.PaymentServiceClient paymentServiceClient,
                               InventoryService.InventoryService.InventoryServiceClient inventoryServiceClient)
        {
            paymentClient = paymentServiceClient;
            inventoryClient = inventoryServiceClient;
        }
        [HttpPost]
        public async Task<OrderResponse> SubmitOrder([FromBody] Order request)
        {
            double totalPrice = request.items.Sum(item => item.Price * item.Quantity);

            // Deduct balance
            var paymentResponse =  paymentClient.DeductUserBalance(new DeductUserBalanceRequest
            {
                UserId = request.UserId,
                TotalPrice = totalPrice
            });

            if (paymentResponse.Status != "Success")
            {
                return new OrderResponse { Status = "Failure", Message = "Payment failed"};
            }

            // Deduct items from inventory
            var inventoryResponse = await inventoryClient.DeductItemsBalanceAsync(new DeductItemsRequest
            {
                Items =
                { request.items.Select(item => new Item 
                  {
                      ItemId = item.ItemId,
                      Quantity = item.Quantity 
                  })
                }
            });

            if (inventoryResponse.Status != "Success")
            {
                return new OrderResponse { Status = "Failure", Message = "Inventory update failed:"};
            }

            return new OrderResponse { Status = "Success", Message = "Order processed successfully" };
        }
    }
}
