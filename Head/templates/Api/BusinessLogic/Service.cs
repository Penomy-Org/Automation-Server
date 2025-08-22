using System;
using System.Threading;
using System.Threading.Tasks;
using ApiTemplate.DataAccess;
using ApiTemplate.Models;
using ATCommon.Service;

namespace ApiTemplate.BusinessLogic;

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
