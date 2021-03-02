using System;
using System.Collections.Generic;
using System.Text;

namespace rabbitmq_message
{
    public interface IOrderAccepted
    {
        Guid OrderId { get; }
    }
}
