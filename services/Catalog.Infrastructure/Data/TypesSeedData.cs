using Catalog.Core.Entities;
using MongoDB.Driver;
using System.Text.Json;

namespace Catalog.Infrastructure.Data;

public static class TypesSeedData
{
    public static void SeedData(IMongoCollection<ProductType> collection)
    {
        var existCollection = collection.Find(x => true).Any();
        if (existCollection) return;
        var pathJson = Path.Combine(AppContext.BaseDirectory, "Data", "SeedData", "types.json");
        if (!File.Exists(pathJson))
            throw new Exception($"the seed data of the types.json not found at path :{pathJson}");

        var dataText = File.ReadAllText(pathJson);
        var brands = JsonSerializer.Deserialize<List<ProductType>>(dataText);
        if (brands != null) collection.InsertMany(brands);
    }
}