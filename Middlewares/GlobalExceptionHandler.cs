using System;
using CodeChallenge.DTOs;
using CodeChallenge.Exceptions;
using Microsoft.AspNetCore.Diagnostics;

namespace CodeChallenge.Middlewares
{
    public static class GlobalExceptionHandler
    {
        public static void UseGlobalExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    var ex = context.Features.Get<IExceptionHandlerFeature>().Error;
                    if (ex is AppException appEx)
                    {
                        context.Response.StatusCode = appEx.ExceptionResult.StatusCode;
                        context.Response.ContentType = "application/json";
                        await context.Response.WriteAsync(appEx.ExceptionResult.ToString());
                    }
                    else
                    {
                        context.Response.StatusCode = 500;
                        context.Response.ContentType = "application/json";
                        await context.Response.WriteAsync(new ResponseDto(
                            WeatherData:null,
                            StatusCode:500,
                            Messages: new List<string> { "Something went wrong. please try again later" })
                        .ToString());
                    }

                });
            });
        }
    }

}