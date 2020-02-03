using Grasews.Domain.Enums;
using Grasews.Domain.Interfaces.Entities;
using Newtonsoft.Json;

namespace Grasews.Infra.ExternalService.Cytoscape.Models
{
    public class CytoscapeNodeData : IGraphNodeData
    {
        #region Private vars

        private string _label;
        private GraphNodeTypeEnum? _nodeType;

        #endregion Private vars

        #region Public props

        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public string Id { get; set; }

        public string OntologyName { get; set; }

        [JsonProperty("label", NullValueHandling = NullValueHandling.Ignore)]
        public string Label
        {
            get
            {
                var labelAlreadyFormatted = _label.Contains("<<") && _label.Contains(">>");

                if ((NodeTypeEnum == GraphNodeTypeEnum.ModelReference || NodeTypeEnum == GraphNodeTypeEnum.OntologyTerm) && !labelAlreadyFormatted)
                {
                    //var ontologyName = UrlHelper.ExtractOntologyNameFromUrl(TermUri);
                    return $"<<{OntologyName}>>\n{_label}";
                }

                return labelAlreadyFormatted ? _label : $"<<{NodeTypeEnum}>>\n{_label}";
            }
            set
            {
                _label = value;
            }
        }

        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty("node-type", NullValueHandling = NullValueHandling.Ignore)]
        public string NodeType { get; set; }

        [JsonProperty("id-node-type", NullValueHandling = NullValueHandling.Ignore)]
        public GraphNodeTypeEnum? NodeTypeEnum
        {
            get
            {
                return _nodeType;
            }
            set
            {
                NodeType = value.ToString();
                _nodeType = value;
            }
        }

        [JsonProperty("parent", NullValueHandling = NullValueHandling.Ignore)]
        public string Parent { get; set; }

        [JsonProperty("term-uri", NullValueHandling = NullValueHandling.Ignore)]
        public string TermUri { get; set; }

        [JsonProperty("id-wsdl-element", NullValueHandling = NullValueHandling.Ignore)]
        public int IdWsdlElement { get; set; }

        [JsonProperty("id-service-description", NullValueHandling = NullValueHandling.Ignore)]
        public int IdServiceDescription { get; set; }

        [JsonProperty("id-ontology-term", NullValueHandling = NullValueHandling.Ignore)]
        public int IdOntologyTerm { get; set; }

        [JsonProperty("id-ontology", NullValueHandling = NullValueHandling.Ignore)]
        public int IdOntology { get; set; }

        #endregion Public props
    }
}