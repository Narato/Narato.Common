﻿using Newtonsoft.Json;

namespace Narato.Common.Models
{
    public class PagedCollectionResponse<T>
    {
        public T Data { get; set; }

        [JsonProperty("skip", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int Skip { get; set; }

        [JsonProperty("take", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int Take { get; set; }

        public int Total { get; set; }
    }
}
