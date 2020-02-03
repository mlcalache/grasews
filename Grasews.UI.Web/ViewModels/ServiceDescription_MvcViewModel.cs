using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Grasews.Web.ViewModels
{
    public class ServiceDescription_MvcViewModel : BaseMvcModel<int>
    {
        #region Public props

        [Display(Name = "OwnerUser", ResourceType = typeof(WebResource))]
        public User_MvcCreateViewModel OwnerUser { get; set; }

        [Display(Name = "ServiceDescription_Ontology", ResourceType = typeof(WebResource))]
        public ICollection<ServiceDescription_Ontology_MvcResponseViewModel> ServiceDescription_Ontology { get; set; }

        [Display(Name = "ServiceDescription_User", ResourceType = typeof(WebResource))]
        public ICollection<ServiceDescription_User_MvcViewModel> ServiceDescription_User { get; set; }

        [Display(Name = "ServiceName", ResourceType = typeof(WebResource))]
        public string ServiceName { get; set; }

        [Display(Name = "Wsdl", ResourceType = typeof(WebResource))]
        public string Xml { get; set; }

        [Display(Name = "CytoscapeGraphJson", ResourceType = typeof(WebResource))]
        public string CytoscapeGraphJson { get; set; }

        #endregion Public props

        #region Public calculated props

        public bool HasLiftingSchemaMapping => Xml.Contains("liftingSchemaMapping=");

        public bool HasLoweringSchemaMapping => Xml.Contains("loweringSchemaMapping=");

        [Display(Name = "ModelReference", ResourceType = typeof(WebResource))]
        public bool HasModelReference => Xml.Contains("modelReference=");

        [Display(Name = "SchemaMapping", ResourceType = typeof(WebResource))]
        public bool HasSchemaMapping => HasLiftingSchemaMapping || HasLoweringSchemaMapping;

        #endregion Public calculated props
    }
}