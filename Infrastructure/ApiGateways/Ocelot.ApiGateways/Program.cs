using Common.Logging;
using Common.Logging.Correlations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Ocelot.ApiGateways.Handlers;
using Ocelot.Cache.CacheManager;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Host.UseSerilog(Logging.ConfigureLogger);
// -----------------------------
// Add Correlation ID
// -----------------------------
builder.Services.AddTransient<ICorrelationIdGenerator, CorrelationIdGenerator>();
builder.Services.AddTransient<CorrelationDelegateHandler>(); //TODO
builder.Services.AddHttpContextAccessor();

builder.Services.AddOcelot()
    .AddDelegatingHandler<CorrelationDelegateHandler>()
    .AddCacheManager(o => o.WithDictionaryHandle());
// -----------------------------
// بارگذاری پیکربندی Ocelot
// -----------------------------
builder.Host.ConfigureAppConfiguration((env, config) =>
{
    var environmentName = env.HostingEnvironment?.EnvironmentName ?? "Development";
    config.AddJsonFile($"ocelot.{environmentName}.json", optional: true, reloadOnChange: true);
});
// -----------------------------
// End Correlation ID
// -----------------------------

#region Identity

const string identityUrl = "https://host.docker.internal:9009";
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer("CatalogAuthScheme", options =>
    {
        options.Authority = identityUrl;
        options.Audience = "Catalog";
        options.RequireHttpsMetadata = false; //dev
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = identityUrl
        };
        options.BackchannelHttpHandler = new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
        };
        options.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = ctx =>
            {
                Log.Error(ctx.Exception, "JWT Authentication failed for CatalogAuthScheme");
                return Task.CompletedTask;
            },
            OnChallenge = ctx =>
            {
                Log.Warning(
                    "JWT Challenge triggered for CatalogAuthScheme. Error: {Error}, Description: {ErrorDescription}",
                    ctx.Error, ctx.ErrorDescription);
                return Task.CompletedTask;
            },
            OnForbidden = ctx =>
            {
                Log.Warning("JWT Forbidden for CatalogAuthScheme. User: {User}", ctx.HttpContext.User?.Identity?.Name);
                return Task.CompletedTask;
            }
        };
    });
    // .AddJwtBearer("BasketAuthScheme", options =>
    // {
    //     //options.Authority = "https://localhost:9009";
    //     options.Authority = "https://host.docker.internal:9009";
    //     options.Audience = "Basket";
    //     options.RequireHttpsMetadata = false;
    //     options.TokenValidationParameters = new TokenValidationParameters
    //     {
    //         ValidateIssuer = true,
    //         ValidIssuer = "https://host.docker.internal:9009"
    //     };
    //     options.BackchannelHttpHandler = new HttpClientHandler
    //     {
    //         ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
    //     };
    //     options.Events = new JwtBearerEvents
    //     {
    //         OnAuthenticationFailed = ctx =>
    //         {
    //             Log.Error(ctx.Exception, "JWT Authentication failed for BasketAuthScheme");
    //             return Task.CompletedTask;
    //         },
    //         OnChallenge = ctx =>
    //         {
    //             Log.Warning(
    //                 "JWT Challenge triggered for BasketAuthScheme. Error: {Error}, Description: {ErrorDescription}",
    //                 ctx.Error, ctx.ErrorDescription);
    //             return Task.CompletedTask;
    //         },
    //         OnForbidden = ctx =>
    //         {
    //             Log.Warning("JWT Forbidden for BasketAuthScheme. User: {User}", ctx.HttpContext.User?.Identity?.Name);
    //             return Task.CompletedTask;
    //         }
    //     };
    // });

#endregion

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseRouting();
//Identity Server
app.UseAuthentication();
app.UseAuthorization(); 
// Middleware برای افزودن CorrelationId
app.AddCorrelationIdMiddleware();
//Controller
app.MapControllers();

app.MapGet("/", () => "Hello World!");
app.Use(async (context, next) =>
{
    var correlation = context.RequestServices.GetRequiredService<ICorrelationIdGenerator>();
    var correlationId = correlation.Get();
    Log.Information("Middleware CorrelationId In Ocelot: {correlationId}", correlationId);

    try
    {
        await next();
    }
    catch (Exception ex)
    {
        Log.Error(ex, "Unhandled exception in Ocelot");
        throw;
    }
});
//Ocelot
await app.UseOcelot();
//Running
await app.RunAsync();