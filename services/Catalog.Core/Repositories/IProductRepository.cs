using Catalog.Core.CatalogSpecs;
using Catalog.Core.Entities;

namespace Catalog.Core.Repositories;

public interface IProductRepository
{
    Task<Pagination<Product>> GetProducts(CatalogSpecParams specParams);
    Task<Product> GetProductById(string id);
    Task<IEnumerable<Product>> GetProductsByName(string name);
    Task<IEnumerable<Product>> GetProductsByType(string type);
    Task<IEnumerable<Product>> GetProductsByTypeId(string typeId);
    Task<IEnumerable<Product>> GetProductsByBrand(string brand);
    Task<IEnumerable<Product>> GetProductsByBrandId(string brandId);
    Task<bool> UpdateProduct(Product product);
    Task<bool> DeleteProduct(string id);
    Task<bool> DeleteProduct(Product product);
    Task<Product> CreateProduct(Product product);
}