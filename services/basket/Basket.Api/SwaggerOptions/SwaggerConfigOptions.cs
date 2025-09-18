using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Basket.Api.SwaggerOptions;

public class SwaggerConfigOptions(IApiVersionDescriptionProvider descriptionProvider)
    : IConfigureOptions<SwaggerGenOptions>
{
    public void Configure(SwaggerGenOptions options)
    {
        foreach (var desc in descriptionProvider.ApiVersionDescriptions)
        {
            options.SwaggerDoc(desc.GroupName, new OpenApiInfo()
            {
                Title = "Basket API",
                Version = desc.ApiVersion.ToString(),
                Contact = new OpenApiContact()
                {
                    Email = "ali@ali.com",
                    Name = "Ali Chavoshi"
                },
                Description = "This is Swagger for Basket API",
                License = new OpenApiLicense()
                {
                    Name = "MIT"
                }
            });
        }
    }
}