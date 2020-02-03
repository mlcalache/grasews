using Grasews.API.Models;
using Grasews.Domain.Interfaces.Services;
using Grasews.Infra.CrossCutting.Helpers.RestClient.Enums;
using Grasews.Infra.CrossCutting.Helpers.RestClient.Interfaces;
using Grasews.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Grasews.Web.Controllers
{
    public class OntologyController : BaseController
    {
        #region Private vars

        private readonly IOntologyService _ontologyService;

        #endregion Private vars

        #region Ctors

        public OntologyController(IOntologyService ontologyService,
            IAPIRestClient apiRestClient)
            : base(apiRestClient)
        {
            _ontologyService = ontologyService;
        }

        #endregion Ctors

        #region Actions

        public async Task<ActionResult> Delete(int idOntology)
        {
            #region Set authorization to API calls

            SetAuthorizationToAPICalls();

            #endregion Set authorization to API calls

            Ontology_ApiResponseViewModel ontology = null;

            var ontologyRequest = CreateApiRequest($"api/ontologies/{idOntology}", HttpMethodENUM.GET, "application/x-www-form-urlencoded");
            var ontologyResponse = await _apiRestClient.ExecuteAsync<Ontology_ApiResponseViewModel>(ontologyRequest);

            if (ontologyResponse.StatusCode == System.Net.HttpStatusCode.OK)
            {
                ontology = ontologyResponse.Data;

                if (ontology.IdOwnerUser != IdUser)
                {
                    AddToastDangerMessage(WebResource.UserIsNotTheOwner);
                }
            }

            var ontologyDeleteViewModel = Mapper.Map<Ontology_MvcDeleteViewModel>(ontology);

            return View(ontologyDeleteViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(int idOntology, Ontology_MvcDeleteViewModel ontologyDeleteViewModel)
        {
            #region Set authorization to API calls

            SetAuthorizationToAPICalls();

            #endregion Set authorization to API calls

            Ontology_ApiResponseViewModel ontology;

            var ontologyRequest = CreateApiRequest($"api/ontologies/{idOntology}", HttpMethodENUM.GET, "application/x-www-form-urlencoded");
            var ontologyResponse = await _apiRestClient.ExecuteAsync<Ontology_ApiResponseViewModel>(ontologyRequest);

            if (ontologyResponse.StatusCode == System.Net.HttpStatusCode.OK)
            {
                ontology = ontologyResponse.Data;

                if (ontology != null)
                {
                    if (ontology.IdOwnerUser != IdUser)
                    {
                        AddToastDangerMessage(WebResource.UserIsNotTheOwner);

                        return View();
                    }
                }
                else
                {
                    AddToastWarningMessage(WebResource.AnErrorHasOccuredServiceDescriptionNotFoundInDatabase);

                    return View();
                }

                try
                {
                    var ontologyRemoveRequest = CreateApiRequest($"api/ontologies/{idOntology}", HttpMethodENUM.DELETE, "application/x-www-form-urlencoded");
                    var ontologyRemoveResponse = await _apiRestClient.ExecuteAsync<Ontology_ApiResponseViewModel>(ontologyRemoveRequest);

                    AddToastSuccessMessage(WebResource.OntologyDeleteSuccess);

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    AddToastDangerMessage(WebResource.OntologyDeleteFail);

                    return View();
                }
            }

            AddToastDangerMessage(WebResource.OntologyDeleteFail);

            return View();
        }

        public ActionResult Details(int idOntology)
        {
            var onntology = _ontologyService.Get(idOntology);

            if (onntology == null)
            {
                AddToastWarningMessage("ops");

                return RedirectToAction("Index");
            }
            else
            {
                var ontologyViewModel = Mapper.Map<Ontology_MvcViewModel>(onntology);

                return View(ontologyViewModel);
            }
        }

        public ActionResult Edit(int idOntology)
        {
            var ontology = _ontologyService.Get(idOntology);

            if (ontology != null)
            {
                var ontologyViewModel = Mapper.Map<Ontology_MvcViewModel>(ontology);

                return View(ontologyViewModel);
            }
            else
            {
                AddToastWarningMessage("kjsjsjsjssj");

                return RedirectToAction("Index");
            }
        }

        public ActionResult Index()
        {
            ViewBag.LoggedInUserFullName = UserFullName;

            var webServices = _ontologyService.GetAllByUser(IdUser);

            var webServicesViewModel = Mapper.Map<List<Ontology_MvcViewModel>>(webServices);

            if (webServicesViewModel != null && webServicesViewModel.Count() == 0)
            {
                AddToastWarningMessage(WebResource.NoOntologyInDatabase);
            }

            return View(webServicesViewModel);
        }

        #endregion Actions
    }
}