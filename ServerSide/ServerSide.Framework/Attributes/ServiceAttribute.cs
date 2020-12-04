using System;

namespace ServerSide.Framework.Attributes
{
    /// <summary>
    /// 服务类特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class ServiceAttribute : Attribute
    {
    }
}
