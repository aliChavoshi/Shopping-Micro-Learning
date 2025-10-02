using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using Basket.Api.SwaggerOptions;
using Basket.Application.GrpcService;
using Basket.Application.Mapper;
using Basket.Application.Queries.GetBasket;
using Basket.Core.Repository;
using Basket.Infrastructure.Services;
using Common.Logging;
using Common.Logging.Correlations;
using Discount.Application.Protos;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
//Logging and ElasticSearch
builder.Host.UseSerilog(Logging.ConfigureLogger);
//Correlations
builder.Services.AddScoped<ICorrelationIdGenerator, CorrelationIdGenerator>();
builder.Services.AddHttpContextAccessor();
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
//API Versioning
builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0); //Set Default API Versioning
    options.AssumeDefaultVersionWhenUnspecified = true; // default version using 
    options.ReportApiVersions = true; // add API Versioning to the response Header
}).AddApiExplorer(option =>
{
    option.SubstituteApiVersionInUrl = true; //Enable version Substitute in route URLs
});
// Add Swagger services
builder.Services.AddEndpointsApiExplorer(); // برای پشتیبانی از endpoint ها
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, SwaggerConfigOptions>();

//Register Mediator
var assemblies = new Assembly[]
{
    Assembly.GetExecutingAssembly(),
    typeof(GetBasketByUserNameQueryHandler).Assembly
};
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assemblies));
builder.Services.AddScoped<IBasketRepository, BasketRepository>();

//Redis Configuration
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
});

#region Identity

var authorizationPolicy = new AuthorizationPolicyBuilder()
    .RequireAuthenticatedUser()
    .Build();

builder.Services.AddControllers(config =>
{
    config.Filters.Add(new AuthorizeFilter(authorizationPolicy)); // Apply global authorization policy
});

// Configure JWT Bearer Authentication
const string identityUrl = "https://host.docker.internal:9009";
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        // IdentityServer URL برای Docker
        options.Authority = identityUrl;
        options.Audience = "Basket";
        options.RequireHttpsMetadata = false;
        options.BackchannelHttpHandler = new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
        };

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidAudiences = ["Basket"],
            ValidIssuers = [identityUrl]
        };

        options.IncludeErrorDetails = true;

        options.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = context =>
            {
                Console.WriteLine($"Auth failed: {context.Exception.Message}");
                return Task.CompletedTask;
            },
            OnTokenValidated = context =>
            {
                Console.WriteLine("Token validated successfully.");
                return Task.CompletedTask;
            }
        };
    });

#endregion
//Build
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.MapOpenApi();
    //Swagger UI Configuration
    app.UseSwagger(); // generate json file
    app.UseSwaggerUI(c =>
    {
        var versionDocProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
        foreach (var desc in versionDocProvider.ApiVersionDescriptions)
        {
            c.SwaggerEndpoint($"{desc.GroupName}/swagger.json", $"Basket.API - {desc.GroupName}");
        }
    }); // فعال‌سازی رابط گرافیکی Swagger UI
}

//Correlation Logging
app.AddCorrelationIdMiddleware();
app.UseAuthentication(); // Identity
app.UseAuthorization();
app.MapControllers();
app.Run();