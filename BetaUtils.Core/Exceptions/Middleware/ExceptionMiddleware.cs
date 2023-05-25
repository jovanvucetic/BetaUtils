using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Routing;

namespace BetaUtils.Core.Exceptions.Middleware;

/// <summary>
/// Handling exceptions that occur during the execution of subsequent middleware components in an ASP.NET Core application
/// pipeline. It catches exceptions, generates appropriate error responses, and adds them to the HTTP context.
/// </summary>
public class ExceptionMiddleware
{
    private static readonly ActionDescriptor EmptyActionDescriptor = new();
    private static readonly RouteData EmptyRouteData = new();

    public ExceptionMiddleware(RequestDelegate next, IActionResultExecutor<ObjectResult> executor)
    {
        Next = next;
        Executor = executor;
    }

    private RequestDelegate Next { get; }

    private IActionResultExecutor<ObjectResult> Executor { get; }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await Next(context).ConfigureAwait(true);
        }
        catch (Exception ex) when (!context.Response.HasStarted)
        {
            (string ExceptionName, int StatusCode) exceptionInformation = ExceptionInformation(ex);

            ResponseHeaderHandler.ClearResponse(context, TrackedExceptions.GetExceptionStatusCode(ex));

            RouteData routeData = context.GetRouteData() ?? EmptyRouteData;

            ActionContext actionContext = new(context, routeData, EmptyActionDescriptor);

            ModelStateDictionary modelState = new();

            modelState.AddModelError(
                exceptionInformation.ExceptionName,
                ex.InnerException is not null ? ex.InnerException.Message : ex.Message);

            ValidationProblemDetails problemDetails = new(modelState);

            await Executor.ExecuteAsync(
                    actionContext,
                    new ObjectResult(problemDetails)
                    {
                        StatusCode = exceptionInformation.StatusCode,
                        DeclaredType = problemDetails.GetType(),
                    })
                .ConfigureAwait(true);
        }
    }

    /// <summary>
    /// Check if exception is tracked. In case that exceptions is tracked it will return its type name and status code
    /// associated.
    /// If exception is not tracked 500 status code will be thrown with system message. (unhandled exception)
    /// </summary>
    private static (string ExceptionName, int StatusCode) ExceptionInformation(Exception exception)
    {
        return TrackedExceptions.ExceptionExist(exception)
            ? (exception.GetType().Name, TrackedExceptions.GetExceptionStatusCode(exception))
            : throw exception;
    }
}