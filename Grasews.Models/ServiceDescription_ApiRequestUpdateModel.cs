﻿using Newtonsoft.Json;

namespace Grasews.API.Models
{
    /// <summary>
    ///
    /// </summary>
    public class ServiceDescription_ApiRequestUpdateModel : BaseModel<int>
    {
        /// <summary>
        ///
        /// </summary>
        [JsonProperty("graph_json", NullValueHandling = NullValueHandling.Ignore)]
        public string GraphJson { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonProperty("service_name", NullValueHandling = NullValueHandling.Ignore)]
        public string ServiceName { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonProperty("xml", NullValueHandling = NullValueHandling.Ignore)]
        public string Xml { get; set; }
    }
}