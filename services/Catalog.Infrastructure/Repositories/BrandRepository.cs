using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using Catalog.Infrastructure.Data;

namespace Catalog.Infrastructure.Repositories;

public class BrandRepository(ICatalogContext context) : IBrandRepository
{
    public Task<IEnumerable<ProductBrand>> GetProductBrands()
    {
        throw new NotImplementedException();
    }
}