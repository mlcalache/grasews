using System.Web.Mvc;

namespace Grasews.Web.Controllers
{
    [AllowAnonymous]
    public class ProjectDocumentationController : Controller
    {
        // GET: DocumentacaoProjeto
        public ActionResult Index()
        {
            return View();
        }
    }
}