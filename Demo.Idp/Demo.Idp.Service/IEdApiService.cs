using Demo.Idp.Service.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Idp.Service
{
    public interface IEdApiService
    {
        /// <summary>
        /// 获取E登用户
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        EdUserModel GetEdUser(string username, string password, out string msg);
    }
}
