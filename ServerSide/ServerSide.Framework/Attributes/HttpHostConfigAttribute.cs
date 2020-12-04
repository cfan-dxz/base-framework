using System;

namespace ServerSide.Framework.Attributes
{
    /// <summary>
    /// WebHost配置
    /// </summary>
    public class HttpHostConfigAttribute : Attribute
    {
        /// <summary>
        /// 配置key
        /// </summary>
        public string Key { get; private set; }

        public HttpHostConfigAttribute(string key)
        {
            Key = key;
        }
    }
}
