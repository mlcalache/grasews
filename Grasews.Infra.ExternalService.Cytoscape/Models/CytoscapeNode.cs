using Grasews.Domain.Enums;
using Grasews.Domain.Interfaces.Entities;
using Grasews.Infra.CrossCutting.Helpers.Extensions;
using Newtonsoft.Json;

namespace Grasews.Infra.ExternalService.Cytoscape.Models
{
    public class CytoscapeNode : IGraphNode
    {
        #region Ctors

        //regular ctor
        public CytoscapeNode()
        {
        }

        //ctor for json converter to work with interface properties
        [JsonConstructor]
        public CytoscapeNode(CytoscapeNodeData data, CytoscapeNodePosition position)
        {
            Data = data;
            Position = position;
        }

        #endregion Ctors

        #region Public props

        [JsonProperty("classes", NullValueHandling = NullValueHandling.Ignore)]
        public string Classes { get; set; }

        [JsonProperty("data", NullValueHandling = NullValueHandling.Ignore)]
        public IGraphNodeData Data { get; set; }

        [JsonProperty("position", NullValueHandling = NullValueHandling.Ignore)]
        public IGraphNodePosition Position { get; set; }

        #endregion Public props

        #region ToString

        public override string ToString()
        {
            return $"[{Data.Id}][{Data.Name}][{Data.Label}]";
        }

        #endregion ToString

        public static class Factory
        {
            public static CytoscapeNode Create(GraphNodeTypeEnum nodeType, int idNode, string nodeText, int idServiceDescription, string idGroupNode, string classes)
            {
                return new CytoscapeNode
                {
                    Data = new CytoscapeNodeData
                    {
                        Id = $"{nodeType.GetEnumDescription()}-{idNode}-{nodeText}",
                        Label = nodeText,
                        Name = nodeText,
                        NodeTypeEnum = nodeType,
                        Parent = idGroupNode,
                        IdWsdlElement = idNode,
                        IdServiceDescription = idServiceDescription
                    },
                    Classes = classes
                };
            }

            public static CytoscapeNode CreateServiceNode(string serviceName)
            {
                return new CytoscapeNode
                {
                    Data = new CytoscapeNodeData
                    {
                        Id = "group-wsdl",
                        Label = serviceName,
                        Name = "group-wsdl",
                        NodeTypeEnum = GraphNodeTypeEnum.Document
                    },
                    Classes = "group"
                };
            }
        }
    }
}