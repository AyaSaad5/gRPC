
using Grpc.Core;
using System;
using System.Threading.Tasks;

namespace InventoryService.Services
{
    public class InventoryService_: InventoryService.InventoryServiceBase
    {
        public override Task<StatusResponse> DeductItemsBalance(DeductItemsRequest request, ServerCallContext context)
        {
            foreach (var item in request.Items)
            {
                Console.WriteLine($"Deducting {item.Quantity} of item {item.ItemId}");
            }
            return Task.FromResult(new StatusResponse { Status = "Success" });
        }
    }
}
