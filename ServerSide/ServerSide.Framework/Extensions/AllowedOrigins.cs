using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using ServerSide.Framework.Utils;

namespace ServerSide.Framework.Extensions
{
    public static class AllowedOrigins
    {
        public static IApplicationBuilder UseAllowedOrigins(this IApplicationBuilder app)
        {
            //跨域设置
            var config = AutofacHelper.Resolve<IConfiguration>();
            var allowedOrigins = config.GetValue<string>("AllowedOrigins") ?? string.Empty;
            return app.UseCors(builder => builder.WithOrigins(allowedOrigins.Split(',')).AllowAnyMethod().AllowAnyHeader().AllowCredentials());
        }
    }
}
