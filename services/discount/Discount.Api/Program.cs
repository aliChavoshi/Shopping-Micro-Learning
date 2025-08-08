using Discount.Application.Mapper;
using Discount.Infrastructure.Extensions;
using System.Reflection;
using Discount.Api.Services;
using Discount.Application.Queries;
using Discount.Core.Interfaces;
using Discount.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllers();
//Register Mapper
builder.Services.AddAutoMapper(typeof(DiscountProfile));
//Register Mediator
var assemblies = new Assembly[]
{
    Assembly.GetExecutingAssembly(),
    typeof(GetDiscountByNameQueryHandler).Assembly
};
//DI
builder.Services.AddScoped<IDiscountRepository, DiscountRepository>();
//GRPC
builder.Services.AddGrpc();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assemblies));
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();
app.MigrateDatabase<Program>();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    // app.MapOpenApi();
}

app.UseRouting();
app.MapGrpcService<DiscountService>();
app.Map("/", async context =>
{
    await context.Response.WriteAsync("communication with GRPC");
});
// app.UseAuthorization();
// app.MapControllers();

app.Run();
