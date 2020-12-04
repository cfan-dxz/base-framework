using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using ServerSide.Framework.Auth;
using ServerSide.Framework.Extensions;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace ServerSide.Web
{
    /// <summary>
    /// Startup
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Startup
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Configuration
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// ConfigureServices
        /// </summary>
        /// <param name="services"></param>
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //swagger
            services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "API文档",
                    Description = "by developer",
                    //TermsOfService = "None",
                    //Contact = new Contact { Name = "dengxiongzhou", Email = "cfan.dengxiongzhou@gmail.com", Url = "" }
                });
            });

            services.AddControllers()
                .AddNewtonsoftJson(options => //System.Text.Json->NewtonsoftJson
                {
                    options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                    options.SerializerSettings.DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Local;
                });

            //身份验证
            var authConfig = Configuration.GetSection("Auth").Get<AuthConfig>();
            services.AddAuthorization();
            services.AddAuthentication(authConfig.DefaultScheme)
                .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = authConfig.Authority;
                    options.RequireHttpsMetadata = false; //不需要https    
                    options.ApiName = authConfig.ApiName; //api的name
                });

            //公共服务
            services.AddService();
        }

        /// <summary>
        /// 配置ioc容器
        /// </summary>
        /// <param name="builder"></param>
        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterService();
        }

        /// <summary>
        /// Configure
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}

            //自定义异常处理
            app.UseApiExceptionHandler(env.IsDevelopment());

            //设置跨域
            app.UseAllowedOrigins();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1 Docs");
                c.DocExpansion(DocExpansion.None);
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
