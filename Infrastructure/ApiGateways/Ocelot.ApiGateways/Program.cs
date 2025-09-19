using Common.Logging;
using Common.Logging.Correlations;
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
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
app.UseRouting();
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