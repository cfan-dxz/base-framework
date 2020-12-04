using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServerSide.Framework.Models
{
    public class ApiResponsePage<T>
    {
        [JsonProperty("code")]
        public int Code { get; set; }

        [JsonProperty("msg")]
        public string Msg { get; set; }

        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("data")]
        public PageResult<T> Data { get; set; }
    }
}
