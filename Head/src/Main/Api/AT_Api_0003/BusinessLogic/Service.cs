using System;
using System.Threading;
using System.Threading.Tasks;
using AT_Api_0003.DataAccess;
using AT_Api_0003.Models;
using ATCommon.Service;

namespace AT_Api_0003.BusinessLogic;

public sealed class Service : IServiceHandler<AppRequestModel, AppResponseModel>
{
    private readonly Lazy<IRepository> _repository;

    public Service(Lazy<IRepository> repository)
    {
        _repository = repository;
    }

    public Task<AppResponseModel> ExecuteAsync(AppRequestModel request, CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}
