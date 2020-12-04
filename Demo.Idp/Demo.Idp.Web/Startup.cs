using Demo.Idp.Service;
using Demo.Idp.Service.Impl;
using Demo.Idp.Web.Core;
using Demo.Idp.Web.Validator;
using Demo.OAuth.Web.ProfileService;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Demo.Idp.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            #region IdentityServer4

            services.AddIdentityServer(options =>
                {
                    options.Events.RaiseErrorEvents = true;
                    options.Events.RaiseInformationEvents = true;
                    options.Events.RaiseFailureEvents = true;
                    options.Events.RaiseSuccessEvents = true;
                    // see https://identityserver4.readthedocs.io/en/latest/topics/resources.html
                    options.EmitStaticAudienceClaim = true;
                })
                .AddDeveloperSigningCredential()
                .AddInMemoryIdentityResources(OAuthConfig.GetIdentityResources())
                .AddInMemoryApiScopes(OAuthConfig.GetApiScopes())
                .AddInMemoryApiResources(OAuthConfig.GetApiResources())
                .AddClientStore<ClientStore>()
                .AddResourceOwnerValidator<ResourceOwnerPasswordValidator>()
                .AddProfileService<UserProfileService>();

            #endregion

            services.AddAuthentication();
            services.AddHttpContextAccessor();

            //cookie£ºSameSite=Lax
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.MinimumSameSitePolicy = SameSiteMode.Lax;
            });

            //service
            services.AddScoped<IEdApiService, EdApiService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseCors(builder => builder.WithOrigins(new string[] { "http://localhost:2222", "http://localhost:1234" }).AllowAnyMethod().AllowAnyHeader().AllowCredentials());

            app.UseIdentityServer();

            app.UseCookiePolicy();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
