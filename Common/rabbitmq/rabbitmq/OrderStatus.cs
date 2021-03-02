using System;
using System.Collections.Generic;
using System.Text;

namespace rabbitmq
{
    public enum OrderStatus
    {
        Accepted,
        Pending,
        Submitted, 
        Rejected
    }
}
