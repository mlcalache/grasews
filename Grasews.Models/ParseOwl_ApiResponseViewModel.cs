using Newtonsoft.Json;
using System.Collections.Generic;

namespace Grasews.API.Models
{
    /// <summary>
    ///
    /// </summary>
    public class ParseOwl_ApiResponseViewModel
    {
        /// <summary>
        ///
        /// </summary>
        [JsonProperty("ontology_name", NullValueHandling = NullValueHandling.Ignore)]
        public string OntologyName { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonProperty("ontology_terms", NullValueHandling = NullValueHandling.Ignore)]
        public List<OntologyTermViewModel> OntologyTerms { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonProperty("xml", NullValueHandling = NullValueHandling.Ignore)]
        public string Xml { get; set; }

        /// <summary>
        ///
        /// </summary>
        public class OntologyTermViewModel
        {
            /// <summary>
            ///
            /// </summary>
            [JsonProperty("term_name", NullValueHandling = NullValueHandling.Ignore)]
            public string TermName { get; set; }

            /// <summary>
            ///
            /// </summary>
            [JsonProperty("term_raw", NullValueHandling = NullValueHandling.Ignore)]
            public string TermRaw { get; set; }

            /// <summary>
            ///
            /// </summary>
            [JsonProperty("term_uri", NullValueHandling = NullValueHandling.Ignore)]
            public string TermURI { get; set; }

            /// <summary>
            ///
            /// </summary>
            [JsonProperty("children_ontology_terms", NullValueHandling = NullValueHandling.Ignore)]
            public List<OntologyTermViewModel> ChildrenOntologyTerms { get; set; }

            /// <summary>
            ///
            /// </summary>
            [JsonProperty("children_ontology_terms_quantity", NullValueHandling = NullValueHandling.Ignore)]
            public int ChildrenOntologyTermsQuantity
            {
                get
                {
                    return ChildrenOntologyTerms != null ? ChildrenOntologyTerms.Count : 0;
                }
            }

            /// <summary>
            ///
            /// </summary>
            /// <returns></returns>
            public bool ShouldSerializeChildrenOntologyTerms()
            {
                return ChildrenOntologyTerms?.Count > 0;
            }

            /// <summary>
            ///
            /// </summary>
            /// <returns></returns>
            public override string ToString()
            {
                return TermName;
            }
        }
    }
}