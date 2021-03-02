using Order_ms.Data;
using Order_ms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Order_ms.DataAccess
{
    public class DataAccess: IDataAccess
    {
        private readonly OrderContext ctx;
        public DataAccess(OrderContext conext)
        {
            ctx = conext;
        }

        public List<OrderModel> GetOrders()
        {
            return ctx.Orders.ToList();
        }

        public async Task<OrderModel> GetOrder(Guid id)
        {
            return await ctx.Orders.FindAsync(id);
        }

        public async Task<bool> SaveOrder(OrderModel order)
        {
            await ctx.Orders.AddAsync(order);
            return (await ctx.SaveChangesAsync()) > 0;
        }

        public async Task<bool> UpdateOrder(OrderModel order)
        {
            ctx.Orders.Update(order);
            return (await ctx.SaveChangesAsync()) > 0;
        }
    }
}
