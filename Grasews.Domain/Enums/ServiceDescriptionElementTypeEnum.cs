using System.ComponentModel;

namespace Grasews.Domain.Enums
{
    public enum ServiceDescriptionElementTypeEnum
    {
        //Should be equal to GraphNodeTypeEnum.Interface
        [Description("Wsdl Interface")]
        WsdlInterface = 1,

        //Should be equal to GraphNodeTypeEnum.Operation
        [Description("Wsdl Operation")]
        WsdlOperation = 2,

        //Should be equal to GraphNodeTypeEnum.Input
        [Description("Wsdl Input")]
        WsdlInput = 3,

        //Should be equal to GraphNodeTypeEnum.Output
        [Description("Wsdl Output")]
        WsdlOutput = 4,

        //Should be equal to GraphNodeTypeEnum.Infault
        [Description("Wsdl Infault")]
        WsdlInfault = 5,

        //Should be equal to GraphNodeTypeEnum.Outfault
        [Description("Wsdl Outfault")]
        WsdlOutfault = 6,

        //Should be equal to GraphNodeTypeEnum.Fault
        [Description("Wsdl Fault")]
        WsdlFault = 7,

        //Should be equal to GraphNodeTypeEnum.ComplexType
        [Description("Xsd Complex Type")]
        XsdComplexType = 8,

        //Should be equal to GraphNodeTypeEnum.SimpleType
        [Description("Xsd Simple Type")]
        XsdSimpleType = 9
    }
}