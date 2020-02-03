using Grasews.Domain.Enums;
using Grasews.Infra.CrossCutting.Helpers.Extensions;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Grasews.API.Models
{
    /// <summary>
    ///
    /// </summary>
    public class ParseWsdl_ApiResponseViewModel
    {
        /// <summary>
        /// /
        /// </summary>
        [JsonProperty("wsdl_interfaces", NullValueHandling = NullValueHandling.Ignore)]
        public ICollection<WsdlInterfaceResponseViewModel> WsdlInterfaces { get; set; }

        /// <summary>
        ///
        /// </summary>
        public class SawsdlModelReferenceViewModel
        {
            /// <summary>
            ///
            /// </summary>
            [JsonProperty("id_ontology_term", NullValueHandling = NullValueHandling.Ignore)]
            public int IdOntologyTerm { get; set; }

            /// <summary>
            ///
            /// </summary>
            [JsonProperty("id_owner_user", NullValueHandling = NullValueHandling.Ignore)]
            public int IdOwnerUser { get; set; }

            /// <summary>
            ///
            /// </summary>
            [JsonProperty("id_service_description", NullValueHandling = NullValueHandling.Ignore)]
            public int IdServiceDescription { get; set; }

            /// <summary>
            ///
            /// </summary>
            [JsonProperty("model_reference", NullValueHandling = NullValueHandling.Ignore)]
            public string ModelReference { get; set; }

            public override string ToString()
            {
                return ModelReference;
            }
        }

        /// <summary>
        ///
        /// </summary>
        public class WsdlInterfaceResponseViewModel : BaseModel<int>
        {
            /// <summary>
            ///
            /// </summary>
            [JsonProperty("element_type", NullValueHandling = NullValueHandling.Ignore)]
            public string ElementType
            {
                get
                {
                    return ServiceDescriptionElementTypeEnum.WsdlInterface.GetEnumDescription();
                }
            }

            /// <summary>
            ///
            /// </summary>
            [JsonProperty("id_element_type", NullValueHandling = NullValueHandling.Ignore)]
            public int IdElementType
            {
                get
                {
                    return (int)ServiceDescriptionElementTypeEnum.WsdlInterface;
                }
            }

            /// <summary>
            ///
            /// </summary>
            [JsonProperty("sawsdl_model_references", NullValueHandling = NullValueHandling.Ignore)]
            public ICollection<SawsdlModelReferenceViewModel> SawsdlModelReferences { get; set; }

            /// <summary>
            ///
            /// </summary>
            [JsonProperty("wsdl_interface_name", NullValueHandling = NullValueHandling.Ignore)]
            public string WsdlInterfaceName { get; set; }

            /// <summary>
            ///
            /// </summary>
            [JsonProperty("wsdl_operations", NullValueHandling = NullValueHandling.Ignore)]
            public ICollection<WsdlOperationResponseViewModel> WsdlOperations { get; set; }

            /// <summary>
            ///
            /// </summary>
            /// <returns></returns>
            public override string ToString()
            {
                return WsdlInterfaceName;
            }

            /// <summary>
            ///
            /// </summary>
            public class WsdlOperationResponseViewModel : BaseModel<int>
            {
                /// <summary>
                ///
                /// </summary>
                [JsonProperty("element_type", NullValueHandling = NullValueHandling.Ignore)]
                public string ElementType
                {
                    get
                    {
                        return ServiceDescriptionElementTypeEnum.WsdlOperation.GetEnumDescription();
                    }
                }

                /// <summary>
                ///
                /// </summary>
                [JsonProperty("id_element_type", NullValueHandling = NullValueHandling.Ignore)]
                public int IdElementType
                {
                    get
                    {
                        return (int)ServiceDescriptionElementTypeEnum.WsdlOperation;
                    }
                }

                /// <summary>
                ///
                /// </summary>
                [JsonProperty("sawsdl_model_references", NullValueHandling = NullValueHandling.Ignore)]
                public ICollection<SawsdlModelReferenceViewModel> SawsdlModelReferences { get; set; }

                /// <summary>
                ///
                /// </summary>
                [JsonProperty("wsdl_infaults", NullValueHandling = NullValueHandling.Ignore)]
                public ICollection<WsdlInfaultResponseViewModel> WsdlInfaults { get; set; }

                /// <summary>
                ///
                /// </summary>
                [JsonProperty("wsdl_inputs", NullValueHandling = NullValueHandling.Ignore)]
                public ICollection<WsdlInputResponseViewModel> WsdlInputs { get; set; }

                /// <summary>
                ///
                /// </summary>
                [JsonProperty("wsdl_operation_name", NullValueHandling = NullValueHandling.Ignore)]
                public string WsdlOperationName { get; set; }

                /// <summary>
                ///
                /// </summary>
                [JsonProperty("wsdl_outfaults", NullValueHandling = NullValueHandling.Ignore)]
                public ICollection<WsdlOutfaultResponseViewModel> WsdlOutfaults { get; set; }

                /// <summary>
                ///
                /// </summary>
                [JsonProperty("wsdl_outputs", NullValueHandling = NullValueHandling.Ignore)]
                public ICollection<WsdlOutputResponseViewModel> WsdlOutputs { get; set; }

                /// <summary>
                ///
                /// </summary>
                /// <returns></returns>
                public bool ShouldSerializeWsdlInfaults()
                {
                    return WsdlInfaults?.Count > 0;
                }

                /// <summary>
                ///
                /// </summary>
                /// <returns></returns>
                public bool ShouldSerializeWsdlInputs()
                {
                    return WsdlInputs?.Count > 0;
                }

                /// <summary>
                ///
                /// </summary>
                /// <returns></returns>
                public bool ShouldSerializeWsdlOutfaults()
                {
                    return WsdlOutfaults?.Count > 0;
                }

                /// <summary>
                ///
                /// </summary>
                /// <returns></returns>
                public bool ShouldSerializeWsdlOutputs()
                {
                    return WsdlOutputs?.Count > 0;
                }

                /// <summary>
                ///
                /// </summary>
                /// <returns></returns>
                public override string ToString()
                {
                    return WsdlOperationName;
                }

                /// <summary>
                ///
                /// </summary>
                public class WsdlInfaultResponseViewModel : BaseModel<int>
                {
                    /// <summary>
                    ///
                    /// </summary>
                    [JsonProperty("element_type", NullValueHandling = NullValueHandling.Ignore)]
                    public string ElementType
                    {
                        get
                        {
                            return ServiceDescriptionElementTypeEnum.WsdlInfault.GetEnumDescription();
                        }
                    }

                    /// <summary>
                    ///
                    /// </summary>
                    [JsonProperty("id_element_type", NullValueHandling = NullValueHandling.Ignore)]
                    public int IdElementType
                    {
                        get
                        {
                            return (int)ServiceDescriptionElementTypeEnum.WsdlInfault;
                        }
                    }

                    /// <summary>
                    ///
                    /// </summary>
                    [JsonProperty("sawsdl_model_references", NullValueHandling = NullValueHandling.Ignore)]
                    public ICollection<SawsdlModelReferenceViewModel> SawsdlModelReferences { get; set; }

                    /// <summary>
                    ///
                    /// </summary>
                    [JsonProperty("wsdl_infault_name", NullValueHandling = NullValueHandling.Ignore)]
                    public string WsdlInfaultName { get; set; }

                    /// <summary>
                    ///
                    /// </summary>
                    [JsonProperty("xsd_complex_element", NullValueHandling = NullValueHandling.Ignore)]
                    public XsdComplexTypeResponseViewModel XsdComplexElement { get; set; }

                    /// <summary>
                    ///
                    /// </summary>
                    [JsonProperty("xsd_simple_element", NullValueHandling = NullValueHandling.Ignore)]
                    public XsdSimpleTypeResponseViewModel XsdSimpleElement { get; set; }

                    /// <summary>
                    ///
                    /// </summary>
                    /// <returns></returns>
                    public bool ShouldSerializeXsdComplexElement()
                    {
                        return XsdComplexElement != null;
                    }

                    /// <summary>
                    ///
                    /// </summary>
                    /// <returns></returns>
                    public bool ShouldSerializeXsdSimpleTypeElement()
                    {
                        return XsdSimpleElement != null;
                    }

                    /// <summary>
                    ///
                    /// </summary>
                    /// <returns></returns>
                    public override string ToString()
                    {
                        return WsdlInfaultName;
                    }
                }

                /// <summary>
                ///
                /// </summary>
                public class WsdlInputResponseViewModel : BaseModel<int>
                {
                    /// <summary>
                    ///
                    /// </summary>
                    [JsonProperty("element_type", NullValueHandling = NullValueHandling.Ignore)]
                    public string ElementType
                    {
                        get
                        {
                            return ServiceDescriptionElementTypeEnum.WsdlInput.GetEnumDescription();
                        }
                    }

                    /// <summary>
                    ///
                    /// </summary>
                    [JsonProperty("id_element_type", NullValueHandling = NullValueHandling.Ignore)]
                    public int IdElementType
                    {
                        get
                        {
                            return (int)ServiceDescriptionElementTypeEnum.WsdlInput;
                        }
                    }

                    /// <summary>
                    ///
                    /// </summary>
                    [JsonProperty("wsdl_input_name", NullValueHandling = NullValueHandling.Ignore)]
                    public string WsdlInputName { get; set; }

                    /// <summary>
                    ///
                    /// </summary>
                    [JsonProperty("xsd_complex_element", NullValueHandling = NullValueHandling.Ignore)]
                    public XsdComplexTypeResponseViewModel XsdComplexType { get; set; }

                    /// <summary>
                    ///
                    /// </summary>
                    [JsonProperty("xsd_simple_element", NullValueHandling = NullValueHandling.Ignore)]
                    public XsdSimpleTypeResponseViewModel XsdSimpleType { get; set; }

                    /// <summary>
                    ///
                    /// </summary>
                    /// <returns></returns>
                    public bool ShouldSerializeXsdComplexElement()
                    {
                        return XsdComplexType != null;
                    }

                    /// <summary>
                    ///
                    /// </summary>
                    /// <returns></returns>
                    public bool ShouldSerializeXsdSimpleTypeElement()
                    {
                        return XsdSimpleType != null;
                    }

                    /// <summary>
                    ///
                    /// </summary>
                    /// <returns></returns>
                    public override string ToString()
                    {
                        return WsdlInputName;
                    }
                }

                /// <summary>
                ///
                /// </summary>
                public class WsdlOutfaultResponseViewModel : BaseModel<int>
                {
                    /// <summary>
                    ///
                    /// </summary>
                    [JsonProperty("element_type", NullValueHandling = NullValueHandling.Ignore)]
                    public string ElementType
                    {
                        get
                        {
                            return ServiceDescriptionElementTypeEnum.WsdlOutfault.GetEnumDescription();
                        }
                    }

                    /// <summary>
                    ///
                    /// </summary>
                    [JsonProperty("id_element_type", NullValueHandling = NullValueHandling.Ignore)]
                    public int IdElementType
                    {
                        get
                        {
                            return (int)ServiceDescriptionElementTypeEnum.WsdlOutfault;
                        }
                    }

                    /// <summary>
                    ///
                    /// </summary>
                    [JsonProperty("sawsdl_model_references", NullValueHandling = NullValueHandling.Ignore)]
                    public ICollection<SawsdlModelReferenceViewModel> SawsdlModelReferences { get; set; }

                    /// <summary>
                    ///
                    /// </summary>
                    [JsonProperty("wsdl_outfault_name", NullValueHandling = NullValueHandling.Ignore)]
                    public string WsdlOutfaultName { get; set; }

                    /// <summary>
                    ///
                    /// </summary>
                    [JsonProperty("xsd_complex_element", NullValueHandling = NullValueHandling.Ignore)]
                    public XsdComplexTypeResponseViewModel XsdComplexElement { get; set; }

                    /// <summary>
                    ///
                    /// </summary>
                    [JsonProperty("xsd_simple_element", NullValueHandling = NullValueHandling.Ignore)]
                    public XsdSimpleTypeResponseViewModel XsdSimpleElement { get; set; }

                    /// <summary>
                    ///
                    /// </summary>
                    /// <returns></returns>
                    public bool ShouldSerializeXsdComplexElement()
                    {
                        return XsdComplexElement != null;
                    }

                    /// <summary>
                    ///
                    /// </summary>
                    /// <returns></returns>
                    public bool ShouldSerializeXsdSimpleTypeElement()
                    {
                        return XsdSimpleElement != null;
                    }

                    /// <summary>
                    ///
                    /// </summary>
                    /// <returns></returns>
                    public override string ToString()
                    {
                        return WsdlOutfaultName;
                    }
                }

                /// <summary>
                ///
                /// </summary>
                public class WsdlOutputResponseViewModel : BaseModel<int>
                {
                    /// <summary>
                    ///
                    /// </summary>
                    [JsonProperty("element_type", NullValueHandling = NullValueHandling.Ignore)]
                    public string ElementType
                    {
                        get
                        {
                            return ServiceDescriptionElementTypeEnum.WsdlOutput.GetEnumDescription();
                        }
                    }

                    /// <summary>
                    ///
                    /// </summary>
                    [JsonProperty("id_element_type", NullValueHandling = NullValueHandling.Ignore)]
                    public int IdElementType
                    {
                        get
                        {
                            return (int)ServiceDescriptionElementTypeEnum.WsdlOutput;
                        }
                    }

                    /// <summary>
                    ///
                    /// </summary>
                    [JsonProperty("wsdl_output_name", NullValueHandling = NullValueHandling.Ignore)]
                    public string WsdlOutputName { get; set; }

                    /// <summary>
                    ///
                    /// </summary>
                    [JsonProperty("xsd_complex_type", NullValueHandling = NullValueHandling.Ignore)]
                    public XsdComplexTypeResponseViewModel XsdComplexType { get; set; }

                    /// <summary>
                    ///
                    /// </summary>
                    [JsonProperty("xsd_simple_element", NullValueHandling = NullValueHandling.Ignore)]
                    public XsdSimpleTypeResponseViewModel XsdSimpleType { get; set; }

                    /// <summary>
                    ///
                    /// </summary>
                    /// <returns></returns>
                    public bool ShouldSerializeXsdComplexType()
                    {
                        return XsdComplexType != null;
                    }

                    /// <summary>
                    ///
                    /// </summary>
                    /// <returns></returns>
                    public bool ShouldSerializeXsdSimpleTypeType()
                    {
                        return XsdSimpleType != null;
                    }

                    /// <summary>
                    ///
                    /// </summary>
                    /// <returns></returns>
                    public override string ToString()
                    {
                        return WsdlOutputName;
                    }
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        public class XsdComplexTypeResponseViewModel : BaseModel<int>
        {
            /// <summary>
            ///
            /// </summary>
            [JsonProperty("element_type", NullValueHandling = NullValueHandling.Ignore)]
            public string ElementType
            {
                get
                {
                    return ServiceDescriptionElementTypeEnum.XsdComplexType.GetEnumDescription();
                }
            }

            /// <summary>
            ///
            /// </summary>
            [JsonProperty("id_element_type", NullValueHandling = NullValueHandling.Ignore)]
            public int IdElementType
            {
                get
                {
                    return (int)ServiceDescriptionElementTypeEnum.XsdComplexType;
                }
            }

            /// <summary>
            ///
            /// </summary>
            [JsonProperty("sawsdl_model_references", NullValueHandling = NullValueHandling.Ignore)]
            public ICollection<SawsdlModelReferenceViewModel> SawsdlModelReferences { get; set; }

            /// <summary>
            ///
            /// </summary>
            [JsonProperty("xsd_complex_type_name", NullValueHandling = NullValueHandling.Ignore)]
            public string XsdComplexTypeName { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public ICollection<XsdComplexTypeResponseViewModel> ChildrenXsdComplexTypes { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public ICollection<XsdSimpleTypeResponseViewModel> XsdSimpleTypes { get; set; }

            /// <summary>
            ///
            /// </summary>
            /// <returns></returns>
            public override string ToString()
            {
                return XsdComplexTypeName;
            }
        }

        /// <summary>
        ///
        /// </summary>
        public class XsdSimpleTypeResponseViewModel : BaseModel<int>
        {
            /// <summary>
            ///
            /// </summary>
            [JsonProperty("element_type", NullValueHandling = NullValueHandling.Ignore)]
            public string ElementType
            {
                get
                {
                    return ServiceDescriptionElementTypeEnum.XsdSimpleType.GetEnumDescription();
                }
            }

            /// <summary>
            ///
            /// </summary>
            [JsonProperty("id_element_type", NullValueHandling = NullValueHandling.Ignore)]
            public int IdElementType
            {
                get
                {
                    return (int)ServiceDescriptionElementTypeEnum.XsdSimpleType;
                }
            }

            /// <summary>
            ///
            /// </summary>
            [JsonProperty("sawsdl_model_references", NullValueHandling = NullValueHandling.Ignore)]
            public ICollection<SawsdlModelReferenceViewModel> SawsdlModelReferences { get; set; }

            /// <summary>
            ///
            /// </summary>
            [JsonProperty("xsd_simple_type_name", NullValueHandling = NullValueHandling.Ignore)]
            public string XsdSimpleTypeName { get; set; }

            /// <summary>
            ///
            /// </summary>
            /// <returns></returns>
            public override string ToString()
            {
                return XsdSimpleTypeName;
            }
        }
    }
}