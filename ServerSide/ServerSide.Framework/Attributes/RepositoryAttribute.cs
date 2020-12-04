using System;
using System.Collections.Generic;
using System.Text;

namespace ServerSide.Framework.Attributes
{
    /// <summary>
    /// 仓储类特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class RepositoryAttribute : Attribute
    {
    }
}
