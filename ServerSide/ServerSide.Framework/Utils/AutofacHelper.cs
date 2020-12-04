using Autofac;
using System;

namespace ServerSide.Framework.Utils
{
    public class AutofacHelper
    {
        private static ILifetimeScope _lifetimeScope;

        /// <summary>
        /// 初始化Autofac容器
        /// </summary>
        /// <param name="lifetimeScope"></param>
        public static void Init(ILifetimeScope lifetimeScope)
        {
            _lifetimeScope = lifetimeScope;
        }

        /// <summary>
        /// 从容器中获取对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public static T Resolve<T>()
        {
            using (var scope = _lifetimeScope.BeginLifetimeScope())
            {
                return scope.Resolve<T>();
            }
        }

        /// <summary>
        /// 从容器中获取对象
        /// </summary>
        /// <typeparam name="serviceType"></typeparam>
        public static object Resolve(Type serviceType)
        {
            using (var scope = _lifetimeScope.BeginLifetimeScope())
            {
                return scope.Resolve(serviceType);
            }
        }
    }
}
