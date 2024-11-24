using Grpc.Core;
using System;
using System.Threading.Tasks;

namespace PaymentService.Services
{
    public class PaymentService_: PaymentService.PaymentServiceBase
    {
        public override Task<StatusResponse> DeductUserBalance(DeductUserBalanceRequest request, ServerCallContext context)
        {
            Console.WriteLine($"Deduct {request.TotalPrice} from user {request.UserId}");
            return Task.FromResult(new StatusResponse { Status = "Success" });
        }
    }
}
