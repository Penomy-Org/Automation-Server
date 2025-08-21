using System;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace PNCommon.DependencyInjection;

/// <summary>
///     Provides extension methods for registering filters in the service collection.
/// </summary>
public static class FilterRegistrationExtension
{
    private static readonly Type AsyncActionFilterType = typeof(IAsyncActionFilter);

    /// <summary>
    ///     Register filters from the given assembly to the service collection.
    ///     Filters are determined by whether they implement the interface
    ///     <see cref="IAsyncActionFilter"/>. If no filters are found in the
    ///     given assembly, an exception is thrown.
    /// </summary>
    /// <param name="services">
    ///     The service collection to register the filters to.
    /// </param>
    /// <param name="assembly">
    ///     The assembly to search for filters.
    /// </param>
    /// <returns>
    ///     The service collection.
    /// </returns>
    /// <exception cref="ApplicationException">
    ///     If no filters are found in the given assembly.
    /// </exception>
    /// <remarks>
    ///     If no filter are found in the assembly, don't call this function.
    /// </remarks>
    public static IServiceCollection RegisterFiltersFromAssembly(
        this IServiceCollection services,
        Assembly assembly
    )
    {
        // Get all types from the specified assembly
        var allTypes = assembly.GetTypes();

        // Check if any filter is found
        // If no filter is found, throw an exception
        var isFilterFound = allTypes.Any(type =>
            AsyncActionFilterType.IsAssignableFrom(type) && !type.IsInterface
        );
        if (!isFilterFound)
        {
            throw new ApplicationException(
                $"No filters are found in this assembly {assembly.GetName()}, please omit this function !!"
            );
        }

        // Iterate through all types and register filters
        foreach (var type in allTypes)
        {
            // Check again if the type is a filter and not an interface
            if (AsyncActionFilterType.IsAssignableFrom(type) && !type.IsInterface)
            {
                services.AddSingleton(type);
            }
        }

        return services;
    }
}
