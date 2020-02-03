using Grasews.Domain.Interfaces.Services;
using Grasews.Infra.CrossCutting.Helpers.RestClient.Interfaces;
using Grasews.Web.ViewModels;
using System.Web.Mvc;

namespace Grasews.Web.Controllers
{
    public class IssueAnswerController : BaseController
    {
        #region Private vars

        private readonly IIssueAnswerService _issueAnswerService;

        #endregion Private vars

        #region Ctors

        public IssueAnswerController(IIssueAnswerService issueAnswerService,
            IAPIRestClient apiRestClient)
            : base(apiRestClient)
        {
            _issueAnswerService = issueAnswerService;
        }

        #endregion Ctors

        #region Actions

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Issue_Answer_MvcViewModel issueAnswer)
        {
            try
            {
                //TODO: [Issue] Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(int idIssueAnswer)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Delete(int idIssueAnswer, Issue_Answer_MvcViewModel issueAnswer)
        {
            try
            {
                //TODO: [Issue] Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Details(int idIssueAnswer)
        {
            return View();
        }

        public ActionResult Index(int idIssue)
        {
            var issueAnswers = _issueAnswerService.GetAll(idIssue);
            return View(issueAnswers);
        }

        #endregion Actions
    }
}