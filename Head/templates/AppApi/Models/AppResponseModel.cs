using ApiTemplate.Common;
using PNCommon.FeatureService;

namespace ApiTemplate.Models;

public sealed class AppResponseModel : IServiceResponse
{
    public Constant.AppCode AppCode { get; set; }

    public BodyModel Body { get; set; }

    public sealed class BodyModel { }
}
