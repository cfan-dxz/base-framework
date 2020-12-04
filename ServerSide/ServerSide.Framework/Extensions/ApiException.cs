using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using ServerSide.Framework.Models;
using ServerSide.Framework.Utils;
using System.Threading.Tasks;

namespace ServerSide.Framework.Extensions
{
    public static class ApiException
    {
        public static IApplicationBuilder UseApiExceptionHandler(this IApplicationBuilder app, bool isDev)
        {
            return app.UseExceptionHandler(option => option.Run(async context => await ErrorEvent(context, isDev)));
        }

        private static Task ErrorEvent(HttpContext context, bool isDev)
        {
            var feature = context.Features.Get<IExceptionHandlerFeature>();
            var error = feature?.Error;

            //记录到日志
            LogHelper.Error(context.Request.Path, error);

            //开发环境默认暴露异常信息
            bool isExpose = isDev;
            //自定义异常
            if (error.GetType() == typeof(CustomException))
            {
                var customException = (error as CustomException);
                if (customException.Status.HasValue)
                {
                    context.Response.StatusCode = customException.Status.Value;
                }
                isExpose = true;
            }
            context.Response.ContentType = "application/json;charset=utf-8";
            context.Response.Headers.Append("Access-Control-Allow-Origin", context.Request.Headers["Origin"]);
            context.Response.Headers.Append("Access-Control-Allow-Credentials", "true");

            //接口返回
            var msg = (isExpose ? error.Message : "服务器错误,请稍后再试...");
            var data = ApiResponse.Error<object>(msg);
            var result = Newtonsoft.Json.JsonConvert.SerializeObject(data);
            return context.Response.WriteAsync(result);
        }
    }
}
