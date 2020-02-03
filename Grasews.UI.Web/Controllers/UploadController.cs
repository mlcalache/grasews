using Grasews.API.Models;
using Grasews.Infra.CrossCutting.Helpers;
using Grasews.Infra.CrossCutting.Helpers.RestClient.Enums;
using Grasews.Infra.CrossCutting.Helpers.RestClient.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Grasews.Web.Controllers
{
    /// <summary>
    ///
    /// </summary>
    public class UploadController : BaseController
    {
        #region Ctor

        /// <summary>
        ///
        /// </summary>
        /// <param name="apiRestClient"></param>
        public UploadController(IAPIRestClient apiRestClient) : base(apiRestClient)
        {
        }

        #endregion Ctor

        #region Actions

        /// <summary>
        ///
        /// </summary>
        /// <param name="stringByteArray"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> UploadOntologyFile(string stringByteArray)
        {
            #region Set authorization to API calls

            SetAuthorizationToAPICalls();

            #endregion Set authorization to API calls

            #region Get Ontologies Cookie

            string[] ontologiesCookie = null;

            var cookie = GetCookie(GRASEWS_ONTOLOGIES_OPENED_COOKIE);

            if (cookie != null)
            {
                ontologiesCookie = cookie.Value.Split(',');
            }

            #endregion Get Ontologies Cookie

            #region Read uploaded file

            var fileContent = ReadUploadedFile(stringByteArray);

            #endregion Read uploaded file

            #region Save ontology

            var postOntologyRequestBody = new Ontology_ApiRequestCreateModel { Xml = fileContent };
            var postOntologyRequest = CreateApiRequest("api/ontologies", HttpMethodENUM.POST, "application/x-www-form-urlencoded");
            postOntologyRequest.AddRequestBodyParameter(postOntologyRequestBody);
            var postOntologyResponse = await _apiRestClient.ExecuteAsync<Ontology_ApiResponseViewModel>(postOntologyRequest);

            #endregion Save ontology

            if (postOntologyResponse.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var ontology = postOntologyResponse.Data;

                #region Get html for the tree view menu

                var htmlTreeViewMenuRequest = CreateApiRequest($"api/ontologies/{ontology.Id}/html/tree-view-menu", HttpMethodENUM.GET, "application/x-www-form-urlencoded");
                var htmlTreeViewMenuResponse = await _apiRestClient.ExecuteAsync<string>(htmlTreeViewMenuRequest);
                var htmlTreeViewMenu = htmlTreeViewMenuResponse.Data;

                ontology.HtmlTreeViewMenu = htmlTreeViewMenu;

                #endregion Get html for the tree view menu

                #region Create cookie with ontologies (ids) opened

                var ontologiesForCookie = new List<string>();
                if (ontologiesCookie != null && ontologiesCookie.Length > 0)
                {
                    ontologiesForCookie.AddRange(ontologiesCookie.ToList());
                }
                ontologiesForCookie.Add(ontology.Id.ToString());

                SetCookie(GRASEWS_ONTOLOGIES_OPENED_COOKIE, string.Join(",", ontologiesForCookie));

                #endregion Create cookie with ontologies (ids) opened

                return Json(new { ontology }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { message = postOntologyResponse.Content }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="stringByteArray"></param>
        /// <param name="filename"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> UploadServiceDescriptionFile(string stringByteArray, string filename)
        {
            var isServiceDescriptionAlreadyOpened = false;
            var isServiceDescriptionAlreadySaved = false;

            #region Set authorization to API calls

            SetAuthorizationToAPICalls();

            #endregion Set authorization to API calls

            #region Get Service Descriptions Cookie

            string[] serviceDescriptionsCookie = null;

            var cookie = GetCookie(GRASEWS_SERVICEDESCRIPTIONS_OPENED_COOKIE);

            if (cookie != null)
            {
                serviceDescriptionsCookie = cookie.Value.Split(',');
            }

            #endregion Get Service Descriptions Cookie

            #region Check if the service description is already in database and if the service description is already opened (cookies)

            var allServiceDescriptionsRequest = CreateApiRequest("api/service-descriptions", HttpMethodENUM.GET, "application/x-www-form-urlencoded");
            var serviceDescriptionCollectionResponse = await _apiRestClient.ExecuteAsync<ServiceDescriptionCollection_ApiResponseViewModel>(allServiceDescriptionsRequest);

            if (serviceDescriptionCollectionResponse.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var serviceDescriptions = serviceDescriptionCollectionResponse.Data;
                var serviceDescriptionAlreadySaved = serviceDescriptions.ServiceDescriptionViewModels.FirstOrDefault(x => x.ServiceName.ToUpper() == filename.ToUpper());
                isServiceDescriptionAlreadySaved = serviceDescriptionAlreadySaved != null;

                if (isServiceDescriptionAlreadySaved)
                {
                    if (serviceDescriptionsCookie?.Length > 0)
                    {
                        isServiceDescriptionAlreadyOpened = serviceDescriptionsCookie.Any(x => x == serviceDescriptionAlreadySaved.Id.ToString());
                    }
                }
            }

            if (isServiceDescriptionAlreadyOpened)
            {
                return Json(new { message = "Service Description is already opened." }, JsonRequestBehavior.AllowGet);
            }

            if (isServiceDescriptionAlreadySaved)
            {
                return Json(new { message = "Service Description has already been saved in the user's repository. Please, use the Saved Service Description instead of the New Service Description menu." }, JsonRequestBehavior.AllowGet);
            }

            #endregion Check if the service description is already in database and if the service description is already opened (cookies)

            #region Read uploaded file

            var fileContent = ReadUploadedFile(stringByteArray);

            #endregion Read uploaded file

            #region Save Service Description

            var model = new ServiceDescription_ApiRequestCreateModel { ServiceName = filename, Xml = fileContent };
            var saveServiceDescriptionRequest = CreateApiRequest("api/service-descriptions", HttpMethodENUM.POST, "application/x-www-form-urlencoded");
            saveServiceDescriptionRequest.AddRequestBodyParameter(model);
            var serviceDescriptionResponse = await _apiRestClient.ExecuteAsync<ServiceDescription_ApiResponseViewModel>(saveServiceDescriptionRequest);
            var serviceDescription = serviceDescriptionResponse.Data;

            #endregion Save Service Description

            #region Get html for the tree view menu

            var htmlTreeViewMenuRequest = CreateApiRequest($"api/service-descriptions/{serviceDescription.Id}/html/tree-view-menu", HttpMethodENUM.GET, "application/x-www-form-urlencoded");
            var htmlTreeViewMenuResponse = await _apiRestClient.ExecuteAsync<string>(htmlTreeViewMenuRequest);
            var htmlTreeViewMenu = htmlTreeViewMenuResponse.Data;

            serviceDescription.HtmlTreeViewMenu = htmlTreeViewMenu;

            #endregion Get html for the tree view menu

            #region Create cookie with service descriptions (ids) opened

            var serviceDescriptionsForCookie = new List<string>();
            if (serviceDescriptionsCookie != null && serviceDescriptionsCookie.Length > 0)
            {
                serviceDescriptionsForCookie.AddRange(serviceDescriptionsCookie.ToList());
            }
            serviceDescriptionsForCookie.Add(serviceDescription.Id.ToString());

            SetCookie(GRASEWS_SERVICEDESCRIPTIONS_OPENED_COOKIE, string.Join(",", serviceDescriptionsForCookie));

            #endregion Create cookie with service descriptions (ids) opened

            SetCookie(GRASEWS_SERVICEDESCRIPTION_ON_VIEW_COOKIE, serviceDescription.Id.ToString());

            return Json(new { serviceDescription }, JsonRequestBehavior.AllowGet);
        }

        #endregion Actions

        #region Private methods

        /// <summary>
        ///
        /// </summary>
        /// <param name="stringByteArray"></param>
        /// <returns></returns>
        private string ReadUploadedFile(string stringByteArray)
        {
            string fileContent;
            var myByteArray = Convert.FromBase64String(stringByteArray);
            var ms = new MemoryStream(myByteArray);
            using (var sr = new StreamReader(ms))
            {
                fileContent = sr.ReadToEnd();
            }

            //var doc = new XmlDocument();
            //doc.Load(fileContent);
            //fileContent = doc.DocumentElement.OuterXml;

            fileContent = XmlHelper.EscapeXml(fileContent);
            fileContent = fileContent.Replace("&", "%26");

            return fileContent;
        }

        #endregion Private methods
    }
}