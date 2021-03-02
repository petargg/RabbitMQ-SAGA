using Order_ms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Order_ms.DataAccess
{
    public interface IDataAccess
    {
        List<OrderModel> GetOrders();
        Task<OrderModel> GetOrder(Guid id);
        Task<bool> SaveOrder(OrderModel order);
        Task<bool> UpdateOrder(OrderModel order);
    }
}
