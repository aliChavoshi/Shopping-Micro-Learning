using Asp.Versioning;
using Catalog.Application.Mapper;
using Catalog.Application.Queries.Brands;
using Catalog.Core.Repositories;
using Catalog.Infrastructure.Data;
using Catalog.Infrastructure.Repositories;
using Common.Logging;
using Common.Logging.Correlations;
using Serilog;
using System.Reflection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
//Logging and ElasticSearch
builder.Host.UseSerilog(Logging.ConfigureLogger);
//Correlations
builder.Services.AddScoped<ICorrelationIdGenerator, CorrelationIdGenerator>();
builder.Services.AddHttpContextAccessor();

// Add services to the container.
builder.Services.AddControllers();

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
    typeof(GetAllProductBrandsQueryHandler).Assembly
};
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assemblies));
// builder.Services.AddTransient<IRequestHandler<DeleteProductCommand, bool>, DeleteProductCommandHandler>();
//DI
builder.Services.AddScoped<ICatalogContext, CatalogContext>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ITypeRepository, TypeRepository>();
builder.Services.AddScoped<IBrandRepository, BrandRepository>();
// Add Swagger services
builder.Services.AddEndpointsApiExplorer(); // برای پشتیبانی از endpoint ها
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "Catalog.Api", Version = "v1", Description = "Catalog API" });
});
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

#region Identity

var authorizationPolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
builder.Services.AddControllers(config =>
{
    // [Authorize]
    config.Filters.Add(new AuthorizeFilter(authorizationPolicy));
});
const string identityUrl = "https://host.docker.internal:9009"; // Identity URL for Docker
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = identityUrl;
        options.Audience = "Catalog";
        options.RequireHttpsMetadata = false; //https development
        options.BackchannelHttpHandler = new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
        };
        //Token Validation
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidAudiences =
            [
                "Catalog"
            ],
            ValidIssuers = [identityUrl]
        };
        //Logging
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

//TODO
// Custom authorization policies
// builder.Services.AddAuthorization(options =>
// {
//     options.AddPolicy("CanRead", policy => policy.RequireClaim("scope", "catalogapi.read"));
//     options.AddPolicy("CanWrite", policy => policy.RequireClaim("scope", "catalogapi.write"));
// });
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger(); // فعال‌سازی Swagger
    app.UseSwaggerUI(); // فعال‌سازی رابط گرافیکی Swagger UI
}

//Correlation Logging
app.AddCorrelationIdMiddleware();

app.UseAuthentication(); //1
app.UseAuthorization(); //2

app.MapControllers();
app.Run();