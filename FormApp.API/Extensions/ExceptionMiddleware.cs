using Microsoft.AspNetCore.Diagnostics;
using FormApp.Core.Exceptions;
using System.Net;

namespace FormApp.API.Extensions;

public static class ExceptionMiddleware
{
    public static void ConfigureExceptionHandler(this WebApplication app)
    {
        app.UseExceptionHandler(appError =>
        {
            appError.Run(async context =>
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";

                var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                if (contextFeature != null)
                {
                    context.Response.StatusCode = contextFeature.Error switch
                    {
                        NotFoundException => StatusCodes.Status404NotFound,
                        BadRequestException => StatusCodes.Status400BadRequest,
                        UnauthorizedException => StatusCodes.Status401Unauthorized,
                        _ => StatusCodes.Status500InternalServerError
                    };

                    if (contextFeature.Error is NotFoundException or BadRequestException or UnauthorizedException)
                    {
                        switch (contextFeature.Error)
                        {
                            case NotFoundException notFoundException:
                                await context.Response.WriteAsync(new ExceptionModel
                                {
                                    HttpStatusCode = context.Response.StatusCode,
                                    StatusCode = notFoundException.StatusCode,
                                    Message = contextFeature.Error.Message
                                }.ToString());
                                break;
                            case BadRequestException badRequestException:
                                await context.Response.WriteAsync(new ExceptionModel
                                {
                                    HttpStatusCode = context.Response.StatusCode,
                                    StatusCode = badRequestException.StatusCode,
                                    Message = contextFeature.Error.Message
                                }.ToString());
                                break;
                            case UnauthorizedException unauthorizedException:
                                await context.Response.WriteAsync(new ExceptionModel
                                {
                                    HttpStatusCode = context.Response.StatusCode,
                                    StatusCode = unauthorizedException.StatusCode,
                                    Message = contextFeature.Error.Message
                                }.ToString());
                                break;
                        }
                    }
                    else
                    {
                        await context.Response.WriteAsync(new ExceptionModel
                        {
                            HttpStatusCode = context.Response.StatusCode,
                            StatusCode = StatusCodes.Status500InternalServerError,
                            Message = "An unexpected error occurred."
                        }.ToString());
                    }
                }
            });
        });
    }
}
