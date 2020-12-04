using Autofac;
using FreeRedis;
using FreeSql;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyModel;
using ServerSide.Framework.Attributes;
using ServerSide.Framework.Redis;
using ServerSide.Framework.Repository;
using ServerSide.Framework.Utils;
using System;
using System.Linq;
using System.Reflection;
using System.Text;
using WebApiClient;
using WebApiClient.Extensions.Autofac;

namespace ServerSide.Framework.Extensions
{
    public static class IOC
    {
        /// <summary>
        /// 公共服务
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static void AddService(this IServiceCollection services)
        {
            //HttpContextAccessor
            services.AddHttpContextAccessor();

            //注册编码
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            //nlog配置文件
            NLog.LogManager.LoadConfiguration("nlog.config");

            //IdleBus
            services.AddSingleton(sp =>
            {
                var ib = new IdleBus<IFreeSql>(TimeSpan.FromMinutes(10));
                var section = sp.GetService<IConfiguration>().GetSection("DataSource");
                foreach (var item in section.GetChildren())
                {
                    //数据库类型
                    var dbType = section[$"{item.Key}:Type"] != null ? Enum.Parse<DataType>(section[$"{item.Key}:Type"], true) : DataType.SqlServer;
                    //连接字符串
                    var connStr = item.Value ?? section[$"{item.Key}:Value"];
                    ib.Register(item.Key, () => new FreeSqlBuilder().UseConnectionString(dbType, connStr).Build());
                }
                return ib;
            });

            //freeredis
            services.AddSingleton(sp =>
            {
                var redisConfig = sp.GetService<IConfiguration>().GetSection("Redis").Get<RedisConfig>();
                var connectionString = string.Format("{0},defaultDatabase={2},poolsize=50,preheat=true,ssl=false,writeBuffer=10240,tryit=0,name={1},prefix={1}",
                    redisConfig.ConnectionString, redisConfig.InstanceName, redisConfig.Db ?? 0);
                var cli = new RedisClient(connectionString);
                return cli;
            });

            //BaseRepository
            services.AddScoped(typeof(ISimpleRepository<>), typeof(SimpleRepository<>));
        }

        #region 本地服务

        /// <summary>
        /// 注册服务
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="assemblyName"></param>
        public static void RegisterService(this ContainerBuilder builder, string assemblyName)
        {
            registerAssembly(builder, assemblyName);
            buildCallback(builder);
        }

        /// <summary>
        /// 注册服务
        /// </summary>
        /// <param name="builder"></param>
        public static void RegisterService(this ContainerBuilder builder)
        {
            //排除所有的系统程序集、Nuget下载包
            var libs = DependencyContext.Default.CompileLibraries.Where(lib => !lib.Serviceable && lib.Type == "project");
            foreach (var lib in libs)
            {
                registerAssembly(builder, lib.Name);
            }
            buildCallback(builder);
        }

        /// <summary>
        /// 注册程序集
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="assemblyName"></param>
        private static void registerAssembly(ContainerBuilder builder, string assemblyName)
        {
            if (!string.IsNullOrWhiteSpace(assemblyName))
            {
                var assembly = Assembly.Load(assemblyName);
                if (assembly != null)
                {
                    //component
                    builder.RegisterAssemblyTypes(assembly)
                        .Where(t => t.IsClass && t.CustomAttributes.Any(a => a.AttributeType == typeof(ComponentAttribute)))
                        .InstancePerLifetimeScope();

                    //service
                    builder.RegisterAssemblyTypes(assembly)
                        .Where(t => t.IsClass
                            && (
                            t.Name.EndsWith("Service")
                            || t.Name.EndsWith("Repository")
                            || t.CustomAttributes.Any(a => a.AttributeType == typeof(ServiceAttribute)
                            || a.AttributeType == typeof(RepositoryAttribute))
                            )
                        ) //服务带Service后缀或者有[Service]特性
                        .AsImplementedInterfaces()
                        .InstancePerLifetimeScope();

                    //httpapi
                    var httpApiList = assembly.GetTypes().Where(x => x.IsInterface && x.GetInterfaces() != null && x.GetInterfaces().Any(i => i == typeof(IHttpApi))).ToList();
                    httpApiList?.ForEach(t => InitHttpService(t, builder));
                }
            }
        }

        /// <summary>
        /// build回调
        /// </summary>
        /// <param name="builder"></param>
        private static void buildCallback(ContainerBuilder builder)
        {
            builder.RegisterBuildCallback(container =>
            {
                //初始化Autofac容器
                AutofacHelper.Init(container);
            });
        }

        #endregion

        #region http服务

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="type"></param>
        /// <param name="builder"></param>
        public static void InitHttpService(Type type, ContainerBuilder builder)
        {
            var thisType = typeof(IOC);
            var genericMethod = thisType.GetMethod("RegisterHttpService");
            MethodInfo method = genericMethod.MakeGenericMethod(type);
            method.Invoke(null, new object[] { builder });
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="builder"></param>
        public static void RegisterHttpService<T>(ContainerBuilder builder) where T : class, IHttpApi
        {
            builder.RegisterHttpApi<T>().ConfigureHttpApiConfig(c =>
            {
                var configAttr = typeof(T).GetCustomAttributes(typeof(HttpHostConfigAttribute), false);
                if (configAttr != null && configAttr.Length > 0)
                {
                    var hostConfig = configAttr.First() as HttpHostConfigAttribute;
                    var config = AutofacHelper.Resolve<IConfiguration>();
                    c.HttpHost = new Uri(config[hostConfig.Key]);
                }
                c.FormatOptions.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
            });
        }

        #endregion
    }
}
