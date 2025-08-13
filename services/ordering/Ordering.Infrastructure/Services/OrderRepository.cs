using Ordering.Core.Entities;
using Ordering.Core.Repositories;

namespace Ordering.Infrastructure.Services;

public class OrderRepository : GenericRepository<Order>,IOrderRepository
{
    public Task<IEnumerable<Order>> GetOrdersByUserName(string userName)
    {
        throw new NotImplementedException();
    }
}