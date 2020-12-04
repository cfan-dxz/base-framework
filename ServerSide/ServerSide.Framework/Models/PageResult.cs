using Newtonsoft.Json;
using System.Collections.Generic;

namespace ServerSide.Framework.Models
{
    public class PageResult<T>
    {
        [JsonProperty("total")]
        public int Total { get; set; }

        [JsonProperty("items")]
        public List<T> Items { get; set; }
    }
}
