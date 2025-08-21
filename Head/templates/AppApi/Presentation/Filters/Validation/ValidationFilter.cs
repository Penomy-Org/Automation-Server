using System.Net.Mime;
using System.Text.Json;
using System.Threading.Tasks;
using ApiTemplate.Common;
using ApiTemplate.Presentation.Filters.SetStateBag;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ApiTemplate.Presentation.Filters.Validation;

public sealed class ValidationFilter : IAsyncActionFilter
{
    private readonly IValidator<Request> _validator;

    public ValidationFilter(IValidator<Request> validator)
    {
        _validator = validator;
    }

    public async Task OnActionExecutionAsync(
        ActionExecutingContext context,
        ActionExecutionDelegate next
    )
    {
        var stateBag = context.HttpContext.Items[nameof(StateBag)] as StateBag;
        var request = stateBag.HttpRequest;

        var result = await _validator.ValidateAsync(request);
        if (!result.IsValid)
        {
            context.Result = new ContentResult
            {
                StatusCode = Constant.DefaultResponse.Http.VALIDATION_FAILED.HttpCode,
                Content = JsonSerializer.Serialize(Constant.DefaultResponse.Http.VALIDATION_FAILED),
                ContentType = MediaTypeNames.Application.Json,
            };

            return;
        }

        await next();
    }
}
