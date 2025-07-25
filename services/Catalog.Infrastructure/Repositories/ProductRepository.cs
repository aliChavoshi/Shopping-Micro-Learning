using Catalog.Core.CatalogSpecs;
using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using Catalog.Infrastructure.Data;
using MongoDB.Driver;
using System.Net.Http.Headers;

namespace Catalog.Infrastructure.Repositories;

public class ProductRepository(ICatalogContext context) : IProductRepository
{
    public async Task<IEnumerable<Product>> GetProducts(CatalogSpecParams specParams)
    {
        //mongo => filters
        return await context.Products.Find(x => true).ToListAsync();
    }

    public async Task<Product> GetProductById(string id)
    {
        return await context.Products.Find(x => x.Id == id).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Product>> GetProductsByName(string name)
    {
        return await context.Products.Find(x => x.Brands.Name == name).ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetProductsByType(string type)
    {
        return await context.Products.Find(x => x.Types.Name == type).ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetProductsByTypeId(string typeId)
    {
        return await context.Products.Find(x => x.Types.Id == typeId).ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetProductsByBrand(string brand)
    {
        return await context.Products.Find(x => x.Brands.Name == brand).ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetProductsByBrandId(string brandId)
    {
        return await context.Products.Find(x => x.Brands.Id == brandId).ToListAsync();
    }

    public async Task<bool> UpdateProduct(Product product)
    {
        var result = await context.Products.ReplaceOneAsync(p => p.Id == product.Id, product);
        return result.IsAcknowledged && result.ModifiedCount > 0;
    }

    public async Task<bool> DeleteProduct(string id)
    {
        var result = await context.Products.DeleteOneAsync(x => x.Id == id);
        return result.IsAcknowledged && result.DeletedCount > 0;
    }

    public async Task<bool> DeleteProduct(Product product)
    {
        return await DeleteProduct(product.Id);
    }

    public async Task<Product> CreateProduct(Product product)
    {
        await context.Products.InsertOneAsync(product);
        return product;
    }
}