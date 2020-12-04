using Newtonsoft.Json;
using ServerSide.Framework.Utils;

namespace ServerSide.Framework.Models
{
    public class ApiRequestPage
    {
        private string _order;
        private string _sort;

        /// <summary>
        /// 页码
        /// </summary>
        [JsonProperty("page")]
        public int Page { get; set; }

        /// <summary>
        /// 条数
        /// </summary>
        [JsonProperty("limit")]
        public int Limit { get; set; }

        /// <summary>
        /// 排序字段
        /// </summary>
        [JsonProperty("sort")]
        public string Sort { get => _sort ?? AntiSqlInject.Instance.GetSafetySql(_sort); set => _sort = value; }

        /// <summary>
        /// 排序方式
        /// </summary>
        [JsonProperty("order")]
        public string Order { get => _order ?? AntiSqlInject.Instance.GetSafetySql(_order); set => _order = value; }
    }
}
