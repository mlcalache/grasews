//namespace Grasews.Infra.ExternalService.Cytoscape.Models
//{
    //public class CytoscapeOntologyInfo
    //{
    //    #region Private vars

    //    private string _ontologyName;
    //    private string _ontologyUrl;

    //    #endregion Private vars

    //    #region Ctors

    //    public OntologyInfo()
    //    {
    //    }

    //    #endregion Ctors

    //    #region Public props

    //    [JsonIgnore]
    //    public string OntologyName
    //    {
    //        get
    //        {
    //            if (string.IsNullOrEmpty(_ontologyName))
    //            {
    //                _ontologyName = UrlHelper.Instance.ExtractOntologyNameFromUrl(TermUri);
    //            }
    //            return _ontologyName;
    //        }
    //    }

    //    [JsonIgnore]
    //    public string OntologyUrl
    //    {
    //        get
    //        {
    //            if (string.IsNullOrEmpty(_ontologyUrl))
    //            {
    //                _ontologyUrl = UrlHelper.Instance.ExtractOntologyUrlFromUrl(TermUri);
    //            }
    //            return _ontologyUrl;
    //        }
    //    }

    //    [JsonProperty("TermUri")]
    //    public string TermUri { get; set; }

    //    #endregion Public props

    //    #region Overrides

    //    public override string ToString()
    //    {
    //        return OntologyName;
    //    }

    //    #endregion Overrides
    //}
//}