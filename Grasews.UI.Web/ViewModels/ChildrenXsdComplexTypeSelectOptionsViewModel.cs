using System.Collections.Generic;

namespace Grasews.Web.ViewModels
{
    public class ChildrenXsdComplexTypeSelectOptionsViewModel
    {
        public IEnumerable<Grasews.API.Models.ParseWsdl_ApiResponseViewModel.XsdComplexTypeResponseViewModel> ChildrenXsdComplexTypes { get; set; }
        public int Level { get; set; }
    }
}