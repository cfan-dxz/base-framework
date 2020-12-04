using NLog;
using System;
using System.Diagnostics;

namespace ServerSide.Framework.Utils
{
    /// <summary>
    /// 日志
    /// </summary>
    public class LogHelper
    {
        //getlogger
        private static Logger getLogger()
        {
            try
            {
                //调用栈信息
                var method = new StackTrace().GetFrame(2).GetMethod();
                return LogManager.GetLogger($"{method.DeclaringType.FullName}.{method.Name}");
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 调试日志
        /// </summary>
        /// <param name="msg"></param>
        public static void Debug(string msg)
        {
            var logger = getLogger();
            if (logger != null && logger.IsDebugEnabled)
            {
                logger.Debug(msg);
            }
        }

        /// <summary>
        /// 调试日志
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="exception"></param>
        public static void Debug(string msg, Exception exception)
        {
            var logger = getLogger();
            if (logger != null && logger.IsDebugEnabled)
            {
                logger.Debug(exception, msg);
            }
        }

        /// <summary>
        /// 普通日志
        /// </summary>
        /// <param name="msg"></param>
        public static void Info(string msg)
        {
            var logger = getLogger();
            if (logger != null && logger.IsInfoEnabled)
            {
                logger.Info(msg);
            }
        }

        /// <summary>
        /// 普通日志
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="exception"></param>
        public static void Info(string msg, Exception exception)
        {
            var logger = getLogger();
            if (logger != null && logger.IsInfoEnabled)
            {
                logger.Info(exception, msg);
            }
        }

        /// <summary>
        /// 警告日志
        /// </summary>
        /// <param name="msg"></param>
        public static void Warn(string msg)
        {
            var logger = getLogger();
            if (logger != null && logger.IsWarnEnabled)
            {
                logger.Warn(msg);
            }
        }

        /// <summary>
        /// 警告日志
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="exception"></param>
        public static void Warn(string msg, Exception exception)
        {
            var logger = getLogger();
            if (logger != null && logger.IsWarnEnabled)
            {
                logger.Warn(exception, msg);
            }
        }

        /// <summary>
        /// 错误日志
        /// </summary>
        /// <param name="msg"></param>
        public static void Error(string msg)
        {
            var logger = getLogger();
            if (logger != null && logger.IsErrorEnabled)
            {
                logger.Error(msg);
            }
        }

        /// <summary>
        /// 错误日志
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="exception"></param>
        public static void Error(string msg, Exception exception)
        {
            var logger = getLogger();
            if (logger != null && logger.IsErrorEnabled)
            {
                logger.Error(exception, msg);
            }
        }

        /// <summary>
        /// 严重错误
        /// </summary>
        /// <param name="msg"></param>
        public static void Fatal(string msg)
        {
            var logger = getLogger();
            if (logger != null && logger.IsFatalEnabled)
            {
                logger.Fatal(msg);
            }
        }

        /// <summary>
        /// 严重错误
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="exception"></param>
        public static void Fatal(string msg, Exception exception)
        {
            var logger = getLogger();
            if (logger != null && logger.IsFatalEnabled)
            {
                logger.Fatal(exception, msg);
            }
        }
    }
}
