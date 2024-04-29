using Castle.DynamicProxy;
using Microsoft.Extensions.DependencyInjection;
using ExecutionLens.Logging.APPLICATION.Attributes;
using ExecutionLens.Logging.APPLICATION.Contracts;
using ExecutionLens.Logging.APPLICATION.Implementations;
using ExecutionLens.Logging.DOMAIN.Configurations;
using ExecutionLens.Logging.DOMAIN.Utilities;

namespace ExecutionLens.Logging;

public static partial class ServiceCollection
{
    public static IServiceCollection AddLoggedScoped<TService, TImplementation>(this IServiceCollection services)
        where TService : class
        where TImplementation : class, TService
    => services.AddLoggedService<TService, TImplementation>(ServiceLifetime.Scoped);

    public static IServiceCollection AddLoggedTransient<TService, TImplementation>(this IServiceCollection services)
        where TService : class
        where TImplementation : class, TService
    => services.AddLoggedService<TService, TImplementation>(ServiceLifetime.Transient);

    private static IServiceCollection AddLoggedService<TService, TImplementation>(this IServiceCollection services, ServiceLifetime lifetime)
        where TService : class
        where TImplementation : class, TService
    {
        if (!typeof(TService).IsInterface)
            throw new InvalidOperationException($"'{typeof(TService).Name}' has to be a Interface!");

        services.Add(new ServiceDescriptor(typeof(TImplementation), typeof(TImplementation), lifetime));
        services.Add(ServiceDescriptor.Describe(
            typeof(TService),
            provider =>
            {
                var proxyGenerator = provider.GetRequiredService<ProxyGenerator>();
                var interceptorService = provider.GetRequiredService<IInterceptorService>();
                var implementationInstance = provider.GetRequiredService<TImplementation>();
                return proxyGenerator.CreateInterfaceProxyWithTarget<TService>(implementationInstance, interceptorService);
            },
            lifetime
        ));
        return services;
    }
    public static LoggerConfiguration AddLogger(this IServiceCollection services, Action<LoggerConfiguration>? configuration = null)
    {
        services.Configure(configuration ??= defaultConfiguration => { });
        services.AddScoped<ProxyGenerator>();

        services.AddScoped<IInterceptorService, InterceptorService>();
        services.AddScoped<IInformationLogger, InformationLogger>();
        services.AddScoped<ILogService, LogService>();
        services.AddScoped<LogAttribute>();
        services.AddScoped<LogManager>();

        return new LoggerConfiguration();
    }
}