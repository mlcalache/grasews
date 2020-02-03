using Grasews.Infra.CrossCutting.Helpers.RestClient.Interfaces;
using System.Web.Mvc;

namespace Grasews.Web.Controllers
{
    public class AdminController : BaseController
    {
        public AdminController(IAPIRestClient apiRestClient)
            : base(apiRestClient)
        {
        }

        public ActionResult Index()
        {
            ViewBag.LoggedInUserFullName = UserFullName;

            return View();
        }
    }
}