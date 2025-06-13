using Catalog.Core.Entities;
using Catalog.Core.Repositories;

namespace Catalog.Infrastructure.Repositories;

public class TypeRepository : ITypeRepository
{
    public Task<IEnumerable<ProductType>> GetProductTypes()
    {
        throw new NotImplementedException();
    }
}