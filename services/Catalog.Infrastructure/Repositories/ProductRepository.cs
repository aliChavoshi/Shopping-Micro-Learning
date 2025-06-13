using Catalog.Core.Entities;
using Catalog.Core.Repositories;

namespace Catalog.Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    public Task<IEnumerable<Product>> GetProducts()
    {
        throw new NotImplementedException();
    }

    public Task<Product> GetProductById(string id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Product>> GetProductsByName(string name)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Product>> GetProductsByType(string type)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Product>> GetProductsByTypeId(string typeId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Product>> GetProductsByBrand(string brand)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Product>> GetProductsByBrandId(string brandId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateProduct(Product product)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteProduct(string id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteProduct(Product product)
    {
        throw new NotImplementedException();
    }
}