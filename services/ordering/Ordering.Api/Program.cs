using Asp.Versioning;
using Ordering.Api.Extensions;
using Ordering.Application;
using Ordering.Infrastructure;
using Ordering.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
//Register Versioning
builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
});
builder.Services.AddOpenApi();
// Add Swagger services
builder.Services.AddEndpointsApiExplorer(); // برای پشتیبانی از endpoint ها
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "Basket.Api", Version = "v1", Description = "Basket API" });
});
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);

var app = builder.Build();
//Migration on DB
app.MigrationDatabase<OrderContext>((context, services) =>
{
    //var logger = services.GetService<ILogger<OrderSeedData>>();
    OrderSeedData.SeedAsync(context).Wait();
});
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.MapOpenApi();
    app.UseSwagger(); // فعال‌سازی Swagger
    app.UseSwaggerUI(); // فعال‌سازی رابط گرافیکی Swagger UI
}

app.UseAuthorization();
app.MapControllers();
app.Run();