using Ordering.Core.Entities;
using Ordering.Core.Repositories;
using Ordering.Infrastructure.Data;

namespace Ordering.Infrastructure.Services;

public class OrderRepository(OrderContext context) : GenericRepository<Order>(context), IOrderRepository
{
    public Task<IEnumerable<Order>> GetOrdersByUserName(string userName)
    {
        throw new NotImplementedException();
    }
}