using System;

namespace ServerSide.Framework.Attributes
{
    /// <summary>
    /// 组件特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class ComponentAttribute : Attribute
    {
    }
}
