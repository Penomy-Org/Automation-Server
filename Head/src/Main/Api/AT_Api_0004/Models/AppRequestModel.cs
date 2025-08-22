using ATCommon.Service;

namespace AT_Api_0004.Models;

public sealed class AppRequestModel : IServiceRequest<AppResponseModel>
{
    public string Email { get; set; }

    public string Password { get; set; }
}
