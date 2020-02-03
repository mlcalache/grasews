using Grasews.Domain.Interfaces.Entities;
using Newtonsoft.Json;

namespace Grasews.Infra.ExternalService.Cytoscape.Models
{
    public class CytoscapeEdge : IGraphEdge
    {
        //regular ctor
        public CytoscapeEdge()
        {
        }

        //ctor for json converter to work with interface properties
        [JsonConstructor]
        public CytoscapeEdge(CytoscapeEdgeData data)
        {
            Data = data;
        }

        [JsonProperty("classes", NullValueHandling = NullValueHandling.Ignore)]
        public string Classes { get; set; }

        [JsonProperty("data", NullValueHandling = NullValueHandling.Ignore)]
        public IGraphEdgeData Data { get; set; }

        public override string ToString()
        {
            return $"[{Data.Source}]->[{Data.Target}]";
        }
    }
}