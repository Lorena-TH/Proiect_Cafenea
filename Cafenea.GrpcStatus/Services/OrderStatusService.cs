using Grpc.Core;
using Cafenea.GrpcStatus;

namespace Cafenea.GrpcStatus.Services
{

    public class OrderStatusService : OrderStatus.OrderStatusBase
    {
        public override Task<OrderReply> GetStatus(OrderRequest request, ServerCallContext context)
        {
            var reply = new OrderReply();


            if (request.OrderId % 3 == 0)
            {
                reply.Mesaj = "STOC EPUIZAT! Aprovizionare necesară.";
                reply.TimpEstimativ = "Reaprovizionare: 48 ore";
                reply.EsteGata = false;
            }
            else
            {
                reply.Mesaj = "STOC DISPONIBIL. Produsul poate fi vândut.";
                reply.TimpEstimativ = "Cantitate: > 20 unități";
                reply.EsteGata = true;
            }

            return Task.FromResult(reply);
        }
    }
}