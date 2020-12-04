using Newtonsoft.Json;
using System.Collections.Generic;

namespace ServerSide.Framework.Models
{
    public class ApiResponse<T>
    {
        [JsonProperty("code")]
        public int Code { get; set; }

        [JsonProperty("msg")]
        public string Msg { get; set; }

        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("data")]
        public T Data { get; set; }

        [JsonProperty("detail", NullValueHandling = NullValueHandling.Ignore)]
        public object Detail { get; set; }
    }

    public class ApiResponse
    {
        public static ApiResponse<TData> Init<TData>(TData data, int code = 0, string msg = null, bool success = true, object detail = null)
        {
            return new ApiResponse<TData>() { Data = data, Code = code, Msg = msg, Success = success, Detail = detail };
        }

        /// <summary>
        /// 成功响应
        /// </summary>
        /// <typeparam name="TData"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public static ApiResponse<TData> Success<TData>(TData data)
        {
            return Init(data);
        }

        /// <summary>
        /// 错误响应
        /// </summary>
        /// <typeparam name="TData"></typeparam>
        /// <param name="msg"></param>
        /// <param name="data"></param>
        /// <param name="code"></param>
        /// <param name="detail"></param>
        /// <returns></returns>
        public static ApiResponse<TData> Error<TData>(string msg, TData data = default, int code = 110, object detail = null)
        {
            return Init(data, code, msg, false, detail);
        }

        /// <summary>
        /// 分页响应
        /// </summary>
        /// <typeparam name="TData"></typeparam>
        /// <param name="data"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public static ApiResponsePage<TData> Page<TData>(List<TData> data, int total)
        {
            return new ApiResponsePage<TData> { Data = new PageResult<TData> { Items = data, Total = total }, Success = true };
        }
    }
}
