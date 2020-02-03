using Grasews.Domain.Interfaces.Entities;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace Grasews.Infra.ExternalService.Cytoscape.Models
{
    public class CytoscapeObject : IGraphDataObject
    {
        //regular ctor
        public CytoscapeObject()
        {
        }

        //ctor for json converter to work with interface properties
        [JsonConstructor]
        public CytoscapeObject(ICollection<CytoscapeEdge> edges, ICollection<CytoscapeNode> nodes)
        {
            Edges = edges.Cast<IGraphEdge>().ToList(); 
            Nodes = nodes.Cast<IGraphNode>().ToList();
        }

        [JsonProperty("edges", NullValueHandling = NullValueHandling.Ignore)]
        public ICollection<IGraphEdge> Edges { get; set; }

        [JsonProperty("nodes", NullValueHandling = NullValueHandling.Ignore)]
        public ICollection<IGraphNode> Nodes { get; set; }
    }
}