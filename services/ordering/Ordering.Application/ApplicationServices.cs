using FluentValidation;
using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Application.Behavior;
using Ordering.Application.Mapping;
using System.Reflection;

namespace Ordering.Application;

public static class ApplicationServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        #region Mapster

        //Start Mapster
        var typeAdapterConfig = TypeAdapterConfig.GlobalSettings;
        typeAdapterConfig.Scan(typeof(OrderMapper).Assembly);

        services.AddSingleton(typeAdapterConfig);
        services.AddScoped<IMapper, ServiceMapper>();
        //End Mapster

        #endregion

        #region Mediator

        services.AddMediatR(c =>
            c.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        #endregion

        #region Validators

        //Validators
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        #endregion

        #region Pipeline

        //PipeLine
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationsBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnHandledExceptionBehavior<,>));

        #endregion

        return services;
    }
}