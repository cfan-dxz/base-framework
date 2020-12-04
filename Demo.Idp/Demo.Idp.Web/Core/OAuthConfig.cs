using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Collections.Generic;
using static IdentityServer4.IdentityServerConstants;

namespace Demo.Idp.Web.Core
{
    public class OAuthConfig
    {
        //scope
        public const string API_SCOPE = "erp";
        //api
        public const string API_NAME = "erp_api";
        //token过期时间(s)
        public const int EXPIRE_IN = 3600;

        #region API资源

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };
        }

        /// <summary>
        /// Api 作用域
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<ApiScope> GetApiScopes()
        {
            return new List<ApiScope>
            {
                new ApiScope(API_SCOPE, API_SCOPE),

            };
        }

        /// <summary>
        /// Api 资源
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource(API_NAME, API_NAME){ Scopes = { API_SCOPE } },
            };
        }

        #endregion

        /// <summary>
        /// 客户端
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
               // 后端接口
                new Client()
                {
                    ClientId = "web_api",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,//Resource Owner Password模式,
                    ClientSecrets = {new Secret("AD7666C3-F54A-4865-AC7D-C52EE1CC6B9B".Sha256()) },
                    AllowOfflineAccess = true,//如果要获取refresh_tokens ,必须把AllowOfflineAccess设置为true
                    AllowedScopes= {
                        API_SCOPE,
                        StandardScopes.OfflineAccess,
                    },
                    AccessTokenLifetime = EXPIRE_IN,
                },

                // spa-vue2
                new Client
                {
                    ClientId = "spa-vue2",
                    ClientSecrets = { new Secret("CAFFAF51-6FEB-4595-9B08-A64EEA036337".Sha256()) },
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,
                    RedirectUris = {
                        "http://localhost:2222/oidc/callback",
                        "http://localhost:2222/oidc/silentrenew"
                    },
                    FrontChannelLogoutUri = "http://localhost:3333/Account/Login",
                    PostLogoutRedirectUris = { "http://localhost:2222" },
                    AllowedCorsOrigins = { "http://localhost:2222" },
                    RequireConsent=false,
                    AllowOfflineAccess = true,
                    AllowedScopes = {
                        StandardScopes.OpenId,
                        StandardScopes.Profile,
                        StandardScopes.OfflineAccess,
                        API_SCOPE
                    }
                },
            };
        }

        /// <summary>
        /// 测试的账号和密码
        /// </summary>
        /// <returns></returns>
        public static List<TestUser> GetTestUsers()
        {
            return new List<TestUser>
            {
                new TestUser()
                {
                     SubjectId = "1",
                     Username = "test",
                     Password = "123456"
                },
            };
        }
    }
}
