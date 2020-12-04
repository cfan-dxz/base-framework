using Demo.Idp.Service;
using Demo.Idp.Web.Core;
using Demo.Idp.Web.Enums;
using IdentityServer4.Test;
using IdentityServer4.Validation;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Demo.Idp.Web.Validator
{
    public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private readonly IEdApiService _edApiService;

        public ResourceOwnerPasswordValidator(IEdApiService edApiService)
        {
            _edApiService = edApiService;
        }

        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            try
            {
                var userName = context.UserName;
                var password = context.Password;

                //验证用户,这么可以到数据库里面验证用户名和密码是否正确
                var claimList = await ValidateUserAsync(userName, password);

                // 验证账号
                context.Result = new GrantValidationResult
                (
                    subject: userName,
                    authenticationMethod: "custom",
                    claims: claimList.ToArray()
                );
            }
            catch (Exception ex)
            {
                //验证异常结果
                context.Result = new GrantValidationResult()
                {
                    IsError = true,
                    Error = ex.Message
                };
            }
        }

        #region Private Method

        /// <summary>
        /// 验证用户
        /// </summary>
        /// <param name="loginName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        private async Task<List<Claim>> ValidateUserAsync(string loginName, string password)
        {
            return await Task.Run(() =>
            {
                List<Claim> claims = null;
                bool isValid = false;
                string errMsg = null;
                // 以及角色相关信息，我这里还是使用内存中已经存在的用户和密码
                var testUser = OAuthConfig.GetTestUsers().Find(t => t.Username == loginName && t.Password == password);
                if (testUser != null)
                {
                    claims = new List<Claim>()
                        {
                            new Claim(UserClaimEnum.UserId.ToString(), $"{testUser.SubjectId}"),
                            new Claim(UserClaimEnum.UserName.ToString(),testUser.Username)
                        };
                    isValid = true;
                }
                else
                {
                    //E登账号
                    var edUser = _edApiService.GetEdUser(loginName, password, out string msg);
                    if (edUser != null)
                    {
                        claims = new List<Claim>()
                        {
                            new Claim(UserClaimEnum.UserId.ToString(), $"{edUser.ID}"),
                            new Claim(UserClaimEnum.UserName.ToString(),edUser.EmployeeName)
                        };
                        isValid = true;
                    }
                    errMsg = msg;
                }

                if (!isValid)
                    throw new Exception(errMsg ?? "登录失败，用户名和密码不正确");

                //实际生产环境需要通过读取数据库的信息并且来声明
                return claims;
            });
        }

        #endregion
    }
}
