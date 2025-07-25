using Catalog.Core.Entities;
using MongoDB.Driver;
using System.Text.Json;

namespace Catalog.Infrastructure.Data;

public static class ProductSeedData
{
    public static void SeedData(IMongoCollection<Product> collection)
    {
        var existCollection = collection.Find(x => true).Any();
        if (existCollection) return;
        var pathJson = Path.Combine(AppContext.BaseDirectory, "Data", "SeedData", "products.json");
        if (!File.Exists(pathJson))
            throw new Exception($"the seed data of the products.json not found at path :{pathJson}");

        var dataText = File.ReadAllText(pathJson);
        var brands = JsonSerializer.Deserialize<List<Product>>(dataText);
        if (brands != null) collection.InsertMany(brands);
    }
}