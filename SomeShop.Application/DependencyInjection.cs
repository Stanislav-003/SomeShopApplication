using Microsoft.Extensions.DependencyInjection;
using SomeShop.Application.Abstractions.Behaviors;

namespace SomeShop.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssemblies(typeof(DependencyInjection).Assembly);

            configuration.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });

        return services;
    }
}
