using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Order_ms.DataAccess;
using Order_ms.Models;
using rabbitmq;
using rabbitmq_message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Order_ms.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : Controller
    {
        //private readonly ISendEndpointProvider _sendEndpointProvider;
        private readonly IDataAccess da;

        private readonly IRequestClient<IOrderMessage> _submitOrderRequestClient;

        public OrderController(
         IRequestClient<IOrderMessage> submitOrderRequestClient, 
          IDataAccess da)
        {
            //_sendEndpointProvider = sendEndpointProvider;
            this.da = da;
            _submitOrderRequestClient = submitOrderRequestClient;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] OrderModel orderModel)
        {
            orderModel.Id = Guid.NewGuid();
            orderModel.CreatedAt = DateTime.UtcNow;
            orderModel.Status = OrderStatus.Accepted;

            await da.SaveOrder(orderModel);

            var (accepted, rejected) = await _submitOrderRequestClient.GetResponse<IOrderAccepted, IOrderRejected>(new
            {
                OrderId = orderModel.Id,
                PaymentCardNumber = orderModel.CardNumber,
                ProductName = orderModel.ProductName
            });

            if (accepted.IsCompletedSuccessfully)
            {
                var response = await accepted;
                await ChangeOrderStatus(response.Message.OrderId, OrderStatus.Submitted);
                return Accepted(response);
            }

            if (accepted.IsCompleted)
            {
                var response = await accepted;
                await ChangeOrderStatus(response.Message.OrderId, OrderStatus.Rejected);
                return Problem("Token was not accepted");
            }
            else
            {
                var response = await rejected;
                await ChangeOrderStatus(response.Message.OrderId, OrderStatus.Rejected);
                return BadRequest(response.Message.Reason);
            }
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(da.GetOrders());
        }

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            return Ok(await da.GetOrder(id));
        }

        private async Task ChangeOrderStatus(Guid orderId, OrderStatus status)
        {
            var order = await da.GetOrder(orderId);
            order.Status = status;
            await da.UpdateOrder(order);
        }
    }
}
