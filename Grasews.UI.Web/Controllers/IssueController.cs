using Grasews.Domain.Entities;
using Grasews.Domain.Interfaces.Services;
using Grasews.Infra.CrossCutting.Helpers.RestClient.Interfaces;
using Grasews.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Grasews.Web.Controllers
{
    public class IssueController : BaseController
    {
        #region Private vars

        private readonly IIssueService _issueService;

        #endregion Private vars

        #region Ctors

        public IssueController(IIssueService issueService,
            IAPIRestClient apiRestClient)
            : base(apiRestClient)
        {
            _issueService = issueService;
        }

        #endregion Ctors

        #region Actions

        public ActionResult Answers(int idIssue)
        {
            var issue = _issueService.Get(idIssue);

            var issueViewModel = Mapper.Map<Issue_MvcViewModel>(issue);

            var answersViewModel = issueViewModel.Answers;

            ViewBag.IdIssue = issueViewModel.Id;
            ViewBag.IssueDescription = issueViewModel.Description;

            return View(answersViewModel);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Issue_MvcCreateViewModel issueCreateViewModel)
        {
            try
            {
                var issue = Mapper.Map<Issue>(issueCreateViewModel);

                issue.IdOwnerUser = IdUser;

                _issueService.Create(issue);

                AddToastSuccessMessage(WebResource.IssueInsertSuccess);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                AddToastSuccessMessage(WebResource.IssueInsertFail);

                return View();
            }
        }

        public ActionResult Delete(int idIssue)
        {
            var issue = _issueService.Get(idIssue);

            var issueViewModel = Mapper.Map<Issue_MvcViewModel>(issue);

            return View(issueViewModel);
        }

        [HttpPost]
        public ActionResult Delete(int idIssue, Issue_MvcViewModel issueViewModel)
        {
            try
            {
                var issue = Mapper.Map<Issue>(issueViewModel);

                _issueService.Remove(issue);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        public ActionResult Index(int? idServiceDescription)
        {
            List<Issue> issues;
            ViewBag.LoggedInUserFullName = UserFullName;

            if (idServiceDescription.HasValue)
            {
                issues = _issueService.GetAllByServiceDescription(idServiceDescription.Value);
            }
            else
            {
                issues = _issueService.GetAllByUser(IdUser);
            }

            if (!issues.Any())
            {
                AddToastWarningMessage(WebResource.NoIssueForTheServiceDescription);
            }

            var issuesViewModel = Mapper.Map<List<Issue_MvcViewModel>>(issues);

            return View(issuesViewModel);
        }

        #endregion Actions
    }
}