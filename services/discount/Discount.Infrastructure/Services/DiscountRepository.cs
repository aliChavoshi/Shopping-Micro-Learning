using Dapper;
using Discount.Core.Entities;
using Discount.Core.Interfaces;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Discount.Infrastructure.Services;

public class DiscountRepository(IConfiguration configuration) : IDiscountRepository
{
    private readonly string? _connectionString = configuration.GetValue<string>("DatabaseSettings:ConnectionString");

    public async Task<Coupon> GetDiscount(string productId)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        const string sql = "SELECT * FROM Coupon WHERE ProductId = @ProductId";
        var coupon = await connection.QueryFirstOrDefaultAsync<Coupon>(sql, new { ProductId = productId });
        return coupon ?? new Coupon();
    }

    public async Task<Coupon> GetDiscountByName(string productName)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        const string sql = "SELECT * FROM Coupon WHERE ProductName = @ProductName";
        var coupon = await connection.QueryFirstOrDefaultAsync<Coupon>(sql, new { ProductName = productName });
        return coupon ?? new Coupon();
    }

    public async Task<bool> CreateDiscount(Coupon coupon)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        const string sql =
            "INSERT INTO Coupon (ProductName, ProductId, Description, Amount) VALUES (@ProductName, @ProductId, @Description, @Amount)";
        var parameters = new { coupon.ProductName, coupon.ProductId, coupon.Description, coupon.Amount };
        var affected = await connection.ExecuteAsync(sql, parameters);
        return affected > 0;
    }

    public async Task<bool> UpdateDiscount(Coupon coupon)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        const string sql =
            "UPDATE Coupon SET ProductName=@ProductName, ProductId=@ProductId, Description=@Description, Amount=@Amount WHERE Id=@Id";
        var parameters = new { coupon.ProductName, coupon.ProductId, coupon.Description, coupon.Amount, coupon.Id };
        var affected = await connection.ExecuteAsync(sql, parameters);
        return affected > 0;
    }

    public async Task<bool> DeleteDiscount(string productId)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        const string sql = "DELETE FROM Coupon WHERE ProductId=@ProductId";
        var affected = await connection.ExecuteAsync(sql, new { ProductId = productId });
        return affected > 0;
    }

    public async Task<bool> DeleteDiscountByName(string productName)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        const string sql = "DELETE FROM Coupon WHERE ProductName=@ProductName";
        var affected = await connection.ExecuteAsync(sql, new { ProductName = productName });
        return affected > 0;
    }
}