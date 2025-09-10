using Asp.Versioning;
using Basket.Application.Mapper;
using System.Reflection;
using Basket.Application.GrpcService;
using Basket.Application.Queries.GetBasket;
using Basket.Core.Repository;
using Basket.Infrastructure.Services;
using Discount.Application.Protos;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddOpenApi();

//GRPC
builder.Services.AddScoped<DiscountGrpcService>();
builder.Services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(options =>
{
    options.Address = new Uri(builder.Configuration["GrpcSettings:DiscountUrl"]!);
});
//RabbitMQ
builder.Services.AddMassTransit(configuration =>
{
    configuration.UsingRabbitMq(((context, cfg) =>
    {
        cfg.Host(builder.Configuration["EventBusSettings:HostAddress"]);
    }));
});
builder.Services.AddMassTransitHostedService();
//Register Automapper
builder.Services.AddAutoMapper(typeof(ProfileMapper));
//Register Versioning
builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
});
//Register Mediator
var assemblies = new Assembly[]
{
    Assembly.GetExecutingAssembly(),
    typeof(GetBasketByUserNameQueryHandler).Assembly
};
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assemblies));
builder.Services.AddScoped<IBasketRepository, BasketRepository>();

// Add Swagger services
builder.Services.AddEndpointsApiExplorer(); // برای پشتیبانی از endpoint ها
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "Basket.Api", Version = "v1", Description = "Basket API" });
});

//Redis Configuration
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
});
//Build
var app = builder.Build();

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