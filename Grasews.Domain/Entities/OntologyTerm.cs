namespace Grasews.Domain.Entities
{
    using System.Collections.Generic;

    public class OntologyTerm : BaseEntity<int>
    {
        #region Public props

        public int IdOntology { get; set; }

        public int? IdParentOntologyTerm { get; set; }

        public string ParentTermURI { get; set; }
        public string TermName { get; set; }

        public string TermRaw { get; set; }
        public string TermUri { get; set; }

        #endregion Public props

        #region Mark term to be removed

        private bool _itemMarkedToBeRemoved;

        public bool IsItemMarkedToBeRemoved()
        {
            return _itemMarkedToBeRemoved;
        }

        public void MarkItemToBeRemoved()
        {
            _itemMarkedToBeRemoved = true;
        }

        #endregion Mark term to be removed

        #region Depth

        public List<OntologyTerm> Depth
        {
            get
            {
                var path = new List<OntologyTerm>();
                if (ChildrenOntologyTerms != null)
                {
                    foreach (var node in ChildrenOntologyTerms)
                    {
                        var tmp = node.Depth;
                        if (tmp.Count > path.Count)
                            path = tmp;
                    }
                    path.Insert(0, this);
                }
                return path;
            }
        }

        #endregion Depth

        #region Navigations

        public virtual ICollection<OntologyTerm> ChildrenOntologyTerms { get; set; }
        public virtual ICollection<GraphNodePosition_OntologyTerm> GraphNodePosition_OntologyTerms { get; set; }
        public virtual Ontology Ontology { get; set; }
        public virtual OntologyTerm ParentOntologyTerm { get; set; }
        public virtual ICollection<SawsdlModelReference> SawsdlModelReferences { get; set; }

        #endregion Navigations

        #region ToString

        public override string ToString()
        {
            return TermName;
        }

        #endregion ToString
    }
}