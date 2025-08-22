namespace ATCommon.Service;

/// <summary>
///     The marker interface for service request of each api.
/// </summary>
/// <typeparam name="TResponse">
///     The type of the service response.
/// </typeparam>
public interface IServiceRequest<TResponse>
    where TResponse : IServiceResponse { }
