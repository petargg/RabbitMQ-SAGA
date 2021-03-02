using System;
using System.Collections.Generic;
using System.Text;

namespace rabbitmq_message
{
    public interface IOrderRejected
    {
        Guid OrderId { get; }

        string Reason { get; }
    }
}
