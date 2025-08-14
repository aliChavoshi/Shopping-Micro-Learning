using Microsoft.EntityFrameworkCore;
using Ordering.Core.Entities;
using Ordering.Core.Repositories;
using Ordering.Infrastructure.Data;

namespace Ordering.Infrastructure.Services;

public class OrderRepository(OrderContext context) : GenericRepository<Order>(context), IOrderRepository
{
    public async Task<IEnumerable<Order>> GetOrdersByUserName(string userName)
    {
        return await context.Orders.Where(x => x.UserName == userName).ToListAsync();
    }
}