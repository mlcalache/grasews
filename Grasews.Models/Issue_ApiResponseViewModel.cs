using Grasews.Domain.Enums;
using Grasews.Infra.CrossCutting.Helpers.Extensions;
using Newtonsoft.Json;
using System;

namespace Grasews.API.Models
{
    /// <summary>
    ///
    /// </summary>
    public class Issue_ApiResponseViewModel : BaseModel<int>
    {
        /// <summary>
        ///
        /// </summary>
        [JsonProperty("id_owner_user", NullValueHandling = NullValueHandling.Ignore, Order = 0)]
        public int IdOwnerUser { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonProperty("id_service_description", NullValueHandling = NullValueHandling.Ignore, Order = 1)]
        public int IdServiceDescription { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonProperty("title", NullValueHandling = NullValueHandling.Ignore)]
        public string Title { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonProperty("description", NullValueHandling = NullValueHandling.Ignore, Order = 2)]
        public string Description { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonProperty("registration_datetime", NullValueHandling = NullValueHandling.Ignore, Order = 4)]
        public DateTime RegistrationDateTime { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonProperty("solved", NullValueHandling = NullValueHandling.Ignore, Order = 3)]
        public bool Solved { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonProperty("id_wsdl_interface", NullValueHandling = NullValueHandling.Ignore, Order = 5)]
        public int? IdWsdlInterface { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonProperty("id_wsdl_operation", NullValueHandling = NullValueHandling.Ignore, Order = 5)]
        public int? IdWsdlOperation { get; set; }

        ///// <summary>
        /////
        ///// </summary>
        //[JsonProperty("id_wsdl_input", NullValueHandling = NullValueHandling.Ignore, Order = 5)]
        //public int? IdWsdlInput { get; set; }

        ///// <summary>
        /////
        ///// </summary>
        //[JsonProperty("id_wsdl_output", NullValueHandling = NullValueHandling.Ignore, Order = 5)]
        //public int? IdWsdlOutput { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonProperty("id_wsdl_fault", NullValueHandling = NullValueHandling.Ignore, Order = 5)]
        public int? IdWsdlFault { get; set; }

        ///// <summary>
        /////
        ///// </summary>
        //[JsonProperty("id_wsdl_outfault", NullValueHandling = NullValueHandling.Ignore, Order = 5)]
        //public int? IdWsdlOutfault { get; set; }

        ///// <summary>
        /////
        ///// </summary>
        //[JsonProperty("id_wsdl_infault", NullValueHandling = NullValueHandling.Ignore, Order = 5)]
        //public int? IdWsdlInfault { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonProperty("id_xsd_complex_type", NullValueHandling = NullValueHandling.Ignore, Order = 5)]
        public int? IdXsdComplexType { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonProperty("id_xsd_simple_type", NullValueHandling = NullValueHandling.Ignore, Order = 5)]
        public int? IdXsdSimpleType { get; set; }

        /// <summary>
        ///
        /// </summary>
        public ServiceDescription_ApiResponseViewModel ServiceDescription { get; set; }

        public string ServiceDescriptionElementLabel { get; set; }

        public string ServiceDescriptionElementTypeLabel
        {
            get
            {
                return ServiceDescriptionElementType.HasValue ? ServiceDescriptionElementType.Value.GetEnumDescription() : string.Empty;
            }
        }

        public ServiceDescriptionElementTypeEnum? ServiceDescriptionElementType
        {
            get
            {
                if (IdWsdlInterface.HasValue)
                {
                    return ServiceDescriptionElementTypeEnum.WsdlInterface;
                }
                else if (IdWsdlOperation.HasValue)
                {
                    return ServiceDescriptionElementTypeEnum.WsdlOperation;
                }
                //else if (IdWsdlInput.HasValue)
                //{
                //    return ServiceDescriptionElementTypeEnum.WsdlInput;
                //}
                //else if (IdWsdlOutput.HasValue)
                //{
                //    return ServiceDescriptionElementTypeEnum.WsdlOutput;
                //}
                else if (IdWsdlFault.HasValue)
                {
                    return ServiceDescriptionElementTypeEnum.WsdlFault;
                }
                //else if (IdWsdlInfault.HasValue)
                //{
                //    return ServiceDescriptionElementTypeEnum.WsdlInfault;
                //}
                //else if (IdWsdlOutfault.HasValue)
                //{
                //    return ServiceDescriptionElementTypeEnum.WsdlOutfault;
                //}
                else if (IdXsdComplexType.HasValue)
                {
                    return ServiceDescriptionElementTypeEnum.XsdComplexType;
                }
                else if (IdXsdSimpleType.HasValue)
                {
                    return ServiceDescriptionElementTypeEnum.XsdSimpleType;
                }
                return null;
            }
        }
    }
}