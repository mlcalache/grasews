using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Grasews.UI.WebServiceSamples
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ColorService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select ColorService.svc or ColorService.svc.cs at the Solution Explorer and start debugging.
    public class ColorService : IColorService
    {
        public List<string> GetColors()
        {
            return new string[] { "blue", "red", "green", "yellow" }.ToList();
        }

        //public async Task<List<string>> GetColorsAsync()
        //{
        //    return new string[] { "blue", "red", "green", "yellow" }.ToList();
        //}
    }
}