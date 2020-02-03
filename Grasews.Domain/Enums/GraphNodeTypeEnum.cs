using System.ComponentModel;

namespace Grasews.Domain.Enums
{
    public enum GraphNodeTypeEnum
    {
        Document = 0,

        //Should be equal to ServiceDescriptionElementType.WsdlInterface
        [Description("wsdl-interface")]
        Interface = 1,

        //Should be equal to ServiceDescriptionElementType.WsdlOperation
        [Description("wsdl-operation")]
        Operation = 2,

        //Should be equal to ServiceDescriptionElementType.WsdlInput
        [Description("wsdl-input")]
        Input = 3,

        //Should be equal to ServiceDescriptionElementType.WsdlOutput
        [Description("wsdl-output")]
        Output = 4,

        //Should be equal to ServiceDescriptionElementType.WsdlInfault
        [Description("wsdl-infault")]
        Infault = 5,

        //Should be equal to ServiceDescriptionElementType.WsdlOutfault
        [Description("wsdl-outfault")]
        Outfault = 6,

        //Should be equal to ServiceDescriptionElementType.WsdlOutfault
        [Description("wsdl-fault")]
        Fault = 7,

        //Should be equal to ServiceDescriptionElementType.XsdComplexType
        [Description("xsd-complex-type")]
        ComplexType = 8,

        //Should be equal to ServiceDescriptionElementType.XsdSimpleType
        [Description("xsd-simple-type")]
        SimpleType = 9,

        ModelReference = 10,

        OntologyTerm = 11
    }
}