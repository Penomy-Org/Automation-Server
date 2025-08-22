using System;
using Microsoft.Extensions.DependencyInjection;

namespace ATCommon.DependencyInjection;

/// <summary>
///     Provides extension methods to register lazy services,
///     which allows for deferred dependency injection until the
///     service is actually needed.
///
///     Why lazy services?
///     Lazy services allow us to defer the dependency injection of a service until the
///     service is actually needed. This can be useful if the service is expensive to
///     create or if the service is not always needed.
/// </summary>
public static class MakeLazyServiceExtension
{
    /// <summary>
    ///     Registers a lazy wrapper for a singleton service.
    /// </summary>
    /// <typeparam name="T">
    ///     The type of the service.
    /// </typeparam>
    /// <param name="services">
    ///     The service collection.
    /// </param>
    /// <returns>
    ///     The modified service collection.
    /// /// </returns>
    public static IServiceCollection MakeSingletonLazy<T>(this IServiceCollection services)
        where T : class
    {
        return services.AddSingleton<Lazy<T>>(provider => new(provider.GetRequiredService<T>()));
    }

    /// <summary>
    ///     Registers a lazy wrapper for a scoped service.
    /// </summary>
    /// <typeparam name="T">
    ///     The type of the service.
    /// </typeparam>
    /// <param name="services">
    ///     The service collection.
    /// </param>
    /// <returns>
    ///     The updated service collection.
    /// </returns>
    public static IServiceCollection MakeScopedLazy<T>(this IServiceCollection services)
        where T : class
    {
        return services.AddScoped<Lazy<T>>(provider => new(provider.GetRequiredService<T>()));
    }
}
