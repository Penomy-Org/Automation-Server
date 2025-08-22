using AT_Api_0003.Common;
using ATCommon.Service;

namespace AT_Api_0003.Models;

public sealed class AppResponseModel : IServiceResponse
{
    public Constant.AppCode AppCode { get; set; }

    public BodyModel Body { get; set; }

    public sealed class BodyModel { }
}
