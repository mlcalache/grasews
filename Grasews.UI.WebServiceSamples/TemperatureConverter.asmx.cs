using System.Web.Services;

namespace Grasews.UI.WebServiceSamples
{
    /// <summary>
    /// Summary description for TemperatureConverter
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
    // [System.Web.Script.Services.ScriptService]
    public class TemperatureConverter : System.Web.Services.WebService
    {
        [WebMethod]
        public double ConvertFahrenheitToCelsius(double fahrenheit)
        {
            var celsius = (((fahrenheit - 32) * 5) / 9);

            return celsius;
        }
    }
}