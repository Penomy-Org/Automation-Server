using System.Linq;
using System.Net.Mime;
using System.Text.Json;
using System.Threading.Tasks;
using AT_Api_0004.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AT_Api_0004.Presentation.Filters.SetStateBag;

public sealed class SetStateBagFilter : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(
        ActionExecutingContext context,
        ActionExecutionDelegate next
    )
    {
        var doesRequestExist = context.ActionArguments.Any(argument =>
            argument.Key.Equals(Constant.REQUEST_ARGUMENT_NAME)
        );

        if (!doesRequestExist)
        {
            context.Result = new ContentResult
            {
                StatusCode = Constant.DefaultResponse.Http.VALIDATION_FAILED.HttpCode,
                Content = JsonSerializer.Serialize(Constant.DefaultResponse.Http.VALIDATION_FAILED),
                ContentType = MediaTypeNames.Application.Json,
            };

            return;
        }

        var stateBag = new StateBag
        {
            HttpRequest = context.ActionArguments[Constant.REQUEST_ARGUMENT_NAME] as Request,
        };

        context.HttpContext.Items.Add(nameof(StateBag), stateBag);

        await next();
    }
}
