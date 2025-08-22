using System.Threading;
using System.Threading.Tasks;

namespace ATCommon.Service;

/// <summary>
///     This interface is the marker interface for service handler of
///     each api, which is the main business flow.
/// </summary>
/// <typeparam name="TRequest">
///     The type of the service request.
///     It must be derived from <see cref="IServiceRequest{TResponse}"/>.
/// </typeparam>
/// <typeparam name="TResponse">
///     The type of the service response.
///     It must be derived from <see cref="IServiceResponse"/>.
/// </typeparam>
public interface IServiceHandler<TRequest, TResponse>
    where TRequest : IServiceRequest<TResponse>
    where TResponse : IServiceResponse
{
    Task<TResponse> ExecuteAsync(TRequest request, CancellationToken ct);
}
