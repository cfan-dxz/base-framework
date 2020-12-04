using FreeSql.DataAnnotations;

namespace ServerSide.Models.Entity
{
    [Table(Name = "dbo.ComShop")]
    public class ComShop
    {
        /// <summary>
        /// 
        /// </summary>
        [Column(IsPrimary = true)]
        public System.Int64 ShopId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String ShopName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String ShopNameCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Int64 PlatformId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String PlatformNameEn { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String PlatformNameCn { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String StoreName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String StoreEmail { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String StoreSite { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String StoreUrl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String MerchantId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String AccessToken { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.DateTime? AccessTokenExpire { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String RefreshToken { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.DateTime? RefreshTokenExpire { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String AppKey { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String AppSecret { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String ApiLogin { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String ApiPwd { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Boolean IsEnabled { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Boolean? IsAuth { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.DateTime? OrderDownloadTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.DateTime? ListingDownloadTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.DateTime CreateDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Int64 CreateUserId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String CreateUserName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.DateTime? ModifyDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Int64? ModifyUserId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String ModifyUserName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.DateTime? DeleteDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Int64? DeleteUserId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String DeleteUserName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Boolean IsDeleted { get; set; }
    }
}
