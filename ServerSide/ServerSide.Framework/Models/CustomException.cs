using System;
using System.Collections.Generic;
using System.Text;

namespace ServerSide.Framework.Models
{
    /// <summary>
    /// 自定义异常
    /// </summary>
    public class CustomException : Exception
    {
        public CustomException(string message) : base(message)
        {
        }

        /// <summary>
        /// 状态
        /// </summary>
        public int? Status { get; set; }
    }
}
