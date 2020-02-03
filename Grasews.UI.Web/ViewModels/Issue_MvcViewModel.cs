using Grasews.Domain.Enums;
using Grasews.Infra.CrossCutting.Helpers.Extensions;
using System;
using System.Collections.Generic;

namespace Grasews.Web.ViewModels
{
    public class Issue_MvcViewModel : BaseMvcModel<int>
    {
        public ICollection<Issue_Answer_MvcViewModel> Answers { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool Solved { get; set; }
        public int IdOwnerUser { get; set; }
        public int IdServiceDescription { get; set; }
        public DateTime RegistrationDateTime { get; set; }
        public int? IdWsdlInterface { get; set; }
        public int? IdWsdlOperation { get; set; }
        public int? IdWsdlFault { get; set; }

        //public int? IdWsdlInput { get; set; }
        //public int? IdWsdlOutput { get; set; }
        //public int? IdWsdlOutfault { get; set; }
        //public int? IdWsdlInfault { get; set; }
        public int? IdXsdComplexType { get; set; }

        public int? IdXsdSimpleType { get; set; }

        public int? IdServiceDescriptionElementType
        {
            get
            {
                return ServiceDescriptionElementType.HasValue ? (int)ServiceDescriptionElementType.Value : default(int?);
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
                else if (IdWsdlFault.HasValue)
                {
                    return ServiceDescriptionElementTypeEnum.WsdlFault;
                }
                //else if (IdWsdlInput.HasValue)
                //{
                //    return ServiceDescriptionElementTypeEnum.WsdlInput;
                //}
                //else if (IdWsdlOutput.HasValue)
                //{
                //    return ServiceDescriptionElementTypeEnum.WsdlOutput;
                //}
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

        public string ServiceDescriptionElementTypeLabel
        {
            get
            {
                return ServiceDescriptionElementType.HasValue ? ServiceDescriptionElementType.Value.GetEnumDescription() : string.Empty;
            }
        }

        public int? IdServiceDescriptionElement
        {
            get
            {
                if (IdWsdlInterface.HasValue)
                {
                    return IdWsdlInterface.Value;
                }
                else if (IdWsdlOperation.HasValue)
                {
                    return IdWsdlOperation.Value;
                }
                else if (IdWsdlFault.HasValue)
                {
                    return IdWsdlFault.Value;
                }
                //else if (IdWsdlInput.HasValue)
                //{
                //    return IdWsdlInput.Value;
                //}
                //else if (IdWsdlOutput.HasValue)
                //{
                //    return IdWsdlOutput.Value;
                //}
                //else if (IdWsdlInfault.HasValue)
                //{
                //    return IdWsdlInfault.Value;
                //}
                //else if (IdWsdlOutfault.HasValue)
                //{
                //    return IdWsdlOutfault.Value;
                //}
                else if (IdXsdComplexType.HasValue)
                {
                    return IdXsdComplexType.Value;
                }
                else if (IdXsdSimpleType.HasValue)
                {
                    return IdXsdSimpleType.Value;
                }
                return null;
            }
        }

        public string ServiceDescriptionElementLabel { get; set; }
    }
}