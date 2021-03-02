using System;
using System.Collections.Generic;
using System.Text;

namespace rabbitmq_message
{
    public class IOrderMessage
    {
        public Guid OrderId { get; set; }
        public string PaymentCardNumber { get; set; }
        public string ProductName { get; set; }
    }
}
