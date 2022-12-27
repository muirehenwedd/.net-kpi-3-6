using System.Net;
using Application.Common.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApi.Filters;

public sealed class ExceptionFilter : IAsyncExceptionFilter
{
    private readonly IDictionary<Type, (int ErrorCode, HttpStatusCode StatusCode)> _knownExceptions;

    public ExceptionFilter()
    {
        _knownExceptions = new Dictionary<Type, (int ErrorCode, HttpStatusCode StatusCode)>
        {
            {typeof(ValidationException), (101, HttpStatusCode.BadRequest)},
            {typeof(NotFoundException), (102, HttpStatusCode.BadRequest)},
            {typeof(ConflictException), (103, HttpStatusCode.Conflict)}
        };
    }

    public async Task OnExceptionAsync(ExceptionContext context)
    {
        var exception = context.Exception;

        if (_knownExceptions.TryGetValue(exception.GetType(), out var exceptionInfo))
        {
            context.Result = new JsonResult(new ErrorBody(exceptionInfo.ErrorCode, exception.Message))
            {
                StatusCode = (int) exceptionInfo.StatusCode
            };

            context.ExceptionHandled = true;
        }
    }

    private record ErrorBody(int Code, string Message);
}