using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;

namespace Grasews.UI.WebServiceSamples
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IColorService" in both code and config file together.
    [ServiceContract]
    public interface IColorService
    {
        [OperationContract]
        List<string> GetColors();

        //[OperationContract]
        //Task<List<string>> GetColorsAsync();
    }
}