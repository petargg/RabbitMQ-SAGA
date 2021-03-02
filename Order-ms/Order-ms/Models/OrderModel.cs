using rabbitmq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Order_ms.Models
{
    public class OrderModel
    {
        public Guid Id { get; set; }
        public string ProductName { get; set; }
        public string CardNumber { get; set; }
        public DateTime CreatedAt { get; set; }
        public OrderStatus Status { get; set; }
    }
}
