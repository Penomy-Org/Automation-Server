using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace PNCommon.DependencyInjection;

/// <summary>
///     Interface for registering services of api.
/// </summary>
public interface IServiceRegister
{
    /// <summary>
    ///     If api has any service of its own that needs to
    ///     be registered, this method must be called.
    /// </summary>
    /// <param name="services">
    ///     The service collection.
    /// </param>
    /// <param name="configuration">
    ///     The configuration.
    /// </param>
    /// <returns>
    ///     The modified service collection.
    /// </returns>
    IServiceCollection Register(IServiceCollection services, IConfiguration configuration);
}
