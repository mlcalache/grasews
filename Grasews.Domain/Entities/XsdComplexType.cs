using Grasews.Domain.Interfaces.Entities;
using System.Collections.Generic;

namespace Grasews.Domain.Entities
{
    public class XsdComplexType : BaseXsd, ICanBeAnnotatedWithModelReference, ICanBeAnnotatedWithSchemaMapping, ICanHaveIssues
    {
        #region Props

        public int IdXsdDocument { get; set; }
        public string XsdComplexTypeName { get; set; }
        public string XsdComplexTypeElementType { get; set; }

        #endregion Props

        #region Navigations

        public virtual ICollection<GraphNodePosition_XsdComplexType> GraphNodePosition_XsdComplexTypes { get; set; }
        public virtual WsdlInfault WsdlInfault { get; set; }
        public virtual WsdlInput WsdlInput { get; set; }
        public virtual WsdlOutfault WsdlOutfault { get; set; }
        public virtual WsdlOutput WsdlOutput { get; set; }
        public virtual XsdDocument XsdDocument { get; set; }
        public virtual ICollection<XsdComplexType> ParentsXsdComplexTypes { get; set; }
        public virtual ICollection<XsdComplexType> ChildrenXsdComplexTypes { get; set; }
        public virtual ICollection<XsdSimpleType> XsdSimpleTypes { get; set; }
        public virtual ICollection<Issue> Issues { get; set; }

        #endregion Navigations

        #region Semantic annotations

        public string LiftingSchemaMapping { get; set; }

        public string LoweringSchemaMapping { get; set; }

        public virtual ICollection<SawsdlModelReference> SawsdlModelReferences { get; set; }

        #endregion Semantic annotations

        #region ToString

        public override string ToString()
        {
            return $"[c-t] {XsdComplexTypeName}";
        }

        #endregion ToString

        //#region Mark term to be removed

        //private bool _itemMarkedToBeRemoved;

        //public bool IsItemMarkedToBeRemoved()
        //{
        //    return _itemMarkedToBeRemoved;
        //}

        //public void MarkItemToBeRemoved()
        //{
        //    _itemMarkedToBeRemoved = true;
        //}

        //#endregion Mark term to be removed

        //#region Depth

        //public List<XsdComplexType> Depth
        //{
        //    get
        //    {
        //        var path = new List<XsdComplexType>();
        //        if (ChildrenXsdComplexType != null)
        //        {
        //            foreach (var node in ChildrenXsdComplexType)
        //            {
        //                var tmp = node.Depth;
        //                if (tmp.Count > path.Count)
        //                    path = tmp;
        //            }
        //            path.Insert(0, this);
        //        }
        //        return path;
        //    }
        //}

        //#endregion Depth
    }
}