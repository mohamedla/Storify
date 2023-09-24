using Microsoft.AspNetCore.Diagnostics;
using Contracts;
using System.Net;
using Entities.ErrorModel;

namespace StorifyAPI.Extensions
{
    public static class ExceptionMiddelware
    {
        public static void ConfigureExeptionHundelar(this WebApplication app, ILoggerManager loggerManager)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run( async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();

                    if(contextFeature != null)
                    {
                        loggerManager.LogError($"Something Went Wrong: {contextFeature.Error}");

                        await context.Response.WriteAsync(new ErrorDetails()
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = "Internal Server Error"
                        }.ToString() );
                    }

                });
            });
        }
    }
}
