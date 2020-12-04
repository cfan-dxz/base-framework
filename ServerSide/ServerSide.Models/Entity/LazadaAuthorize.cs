using FreeSql.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServerSide.Models.Entity
{
    /// <summary>
    /// 
    /// </summary>
    [Table(Name = "dbo.LazadaAuthorize")]
    public class LazadaAuthorize
    {
        /// <summary>
        /// 
        /// </summary>
        [Column(IsIdentity = true)]
        public System.Int32 LazadaAuthorizeID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Int32? AccountID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Int32? SiteID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String UserID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String ApiKey { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String SiteURL { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String AllowRunComputer { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Boolean? IsEnabled { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.DateTime? CreateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String CreateUser { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.DateTime? LastUpdTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String LastUpdUser { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String AccessToken { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String RefreshToken { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.DateTime? AccessExpirationTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.DateTime? RefreshExpirationTime { get; set; }
    }
}
