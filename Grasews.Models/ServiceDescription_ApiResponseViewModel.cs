using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Grasews.API.Models
{
    /// <summary>
    ///
    /// </summary>
    public class ServiceDescription_ApiResponseViewModel : BaseModel<int>
    {
        /// <summary>
        ///
        /// </summary>
        [JsonProperty("graph_json", NullValueHandling = NullValueHandling.Ignore)]
        public string GraphJson { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonProperty("id_owner_user", NullValueHandling = NullValueHandling.Ignore)]
        public int IdOwnerUser { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonProperty("registration_datetime", NullValueHandling = NullValueHandling.Ignore, Order = 100)]
        public DateTime RegistrationDateTime { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonProperty("service_name", NullValueHandling = NullValueHandling.Ignore, Order = -9)]
        public string ServiceName { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonProperty("xml", NullValueHandling = NullValueHandling.Ignore)]
        public string Xml { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonProperty("html_treeview_menu", NullValueHandling = NullValueHandling.Ignore)]
        public string HtmlTreeViewMenu { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonProperty("wsdl_interfaces", NullValueHandling = NullValueHandling.Ignore)]
        public ICollection<ParseWsdl_ApiResponseViewModel.WsdlInterfaceResponseViewModel> WsdlInterfaces { get; set; }
    }
}