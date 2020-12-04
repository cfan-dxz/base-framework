using Demo.Idp.Service.Helper;
using Demo.Idp.Service.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Demo.Idp.Service.Impl
{
    public class EdApiService : IEdApiService
    {
        private string ApiHost = "http://sandbox.bestmine.net:8088";
        private Dictionary<string, string> Token = new Dictionary<string, string> { ["Authorization"] = "Basic YWRtaW46MTIzNDU2" };

        private readonly IHttpContextAccessor _httpContextAccessor;

        public EdApiService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// 获取客户端ip
        /// </summary>
        /// <returns></returns>
        private string getClientIp()
        {
            var ip = _httpContextAccessor.HttpContext.Request.Headers["X-Forwarded-For"].FirstOrDefault();
            if (string.IsNullOrEmpty(ip))
            {
                ip = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
            }
            return ip;
        }

        /// <summary>
        /// 获取E登用户
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public EdUserModel GetEdUser(string username, string password, out string msg)
        {
            var apiUrl = $"{ApiHost}/api/ed/user";
            var data = new { UserName = username, Pwd = password, IP = getClientIp() };
            var resStr = HttpHelper.Post(apiUrl, data, Token);
            string errMsg = null;
            if (resStr.Contains("没有访问权限"))
            {
                errMsg = resStr;
            }
            else if (resStr.Contains("ErrorMsg"))
            {
                var errJson = resStr.Substring(1, resStr.Length - 2).Replace(@"\", "");
                var error = JsonConvert.DeserializeObject<EdErrorModel>(errJson);
                errMsg = error?.ErrorMsg ?? "未知错误";
                if (errMsg.Contains("该用户不存在"))
                {
                    errMsg = "用户名或者密码错误，请检查后再登录。";
                }
            }
            else if (resStr.Contains("ExceptionMessage"))
            {
                //var error = JsonConvert.DeserializeObject<dynamic>(resStr);
                errMsg = "E登接口服务器内部错误";
            }
            if (!string.IsNullOrEmpty(errMsg))
            {
                msg = errMsg;
                return null;
            }
            msg = null;
            return JsonConvert.DeserializeObject<EdUserModel>(resStr);
        }
    }
}
