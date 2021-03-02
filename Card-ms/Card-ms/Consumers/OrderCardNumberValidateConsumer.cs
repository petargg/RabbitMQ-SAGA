using MassTransit;
using rabbitmq_message;
using System.Threading.Tasks;

namespace Card_ms.Consumers
{
    public class OrderCardNumberValidateConsumer :
     IConsumer<IOrderMessage>
    {
        public async Task Consume(ConsumeContext<IOrderMessage> context)
        {
            if (context.Message.ProductName.Contains("test"))
            {
                if (context.RequestId != null)
                    await context.RespondAsync<IOrderRejected>(new
                    {
                        OrderId = context.Message.OrderId,
                        Reason = $"Test cannot submit token: {context.Message.OrderId}"
                    });

                return;
            }

            if (context.RequestId != null)
            {
                await context.RespondAsync<IOrderAccepted>(new
                {
                    OrderId = context.Message.OrderId,
                });
            }
        }
    }
}
