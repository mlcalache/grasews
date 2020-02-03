using AutoMapper;
using Grasews.API.Models;
using Grasews.Application.DTOs;
using Grasews.Domain.Entities;
using Grasews.Domain.Exceptions;
using Grasews.Domain.Interfaces.Services;
using Grasews.Infra.CrossCutting.Helpers.Extensions;
using Grasews.Infra.ExternalService.Cytoscape.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;

namespace Grasews.API.Controllers
{
    /// <summary>
    ///
    /// </summary>
    [Authorize]
    [RoutePrefix("api/service-descriptions/{id:int}")]
    [DisplayName("Sawsdl for Service Descriptions")]
    public class SawsdlApiController : BaseApiController
    {
        #region Private vars

        /// <summary>
        ///
        /// </summary>
        private readonly IGraphService _graphService;

        /// <summary>
        ///
        /// </summary>
        private readonly IOntology_UserService _ontology_UserService;

        /// <summary>
        ///
        /// </summary>
        private readonly IOntologyService _ontologyService;

        /// <summary>
        ///
        /// </summary>
        private readonly ISawsdlService _sawsdlService;

        /// <summary>
        ///
        /// </summary>
        private readonly IServiceDescription_OntologyService _serviceDescription_OntologyService;

        /// <summary>
        ///
        /// </summary>
        private readonly IServiceDescription_UserService _serviceDescription_UserService;

        /// <summary>
        ///
        /// </summary>
        private readonly IServiceDescriptionService _serviceDescriptionService;

        #endregion Private vars

        #region Ctors

        /// <summary>
        ///
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="sawsdlService"></param>
        /// <param name="ontologyService"></param>
        /// <param name="serviceDescriptionService"></param>
        /// <param name="graphService"></param>
        /// <param name="userIdentityService"></param>
        /// <param name="ontology_UserService"></param>
        /// <param name="serviceDescription_OntologyService"></param>
        /// <param name="serviceDescription_UserService"></param>
        public SawsdlApiController(IMapper mapper,
            ISawsdlService sawsdlService,
            IOntologyService ontologyService,
            IServiceDescriptionService serviceDescriptionService,
            IGraphService graphService,
            IUserIdentityService userIdentityService,
            IOntology_UserService ontology_UserService,
            IServiceDescription_OntologyService serviceDescription_OntologyService,
            IServiceDescription_UserService serviceDescription_UserService)
           : base(mapper, userIdentityService)
        {
            _sawsdlService = sawsdlService;
            _ontologyService = ontologyService;
            _serviceDescriptionService = serviceDescriptionService;
            _graphService = graphService;
            _ontology_UserService = ontology_UserService;
            _serviceDescription_OntologyService = serviceDescription_OntologyService;
            _serviceDescription_UserService = serviceDescription_UserService;
        }

        #endregion Ctors

        #region Actions

        #region Schema Mapping

        #region Get Schema Mapping

        #region Get Lifting Schema Mapping

        // GET: api/service-descriptions/1/complex-types/1/sawsdl/lifting-schema-mapping
        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <param name="idXsdComplexType"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("complex-types/{idXsdComplexType:int}/sawsdl/lifting-schema-mapping")]
        [ResponseType(typeof(SawsdlLiftingSchemaMapping_ApiRequestViewModel))]
        public IHttpActionResult GetLiftingSchemaMappingFromXsdComplexType(int id, int idXsdComplexType)
        {
            var requestDTO = new SawsdlSchemaMappingRequestViewDTO
            {
                IdServiceDescription = id,
                IdServiceDescriptionElement = idXsdComplexType
            };

            var liftingSchemaMappingUrl = _sawsdlService.GetLiftingSchemaMappingFromXsdComplexType(requestDTO);

            var liftingSchemaMappingResponseModel = new SawsdlLiftingSchemaMapping_ApiRequestViewModel();

            var xsdComplexType = _serviceDescriptionService.GetXsdComplexType(idXsdComplexType);

            liftingSchemaMappingResponseModel.LiftingSchemaMappingUrl = liftingSchemaMappingUrl;
            liftingSchemaMappingResponseModel.IdServiceDescription = id;
            liftingSchemaMappingResponseModel.IdWsdlElement = idXsdComplexType;
            liftingSchemaMappingResponseModel.IdWsdlElementType = (int)Domain.Enums.ServiceDescriptionElementTypeEnum.XsdComplexType;
            liftingSchemaMappingResponseModel.WsdlElementTypeName = Domain.Enums.ServiceDescriptionElementTypeEnum.XsdComplexType.GetEnumDescription();
            liftingSchemaMappingResponseModel.WsdlElementName = xsdComplexType.XsdComplexTypeName;

            return Ok(liftingSchemaMappingResponseModel);
        }

        // GET: api/service-descriptions/1/simple-types/1/sawsdl/lifting-schema-mapping
        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <param name="idXsdSimpleType"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("simple-types/{idXsdSimpleType:int}/sawsdl/lifting-schema-mapping")]
        [ResponseType(typeof(SawsdlLiftingSchemaMapping_ApiRequestViewModel))]
        public IHttpActionResult GetLiftingSchemaMappingFromXsdSimpleType(int id, int idXsdSimpleType)
        {
            var requestDTO = new SawsdlSchemaMappingRequestViewDTO
            {
                IdServiceDescription = id,
                IdServiceDescriptionElement = idXsdSimpleType
            };

            var liftingSchemaMappingUrl = _sawsdlService.GetLiftingSchemaMappingFromXsdSimpleType(requestDTO);

            var liftingSchemaMappingResponseModel = new SawsdlLiftingSchemaMapping_ApiRequestViewModel();

            var xsdSimpleType = _serviceDescriptionService.GetXsdSimpleType(idXsdSimpleType);

            liftingSchemaMappingResponseModel.LiftingSchemaMappingUrl = liftingSchemaMappingUrl;
            liftingSchemaMappingResponseModel.IdServiceDescription = id;
            liftingSchemaMappingResponseModel.IdWsdlElement = idXsdSimpleType;
            liftingSchemaMappingResponseModel.IdWsdlElementType = (int)Domain.Enums.ServiceDescriptionElementTypeEnum.XsdSimpleType;
            liftingSchemaMappingResponseModel.WsdlElementTypeName = Domain.Enums.ServiceDescriptionElementTypeEnum.XsdSimpleType.GetEnumDescription();
            liftingSchemaMappingResponseModel.WsdlElementName = xsdSimpleType.XsdSimpleTypeName;

            return Ok(liftingSchemaMappingResponseModel);
        }

        #endregion Get Lifting Schema Mapping

        #region Get Lowering Schema Mapping

        // GET: api/service-descriptions/1/complex-types/1/sawsdl/lowering-schema-mapping
        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <param name="idXsdComplexType"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("complex-types/{idXsdComplexType:int}/sawsdl/lowering-schema-mapping")]
        [ResponseType(typeof(SawsdlLoweringSchemaMapping_ApiRequestViewModel))]
        public IHttpActionResult GetLoweringSchemaMappingFromXsdComplexType(int id, int idXsdComplexType)
        {
            var requestDTO = new SawsdlSchemaMappingRequestViewDTO
            {
                IdServiceDescription = id,
                IdServiceDescriptionElement = idXsdComplexType
            };

            var loweringSchemaMappingUrl = _sawsdlService.GetLoweringSchemaMappingFromXsdComplexType(requestDTO);

            var loweringSchemaMappingResponseModel = new SawsdlLoweringSchemaMapping_ApiRequestViewModel();

            var xsdComplexType = _serviceDescriptionService.GetXsdComplexType(idXsdComplexType);

            loweringSchemaMappingResponseModel.LoweringSchemaMappingUrl = loweringSchemaMappingUrl;
            loweringSchemaMappingResponseModel.IdServiceDescription = id;
            loweringSchemaMappingResponseModel.IdWsdlElement = idXsdComplexType;
            loweringSchemaMappingResponseModel.IdWsdlElementType = (int)Domain.Enums.ServiceDescriptionElementTypeEnum.XsdComplexType;
            loweringSchemaMappingResponseModel.WsdlElementTypeName = Domain.Enums.ServiceDescriptionElementTypeEnum.XsdComplexType.GetEnumDescription();
            loweringSchemaMappingResponseModel.WsdlElementName = xsdComplexType.XsdComplexTypeName;

            return Ok(loweringSchemaMappingResponseModel);
        }

        // GET: api/service-descriptions/1/simple-types/1/sawsdl/lowering-schema-mapping
        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <param name="idXsdSimpleType"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("simple-types/{idXsdSimpleType:int}/sawsdl/lowering-schema-mapping")]
        [ResponseType(typeof(SawsdlLoweringSchemaMapping_ApiRequestViewModel))]
        public IHttpActionResult GetLoweringSchemaMappingFromXsdSimpleType(int id, int idXsdSimpleType)
        {
            var requestDTO = new SawsdlSchemaMappingRequestViewDTO
            {
                IdServiceDescription = id,
                IdServiceDescriptionElement = idXsdSimpleType
            };

            var loweringSchemaMappingUrl = _sawsdlService.GetLoweringSchemaMappingFromXsdSimpleType(requestDTO);

            var loweringSchemaMappingResponseModel = new SawsdlLoweringSchemaMapping_ApiRequestViewModel();

            var xsdSimpleType = _serviceDescriptionService.GetXsdSimpleType(idXsdSimpleType);

            loweringSchemaMappingResponseModel.LoweringSchemaMappingUrl = loweringSchemaMappingUrl;
            loweringSchemaMappingResponseModel.IdServiceDescription = id;
            loweringSchemaMappingResponseModel.IdWsdlElement = idXsdSimpleType;
            loweringSchemaMappingResponseModel.IdWsdlElementType = (int)Domain.Enums.ServiceDescriptionElementTypeEnum.XsdSimpleType;
            loweringSchemaMappingResponseModel.WsdlElementTypeName = Domain.Enums.ServiceDescriptionElementTypeEnum.XsdSimpleType.GetEnumDescription();
            loweringSchemaMappingResponseModel.WsdlElementName = xsdSimpleType.XsdSimpleTypeName;

            return Ok(loweringSchemaMappingResponseModel);
        }

        #endregion Get Lowering Schema Mapping

        #endregion Get Schema Mapping

        #region Add Schema Mapping

        #region Add Lifting Schema Mapping

        // POST: api/service-descriptions/5/complex-types/1/sawsdl/lifting-schema-mapping
        /// <summary>
        ///
        /// </summary>
        /// <param name="requestModel">todo: describe requestModel parameter on AddLiftingSchemaMapping</param>
        /// <param name="id">todo: describe id parameter on AddLiftingSchemaMapping</param>
        /// <param name="idXsdComplexType"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("complex-types/{idXsdComplexType:int}/sawsdl/lifting-schema-mapping")]
        [ResponseType(typeof(ServiceDescription_ApiResponseViewModel))]
        public IHttpActionResult AddLiftingSchemaMappingToXsdComplexType(int id, int idXsdComplexType, SawsdlLiftingSchemaMapping_ApiRequestCreateModel requestModel)
        {
            var idUser = _userIdentityService.Id;

            var requestDTO = new SawsdlLiftingSchemaMappingRequestCreateDTO
            {
                IdServiceDescription = id,
                IdServiceDescriptionElement = idXsdComplexType,
                LiftingSchemaMappingUrl = requestModel.LiftingSchemaMappingUrl,
                IdOwnerUser = idUser
            };

            try
            {
                _sawsdlService.AddLiftingSchemaMappingToXsdComplexType(requestDTO);

                var serviceDescription = _serviceDescriptionService.Get(id);

                var graphDataObject = JsonConvert.DeserializeObject<CytoscapeObject>(serviceDescription.GraphJson);

                var xsdComplexType = _serviceDescriptionService.GetXsdComplexTypeWithSemanticAnnotations(idXsdComplexType);

                graphDataObject = _graphService.AddLiftingSchemaMappingToXsdComplexType(graphDataObject, xsdComplexType) as CytoscapeObject;

                serviceDescription.GraphJson = JsonConvert.SerializeObject(graphDataObject);

                _serviceDescriptionService.Update(serviceDescription);

                var serviceDescriptionResponseModel = _mapper.Map<ServiceDescription_ApiResponseViewModel>(serviceDescription);

                return Ok(serviceDescriptionResponseModel);
            }
            catch (SawsdlLiftingSchemaMappingAlreadyAddedException alreadyAddedException)
            {
                return Content(System.Net.HttpStatusCode.Conflict, alreadyAddedException);
            }
        }

        // POST: api/service-descriptions/5/simple-types/1/sawsdl/lifting-schema-mapping
        /// <summary>
        ///
        /// </summary>
        /// <param name="requestModel">todo: describe requestModel parameter on AddLiftingSchemaMapping</param>
        /// <param name="id">todo: describe id parameter on AddLiftingSchemaMapping</param>
        /// <param name="idXsdSimpleType"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("simple-types/{idXsdSimpleType:int}/sawsdl/lifting-schema-mapping")]
        [ResponseType(typeof(ServiceDescription_ApiResponseViewModel))]
        public IHttpActionResult AddLiftingSchemaMappingToXsdSimpleType(int id, int idXsdSimpleType, SawsdlLiftingSchemaMapping_ApiRequestCreateModel requestModel)
        {
            var idUser = _userIdentityService.Id;

            var requestDTO = new SawsdlLiftingSchemaMappingRequestCreateDTO
            {
                IdServiceDescription = id,
                IdServiceDescriptionElement = idXsdSimpleType,
                LiftingSchemaMappingUrl = requestModel.LiftingSchemaMappingUrl,
                IdOwnerUser = idUser
            };

            try
            {
                _sawsdlService.AddLiftingSchemaMappingToXsdSimpleType(requestDTO);

                var serviceDescription = _serviceDescriptionService.Get(id);

                var graphDataObject = JsonConvert.DeserializeObject<CytoscapeObject>(serviceDescription.GraphJson);

                var xsdSimpleType = _serviceDescriptionService.GetXsdSimpleTypeWithSemanticAnnotations(idXsdSimpleType);

                graphDataObject = _graphService.AddLiftingSchemaMappingToXsdSimpleType(graphDataObject, xsdSimpleType) as CytoscapeObject;

                serviceDescription.GraphJson = JsonConvert.SerializeObject(graphDataObject);

                _serviceDescriptionService.Update(serviceDescription);

                var serviceDescriptionResponseModel = _mapper.Map<ServiceDescription_ApiResponseViewModel>(serviceDescription);

                return Ok(serviceDescriptionResponseModel);
            }
            catch (SawsdlLiftingSchemaMappingAlreadyAddedException alreadyAddedException)
            {
                return Content(System.Net.HttpStatusCode.Conflict, alreadyAddedException);
            }
        }

        #endregion Add Lifting Schema Mapping

        #region Add Lowering Schema Mapping

        // POST: api/service-descriptions/5/complex-types/1/sawsdl/lowering-schema-mapping
        /// <summary>
        ///
        /// </summary>
        /// <param name="requestModel">todo: describe requestModel parameter on AddLoweringSchemaMapping</param>
        /// <param name="id">todo: describe id parameter on AddLoweringSchemaMapping</param>
        /// <param name="idXsdComplexType"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("complex-types/{idXsdComplexType:int}/sawsdl/lowering-schema-mapping")]
        [ResponseType(typeof(ServiceDescription_ApiResponseViewModel))]
        public IHttpActionResult AddLoweringSchemaMappingToXsdComplexType(int id, int idXsdComplexType, SawsdlLoweringSchemaMapping_ApiRequestCreateModel requestModel)
        {
            var idUser = _userIdentityService.Id;

            var requestDTO = new SawsdlLoweringSchemaMappingRequestCreateDTO
            {
                IdServiceDescription = id,
                IdServiceDescriptionElement = idXsdComplexType,
                LoweringSchemaMappingUrl = requestModel.LoweringSchemaMappingUrl,
                IdOwnerUser = idUser
            };

            try
            {
                _sawsdlService.AddLoweringSchemaMappingToXsdComplexType(requestDTO);

                var serviceDescription = _serviceDescriptionService.Get(id);

                var graphDataObject = JsonConvert.DeserializeObject<CytoscapeObject>(serviceDescription.GraphJson);

                var xsdComplexType = _serviceDescriptionService.GetXsdComplexTypeWithSemanticAnnotations(idXsdComplexType);

                graphDataObject = _graphService.AddLoweringSchemaMappingToXsdComplexType(graphDataObject, xsdComplexType) as CytoscapeObject;

                serviceDescription.GraphJson = JsonConvert.SerializeObject(graphDataObject);

                _serviceDescriptionService.Update(serviceDescription);

                var serviceDescriptionResponseModel = _mapper.Map<ServiceDescription_ApiResponseViewModel>(serviceDescription);

                return Ok(serviceDescriptionResponseModel);
            }
            catch (SawsdlLoweringSchemaMappingAlreadyAddedException alreadyAddedException)
            {
                return Content(System.Net.HttpStatusCode.Conflict, alreadyAddedException);
            }
        }

        // POST: api/service-descriptions/5/simple-types/1/sawsdl/lowering-schema-mapping
        /// <summary>
        ///
        /// </summary>
        /// <param name="requestModel">todo: describe requestModel parameter on AddLoweringSchemaMapping</param>
        /// <param name="id">todo: describe id parameter on AddLoweringSchemaMapping</param>
        /// <param name="idXsdSimpleType"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("simple-types/{idXsdSimpleType:int}/sawsdl/lowering-schema-mapping")]
        [ResponseType(typeof(ServiceDescription_ApiResponseViewModel))]
        public IHttpActionResult AddLoweringSchemaMappingToXsdSimpleType(int id, int idXsdSimpleType, SawsdlLoweringSchemaMapping_ApiRequestCreateModel requestModel)
        {
            var idUser = _userIdentityService.Id;

            var requestDTO = new SawsdlLoweringSchemaMappingRequestCreateDTO
            {
                IdServiceDescription = id,
                IdServiceDescriptionElement = idXsdSimpleType,
                LoweringSchemaMappingUrl = requestModel.LoweringSchemaMappingUrl,
                IdOwnerUser = idUser
            };

            try
            {
                _sawsdlService.AddLoweringSchemaMappingToXsdSimpleType(requestDTO);

                var serviceDescription = _serviceDescriptionService.Get(id);

                var graphDataObject = JsonConvert.DeserializeObject<CytoscapeObject>(serviceDescription.GraphJson);

                var xsdSimpleType = _serviceDescriptionService.GetXsdSimpleTypeWithSemanticAnnotations(idXsdSimpleType);

                graphDataObject = _graphService.AddLoweringSchemaMappingToXsdSimpleType(graphDataObject, xsdSimpleType) as CytoscapeObject;

                serviceDescription.GraphJson = JsonConvert.SerializeObject(graphDataObject);

                _serviceDescriptionService.Update(serviceDescription);

                var serviceDescriptionResponseModel = _mapper.Map<ServiceDescription_ApiResponseViewModel>(serviceDescription);

                return Ok(serviceDescriptionResponseModel);
            }
            catch (SawsdlLoweringSchemaMappingAlreadyAddedException alreadyAddedException)
            {
                return Content(System.Net.HttpStatusCode.Conflict, alreadyAddedException);
            }
        }

        #endregion Add Lowering Schema Mapping

        #endregion Add Schema Mapping

        #region Remove Schema Mapping

        #region Remove Lifting Schema Mapping

        // DELETE: api/service-descriptions/5/complex-types/1/sawsdl/lifting-schema-mapping
        /// <summary>
        ///
        /// </summary>
        /// <param name="id">todo: describe id parameter on AddLiftingSchemaMapping</param>
        /// <param name="idXsdComplexType"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("complex-types/{idXsdComplexType:int}/sawsdl/lifting-schema-mapping")]
        public IHttpActionResult RemoveLiftingSchemaMappingFromXsdComplexType(int id, int idXsdComplexType)
        {
            var idUser = _userIdentityService.Id;

            var requestDTO = new SawsdlLiftingSchemaMappingRequestRemoveDTO
            {
                IdServiceDescription = id,
                IdServiceDescriptionElement = idXsdComplexType,
                IdOwnerUser = idUser
            };

            _sawsdlService.RemoveLiftingSchemaMappingFromXsdComplexType(requestDTO);

            var serviceDescription = _serviceDescriptionService.Get(id);

            var graphDataObject = JsonConvert.DeserializeObject<CytoscapeObject>(serviceDescription.GraphJson);

            var xsdComplexType = _serviceDescriptionService.GetXsdComplexTypeWithSemanticAnnotations(idXsdComplexType);

            graphDataObject = _graphService.RemoveLiftingSchemaMappingFromXsdComplexType(graphDataObject, xsdComplexType) as CytoscapeObject;

            serviceDescription.GraphJson = JsonConvert.SerializeObject(graphDataObject);

            _serviceDescriptionService.Update(serviceDescription);

            var serviceDescriptionResponseModel = _mapper.Map<ServiceDescription_ApiResponseViewModel>(serviceDescription);

            return Ok(serviceDescriptionResponseModel);
        }

        // DELETE: api/service-descriptions/5/simple-types/1/sawsdl/lifting-schema-mapping
        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <param name="idXsdSimpleType"></param>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("simple-types/{idXsdSimpleType:int}/sawsdl/lifting-schema-mapping")]
        public IHttpActionResult RemoveLiftingSchemaMappingFromXsdSimpleType(int id, int idXsdSimpleType, SawsdlLiftingSchemaMapping_ApiRequestRemoveModel requestModel)
        {
            var idUser = _userIdentityService.Id;

            var requestDTO = new SawsdlLiftingSchemaMappingRequestRemoveDTO
            {
                IdServiceDescription = id,
                IdServiceDescriptionElement = idXsdSimpleType,
                IdOwnerUser = idUser
            };

            _sawsdlService.RemoveLiftingSchemaMappingFromXsdSimpleType(requestDTO);

            var serviceDescription = _serviceDescriptionService.Get(id);

            var graphDataObject = JsonConvert.DeserializeObject<CytoscapeObject>(serviceDescription.GraphJson);

            var xsdSimpleType = _serviceDescriptionService.GetXsdSimpleTypeWithSemanticAnnotations(idXsdSimpleType);

            graphDataObject = _graphService.RemoveLiftingSchemaMappingFromXsdSimpleType(graphDataObject, xsdSimpleType) as CytoscapeObject;

            serviceDescription.GraphJson = JsonConvert.SerializeObject(graphDataObject);

            _serviceDescriptionService.Update(serviceDescription);

            var serviceDescriptionResponseModel = _mapper.Map<ServiceDescription_ApiResponseViewModel>(serviceDescription);

            return Ok(serviceDescriptionResponseModel);
        }

        #endregion Remove Lifting Schema Mapping

        #region Remove Lowering Schema Mapping

        // DELETE: api/service-descriptions/5/complex-types/1/sawsdl/lowering-schema-mapping
        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <param name="idXsdComplexType"></param>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("complex-types/{idXsdComplexType:int}/sawsdl/lowering-schema-mapping")]
        public IHttpActionResult RemoveLoweringSchemaMappingFromXsdComplexType(int id, int idXsdComplexType, SawsdlLoweringSchemaMapping_ApiRequestRemoveModel requestModel)
        {
            var idUser = _userIdentityService.Id;

            var requestDTO = new SawsdlLoweringSchemaMappingRequestRemoveDTO
            {
                IdServiceDescription = id,
                IdServiceDescriptionElement = idXsdComplexType,
                IdOwnerUser = idUser
            };

            _sawsdlService.RemoveLoweringSchemaMappingFromXsdComplexType(requestDTO);

            var serviceDescription = _serviceDescriptionService.Get(id);

            var graphDataObject = JsonConvert.DeserializeObject<CytoscapeObject>(serviceDescription.GraphJson);

            var xsdComplexType = _serviceDescriptionService.GetXsdComplexTypeWithSemanticAnnotations(idXsdComplexType);

            graphDataObject = _graphService.RemoveLoweringSchemaMappingFromXsdComplexType(graphDataObject, xsdComplexType) as CytoscapeObject;

            serviceDescription.GraphJson = JsonConvert.SerializeObject(graphDataObject);

            _serviceDescriptionService.Update(serviceDescription);

            var serviceDescriptionResponseModel = _mapper.Map<ServiceDescription_ApiResponseViewModel>(serviceDescription);

            return Ok(serviceDescriptionResponseModel);
        }

        // DELETE: api/service-descriptions/5/simple-types/1/sawsdl/lowering-schema-mapping
        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <param name="idXsdSimpleType"></param>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("simple-types/{idXsdSimpleType:int}/sawsdl/lowering-schema-mapping")]
        public IHttpActionResult RemoveLoweringSchemaMappingFromXsdSimpleType(int id, int idXsdSimpleType, SawsdlLoweringSchemaMapping_ApiRequestRemoveModel requestModel)
        {
            var idUser = _userIdentityService.Id;

            var requestDTO = new SawsdlLoweringSchemaMappingRequestRemoveDTO
            {
                IdServiceDescription = id,
                IdServiceDescriptionElement = idXsdSimpleType,
                IdOwnerUser = idUser
            };

            _sawsdlService.RemoveLoweringSchemaMappingFromXsdSimpleType(requestDTO);

            var serviceDescription = _serviceDescriptionService.Get(id);

            var graphDataObject = JsonConvert.DeserializeObject<CytoscapeObject>(serviceDescription.GraphJson);

            var xsdSimpleType = _serviceDescriptionService.GetXsdSimpleTypeWithSemanticAnnotations(idXsdSimpleType);

            graphDataObject = _graphService.RemoveLoweringSchemaMappingFromXsdSimpleType(graphDataObject, xsdSimpleType) as CytoscapeObject;

            serviceDescription.GraphJson = JsonConvert.SerializeObject(graphDataObject);

            _serviceDescriptionService.Update(serviceDescription);

            var serviceDescriptionResponseModel = _mapper.Map<ServiceDescription_ApiResponseViewModel>(serviceDescription);

            return Ok(serviceDescriptionResponseModel);
        }

        #endregion Remove Lowering Schema Mapping

        #endregion Remove Schema Mapping

        #region Update Schema Mapping

        // Put: api/service-descriptions/5/complex-types/1/sawsdl/lifting-schema-mapping
        /// <summary>
        ///
        /// </summary>
        /// <param name="id">todo: describe id parameter on AddLiftingSchemaMapping</param>
        /// <param name="idXsdComplexType"></param>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("complex-types/{idXsdComplexType:int}/sawsdl/lifting-schema-mapping")]
        public IHttpActionResult UpdateLiftingSchemaMappingInXsdComplexType(int id, int idXsdComplexType, SawsdlLiftingSchemaMapping_ApiRequestUpdateModel requestModel)
        {
            var idUser = _userIdentityService.Id;

            var requestDTO = new SawsdlLiftingSchemaMappingRequestUpdateDTO
            {
                IdServiceDescription = id,
                IdServiceDescriptionElement = idXsdComplexType,
                LiftingSchemaMappingUrl = requestModel.LiftingSchemaMappingUrl,
                IdOwnerUser = idUser
            };

            _sawsdlService.UpdateLiftingSchemaMappingInXsdComplexType(requestDTO);

            return Ok();
        }

        // Put: api/service-descriptions/5/simple-types/1/sawsdl/lifting-schema-mapping
        /// <summary>
        ///
        /// </summary>
        /// <param name="id">todo: describe id parameter on AddLiftingSchemaMapping</param>
        /// <param name="idXsdSimpleType"></param>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("simple-types/{idXsdSimpleType:int}/sawsdl/lifting-schema-mapping")]
        public IHttpActionResult UpdateLiftingSchemaMappingInXsdSimpleType(int id, int idXsdSimpleType, SawsdlLiftingSchemaMapping_ApiRequestUpdateModel requestModel)
        {
            var idUser = _userIdentityService.Id;

            var requestDTO = new SawsdlLiftingSchemaMappingRequestUpdateDTO
            {
                IdServiceDescription = id,
                IdServiceDescriptionElement = idXsdSimpleType,
                LiftingSchemaMappingUrl = requestModel.LiftingSchemaMappingUrl,
                IdOwnerUser = idUser
            };

            _sawsdlService.UpdateLiftingSchemaMappingInXsdSimpleType(requestDTO);

            return Ok();
        }

        // Put: api/service-descriptions/5/complex-types/1/sawsdl/lowering-schema-mapping
        /// <summary>
        ///
        /// </summary>
        /// <param name="id">todo: describe id parameter on AddLiftingSchemaMapping</param>
        /// <param name="idXsdComplexType"></param>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("complex-types/{idXsdComplexType:int}/sawsdl/lowering-schema-mapping")]
        public IHttpActionResult UpdateLoweringSchemaMappingInXsdComplexType(int id, int idXsdComplexType, SawsdlLoweringSchemaMapping_ApiRequestUpdateModel requestModel)
        {
            var idUser = _userIdentityService.Id;

            var requestDTO = new SawsdlLoweringSchemaMappingRequestUpdateDTO
            {
                IdServiceDescription = id,
                IdServiceDescriptionElement = idXsdComplexType,
                LoweringSchemaMappingUrl = requestModel.LoweringSchemaMappingUrl,
                IdOwnerUser = idUser
            };

            _sawsdlService.UpdateLoweringSchemaMappingInXsdComplexType(requestDTO);

            return Ok();
        }

        // Put: api/service-descriptions/5/simple-types/1/sawsdl/lowering-schema-mapping
        /// <summary>
        ///
        /// </summary>
        /// <param name="id">todo: describe id parameter on AddLiftingSchemaMapping</param>
        /// <param name="idXsdSimpleType"></param>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("simple-types/{idXsdSimpleType:int}/sawsdl/lowering-schema-mapping")]
        public IHttpActionResult UpdateLoweringSchemaMappingInXsdSimpleType(int id, int idXsdSimpleType, SawsdlLoweringSchemaMapping_ApiRequestUpdateModel requestModel)
        {
            var idUser = _userIdentityService.Id;

            var requestDTO = new SawsdlLoweringSchemaMappingRequestUpdateDTO
            {
                IdServiceDescription = id,
                IdServiceDescriptionElement = idXsdSimpleType,
                LoweringSchemaMappingUrl = requestModel.LoweringSchemaMappingUrl,
                IdOwnerUser = idUser
            };

            _sawsdlService.UpdateLoweringSchemaMappingInXsdSimpleType(requestDTO);

            return Ok();
        }

        #endregion Update Schema Mapping

        #endregion Schema Mapping

        #region Model Reference

        #region Get Model References

        // GET: api/service-descriptions/1/interfaces/1/sawsdl/model-reference
        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <param name="idWsdlInterface"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("interfaces/{idWsdlInterface:int}/sawsdl/model-reference")]
        [ResponseType(typeof(SawsdlModelReferenceCollection_ApiRequestViewModel))]
        public IHttpActionResult GetModelReferencesFromWsdlInterface(int id, int idWsdlInterface)
        {
            var requestDTO = new SawsdlModelReferenceRequestViewDTO
            {
                IdServiceDescription = id,
                IdServiceDescriptionElement = idWsdlInterface
            };

            var modelReferences = _sawsdlService.GetModelReferencesFromWsdlInterface(requestDTO);

            var modelReferencesResponseModel = _mapper.Map<SawsdlModelReferenceCollection_ApiRequestViewModel>(modelReferences);

            var wsdlInterface = _serviceDescriptionService.GetWsdlInterface(idWsdlInterface);

            modelReferencesResponseModel.IdServiceDescription = id;
            modelReferencesResponseModel.IdWsdlElement = idWsdlInterface;
            modelReferencesResponseModel.IdWsdlElementType = (int)Domain.Enums.ServiceDescriptionElementTypeEnum.WsdlInterface;
            modelReferencesResponseModel.WsdlElementTypeName = Domain.Enums.ServiceDescriptionElementTypeEnum.WsdlInterface.GetEnumDescription();
            modelReferencesResponseModel.WsdlElementName = wsdlInterface.WsdlInterfaceName;

            return Ok(modelReferencesResponseModel);
        }

        // GET: api/service-descriptions/1/operations/1/sawsdl/model-reference
        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <param name="idWsdlOperation"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("operations/{idWsdlOperation:int}/sawsdl/model-reference")]
        [ResponseType(typeof(SawsdlModelReferenceCollection_ApiRequestViewModel))]
        public IHttpActionResult GetModelReferencesFromWsdlOperation(int id, int idWsdlOperation)
        {
            var requestDTO = new SawsdlModelReferenceRequestViewDTO
            {
                IdServiceDescription = id,
                IdServiceDescriptionElement = idWsdlOperation
            };

            var modelReferences = _sawsdlService.GetModelReferencesFromWsdlOperation(requestDTO);

            var modelReferencesResponseModel = _mapper.Map<SawsdlModelReferenceCollection_ApiRequestViewModel>(modelReferences);

            var wsdlOperation = _serviceDescriptionService.GetWsdlOperation(idWsdlOperation);

            modelReferencesResponseModel.IdServiceDescription = id;
            modelReferencesResponseModel.IdWsdlElement = idWsdlOperation;
            modelReferencesResponseModel.IdWsdlElementType = (int)Domain.Enums.ServiceDescriptionElementTypeEnum.WsdlOperation;
            modelReferencesResponseModel.WsdlElementTypeName = Domain.Enums.ServiceDescriptionElementTypeEnum.WsdlOperation.GetEnumDescription();
            modelReferencesResponseModel.WsdlElementName = wsdlOperation.WsdlOperationName;

            return Ok(modelReferencesResponseModel);
        }

        // GET: api/service-descriptions/1/complex-types/1/sawsdl/model-reference
        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <param name="idXsdComplexType"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("complex-types/{idXsdComplexType:int}/sawsdl/model-reference")]
        [ResponseType(typeof(SawsdlModelReferenceCollection_ApiRequestViewModel))]
        public IHttpActionResult GetModelReferencesFromXsdComplexType(int id, int idXsdComplexType)
        {
            var requestDTO = new SawsdlModelReferenceRequestViewDTO
            {
                IdServiceDescription = id,
                IdServiceDescriptionElement = idXsdComplexType
            };

            var modelReferences = _sawsdlService.GetModelReferencesFromXsdComplexType(requestDTO);

            var modelReferencesResponseModel = _mapper.Map<SawsdlModelReferenceCollection_ApiRequestViewModel>(modelReferences);

            var xsdComplexType = _serviceDescriptionService.GetXsdComplexType(idXsdComplexType);

            modelReferencesResponseModel.IdServiceDescription = id;
            modelReferencesResponseModel.IdWsdlElement = idXsdComplexType;
            modelReferencesResponseModel.IdWsdlElementType = (int)Domain.Enums.ServiceDescriptionElementTypeEnum.XsdComplexType;
            modelReferencesResponseModel.WsdlElementTypeName = Domain.Enums.ServiceDescriptionElementTypeEnum.XsdComplexType.GetEnumDescription();
            modelReferencesResponseModel.WsdlElementName = xsdComplexType.XsdComplexTypeName;

            return Ok(modelReferencesResponseModel);
        }

        // GET: api/service-descriptions/1/simple-types/1/sawsdl/model-reference
        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <param name="idXsdSimpleType"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("simple-types/{idXsdSimpleType:int}/sawsdl/model-reference")]
        [ResponseType(typeof(SawsdlModelReferenceCollection_ApiRequestViewModel))]
        public IHttpActionResult GetModelReferencesFromxsdSimpleType(int id, int idXsdSimpleType)
        {
            var requestDTO = new SawsdlModelReferenceRequestViewDTO
            {
                IdServiceDescription = id,
                IdServiceDescriptionElement = idXsdSimpleType
            };

            var modelReferences = _sawsdlService.GetModelReferencesFromXsdSimpleType(requestDTO);

            var modelReferencesResponseModel = _mapper.Map<SawsdlModelReferenceCollection_ApiRequestViewModel>(modelReferences);

            var xsdSimpleType = _serviceDescriptionService.GetXsdSimpleType(idXsdSimpleType);

            modelReferencesResponseModel.IdServiceDescription = id;
            modelReferencesResponseModel.IdWsdlElement = idXsdSimpleType;
            modelReferencesResponseModel.IdWsdlElementType = (int)Domain.Enums.ServiceDescriptionElementTypeEnum.XsdSimpleType;
            modelReferencesResponseModel.WsdlElementTypeName = Domain.Enums.ServiceDescriptionElementTypeEnum.XsdSimpleType.GetEnumDescription();
            modelReferencesResponseModel.WsdlElementName = xsdSimpleType.XsdSimpleTypeName;

            return Ok(modelReferencesResponseModel);
        }

        #endregion Get Model References

        #region Add Model Reference

        // POST: api/service-descriptions/5/faults/1/sawsdl/model-reference
        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <param name="idWsdlFault"></param>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("faults/{idWsdlFault:int}/sawsdl/model-reference")]
        [ResponseType(typeof(ServiceDescription_ApiResponseViewModel))]
        public IHttpActionResult AddModelReferenceToWsdlFault(int id, int idWsdlFault, SawsdlModelReference_ApiRequestCreateModel requestModel)
        {
            var idUser = _userIdentityService.Id;

            var requestDTO = new SawsdlModelReferenceRequestCreateDTO
            {
                IdServiceDescription = id,
                IdServiceDescriptionElement = idWsdlFault,
                IdOntologyTerm = requestModel.IdOntologyTerm,
                IdOwnerUser = idUser
            };

            _sawsdlService.AddModelReferenceToWsdlFault(requestDTO);

            return Ok();
        }

        // POST: api/service-descriptions/5/interfaces/1/sawsdl/model-reference
        /// <summary>
        ///
        /// </summary>
        /// <param name="id">todo: describe id parameter on AddModelReference</param>
        /// <param name="idWsdlInterface">todo: describe idOutput parameter on AddModelReferenceToWsdlOutput</param>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("interfaces/{idWsdlInterface:int}/sawsdl/model-reference")]
        [ResponseType(typeof(ServiceDescription_ApiResponseViewModel))]
        public IHttpActionResult AddModelReferenceToWsdlInterface(int id, int idWsdlInterface, SawsdlModelReference_ApiRequestCreateModel requestModel)
        {
            var idUser = _userIdentityService.Id;

            var idOntologyTerm = requestModel.IdOntologyTerm;

            var requestDTO = new SawsdlModelReferenceRequestCreateDTO
            {
                IdServiceDescription = id,
                IdServiceDescriptionElement = idWsdlInterface,
                IdOntologyTerm = idOntologyTerm,
                IdOwnerUser = idUser
            };

            try
            {
                _sawsdlService.AddModelReferenceToWsdlInterface(requestDTO);

                var serviceDescription = _serviceDescriptionService.Get(id);

                var graphDataObject = JsonConvert.DeserializeObject<CytoscapeObject>(serviceDescription.GraphJson);

                var wsdlInterface = _serviceDescriptionService.GetWsdlInterfaceWithSemanticAnnotations(idWsdlInterface);

                graphDataObject = _graphService.AddModelReferencesNodesToWsdlInterface(graphDataObject, wsdlInterface, wsdlInterface.SawsdlModelReferences) as CytoscapeObject;

                var affectedNodePositionsQuantity = _serviceDescriptionService.CreateNodePositionsForSameModelReferencesBetweenWsdlElements(idServiceDescription: id, idOwnerUser: idUser);

                var transferredQuantity = _serviceDescriptionService.TransferNodePositionsFromOntologyTermsToModelReferences(idUser, id);

                graphDataObject = AddIntermediateOntologyTermsToGraph(graphDataObject);

                serviceDescription.GraphJson = JsonConvert.SerializeObject(graphDataObject);

                var idOntology = _ontologyService.GetOntologyTerm(idOntologyTerm).IdOntology;

                //TODO: This can be asynchronous with an event handler
                AddOntologyLinkToServiceDescription(idOntology, serviceDescription);

                //TODO: This can be asynchronous with an event handler
                AddOntologyToSharedUsers(idOntology, serviceDescription);

                _serviceDescriptionService.Update(serviceDescription);

                var affectedSharedUsersQuatity = _ontology_UserService.CreateBySharedServiceDescription(idServiceDescription: id, idOntologyTerm: idOntologyTerm);

                var serviceDescriptionResponseModel = _mapper.Map<ServiceDescription_ApiResponseViewModel>(serviceDescription);

                return Ok(serviceDescriptionResponseModel);
            }
            catch (WsdlOperationNotFoundException ex1)
            {
                return Content(System.Net.HttpStatusCode.NotFound, ex1);
            }
            catch (OntologyTermNotFoundException ex2)
            {
                return Content(System.Net.HttpStatusCode.NotFound, ex2);
            }
            catch (SawsdlModelReferenceAlreadyAddedException ex3)
            {
                return Content(System.Net.HttpStatusCode.BadRequest, ex3);
            }
        }

        // POST: api/service-descriptions/5/operations/1/sawsdl/model-reference
        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <param name="idWsdlOperation"></param>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("operations/{idWsdlOperation:int}/sawsdl/model-reference")]
        [ResponseType(typeof(ServiceDescription_ApiResponseViewModel))]
        public IHttpActionResult AddModelReferenceToWsdlOperation(int id, int idWsdlOperation, SawsdlModelReference_ApiRequestCreateModel requestModel)
        {
            var idUser = _userIdentityService.Id;

            var idOntologyTerm = requestModel.IdOntologyTerm;

            var requestDTO = new SawsdlModelReferenceRequestCreateDTO
            {
                IdServiceDescription = id,
                IdServiceDescriptionElement = idWsdlOperation,
                IdOntologyTerm = idOntologyTerm,
                IdOwnerUser = idUser
            };

            try
            {
                _sawsdlService.AddModelReferenceToWsdlOperation(requestDTO);

                var serviceDescription = _serviceDescriptionService.Get(id);

                var graphDataObject = JsonConvert.DeserializeObject<CytoscapeObject>(serviceDescription.GraphJson);

                var wsdlOperation = _serviceDescriptionService.GetWsdlOperationWithSemanticAnnotations(idWsdlOperation);

                graphDataObject = _graphService.AddModelReferencesNodesToWsdlOperation(graphDataObject, wsdlOperation, wsdlOperation.SawsdlModelReferences) as CytoscapeObject;

                var affectedNodePositionsQuantity = _serviceDescriptionService.CreateNodePositionsForSameModelReferencesBetweenWsdlElements(idServiceDescription: id, idOwnerUser: idUser);

                var transferredQuantity = _serviceDescriptionService.TransferNodePositionsFromOntologyTermsToModelReferences(idUser, id);

                graphDataObject = AddIntermediateOntologyTermsToGraph(graphDataObject);

                serviceDescription.GraphJson = JsonConvert.SerializeObject(graphDataObject);

                var idOntology = _ontologyService.GetOntologyTerm(idOntologyTerm).IdOntology;

                //TODO: This can be asynchronous with an event handler
                AddOntologyLinkToServiceDescription(idOntology, serviceDescription);

                //TODO: This can be asynchronous with an event handler
                AddOntologyToSharedUsers(idOntology, serviceDescription);

                _serviceDescriptionService.Update(serviceDescription);

                var serviceDescriptionResponseModel = _mapper.Map<ServiceDescription_ApiResponseViewModel>(serviceDescription);

                return Ok(serviceDescriptionResponseModel);
            }
            catch (WsdlOperationNotFoundException ex1)
            {
                return Content(System.Net.HttpStatusCode.NotFound, ex1);
            }
            catch (OntologyTermNotFoundException ex2)
            {
                return Content(System.Net.HttpStatusCode.NotFound, ex2);
            }
            catch (SawsdlModelReferenceAlreadyAddedException ex3)
            {
                return Content(System.Net.HttpStatusCode.BadRequest, ex3);
            }
        }

        // POST: api/service-descriptions/5/complex-types/1/sawsdl/model-reference
        /// <summary>
        ///
        /// </summary>
        /// <param name="id">todo: describe id parameter on AddModelReference</param>
        /// <param name="idXsdComplexType">todo: describe idOutput parameter on AddModelReferenceToWsdlOutput</param>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("complex-types/{idXsdComplexType:int}/sawsdl/model-reference")]
        [ResponseType(typeof(ServiceDescription_ApiResponseViewModel))]
        public IHttpActionResult AddModelReferenceToXsdComplexType(int id, int idXsdComplexType, SawsdlModelReference_ApiRequestCreateModel requestModel)
        {
            var idUser = _userIdentityService.Id;

            var idOntologyTerm = requestModel.IdOntologyTerm;

            var requestDTO = new SawsdlModelReferenceRequestCreateDTO
            {
                IdServiceDescription = id,
                IdServiceDescriptionElement = idXsdComplexType,
                IdOntologyTerm = idOntologyTerm,
                IdOwnerUser = idUser
            };

            try
            {
                _sawsdlService.AddModelReferenceToXsdComplexType(requestDTO);

                var serviceDescription = _serviceDescriptionService.Get(id);

                var graphDataObject = JsonConvert.DeserializeObject<CytoscapeObject>(serviceDescription.GraphJson);

                var xsdComplexType = _serviceDescriptionService.GetXsdComplexTypeWithSemanticAnnotations(idXsdComplexType);

                graphDataObject = _graphService.AddModelReferencesNodesToXsdComplexType(graphDataObject, xsdComplexType, xsdComplexType.SawsdlModelReferences) as CytoscapeObject;

                var affectedNodePositionsQuantity = _serviceDescriptionService.CreateNodePositionsForSameModelReferencesBetweenWsdlElements(idServiceDescription: id, idOwnerUser: idUser);

                var transferredQuantity = _serviceDescriptionService.TransferNodePositionsFromOntologyTermsToModelReferences(idUser, id);

                graphDataObject = AddIntermediateOntologyTermsToGraph(graphDataObject);

                serviceDescription.GraphJson = JsonConvert.SerializeObject(graphDataObject);

                var idOntology = _ontologyService.GetOntologyTerm(idOntologyTerm).IdOntology;

                //TODO: This can be asynchronous with an event handler
                AddOntologyLinkToServiceDescription(idOntology, serviceDescription);

                AddOntologyToSharedUsers(idOntology, serviceDescription);

                _serviceDescriptionService.Update(serviceDescription);

                var serviceDescriptionResponseModel = _mapper.Map<ServiceDescription_ApiResponseViewModel>(serviceDescription);

                return Ok(serviceDescriptionResponseModel);
            }
            catch (XsdComplexTypeNotFoundException ex1)
            {
                return Content(System.Net.HttpStatusCode.NotFound, ex1);
            }
            catch (OntologyTermNotFoundException ex2)
            {
                return Content(System.Net.HttpStatusCode.NotFound, ex2);
            }
            catch (SawsdlModelReferenceAlreadyAddedException ex3)
            {
                return Content(System.Net.HttpStatusCode.BadRequest, ex3);
            }
        }

        // POST: api/service-descriptions/5/simple-types/1/sawsdl/model-reference
        /// <summary>
        ///
        /// </summary>
        /// <param name="id">todo: describe id parameter on AddModelReference</param>
        /// <param name="idXsdSimpleType">todo: describe idOutput parameter on AddModelReferenceToWsdlOutput</param>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("simple-types/{idXsdSimpleType:int}/sawsdl/model-reference")]
        [ResponseType(typeof(ServiceDescription_ApiResponseViewModel))]
        public IHttpActionResult AddModelReferenceToXsdSimpleType(int id, int idXsdSimpleType, SawsdlModelReference_ApiRequestCreateModel requestModel)
        {
            var idUser = _userIdentityService.Id;

            var idOntologyTerm = requestModel.IdOntologyTerm;

            var requestDTO = new SawsdlModelReferenceRequestCreateDTO
            {
                IdServiceDescription = id,
                IdServiceDescriptionElement = idXsdSimpleType,
                IdOntologyTerm = idOntologyTerm,
                IdOwnerUser = idUser
            };

            try
            {
                _sawsdlService.AddModelReferenceToXsdSimpleType(requestDTO);

                var serviceDescription = _serviceDescriptionService.Get(id);

                var graphDataObject = JsonConvert.DeserializeObject<CytoscapeObject>(serviceDescription.GraphJson);

                var xsdSimpleType = _serviceDescriptionService.GetXsdSimpleTypeWithSemanticAnnotations(idXsdSimpleType);

                graphDataObject = _graphService.AddModelReferencesNodesToXsdSimpleType(graphDataObject, xsdSimpleType, xsdSimpleType.SawsdlModelReferences) as CytoscapeObject;

                var affectedNodePositionsQuantity = _serviceDescriptionService.CreateNodePositionsForSameModelReferencesBetweenWsdlElements(idServiceDescription: id, idOwnerUser: idUser);

                var transferredQuantity = _serviceDescriptionService.TransferNodePositionsFromOntologyTermsToModelReferences(idUser, id);

                graphDataObject = AddIntermediateOntologyTermsToGraph(graphDataObject);

                serviceDescription.GraphJson = JsonConvert.SerializeObject(graphDataObject);

                var idOntology = _ontologyService.GetOntologyTerm(idOntologyTerm).IdOntology;

                //TODO: This can be asynchronous with an event handler
                AddOntologyLinkToServiceDescription(idOntology, serviceDescription);

                //TODO: This can be asynchronous with an event handler
                AddOntologyToSharedUsers(idOntology, serviceDescription);

                _serviceDescriptionService.Update(serviceDescription);

                var serviceDescriptionResponseModel = _mapper.Map<ServiceDescription_ApiResponseViewModel>(serviceDescription);

                return Ok(serviceDescriptionResponseModel);
            }
            catch (XsdSimpleTypeNotFoundException ex1)
            {
                return Content(System.Net.HttpStatusCode.NotFound, ex1);
            }
            catch (OntologyTermNotFoundException ex2)
            {
                return Content(System.Net.HttpStatusCode.NotFound, ex2);
            }
            catch (SawsdlModelReferenceAlreadyAddedException ex3)
            {
                return Content(System.Net.HttpStatusCode.BadRequest, ex3);
            }
        }

        #endregion Add Model Reference

        #region Remove Model Reference

        // DELETE: api/service-descriptions/5/faults/1/sawsdl/model-reference
        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <param name="idWsdlFault"></param>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("faults/{idWsdlFault:int}/sawsdl/model-reference")]
        [ResponseType(typeof(string))]
        public IHttpActionResult RemoveModelReferenceFromWsdlFault(int id, int idWsdlFault, SawsdlModelReference_ApiRequestRemoveModel requestModel)
        {
            if (string.IsNullOrEmpty(requestModel.IdOntologyTerms))
            {
                return Content(System.Net.HttpStatusCode.BadRequest, new OntologyTermListEmptyToRemoveModelReferenceException());
            }

            var idOntologyTerms = requestModel.IdOntologyTerms.Split(',').Select(x => Convert.ToInt32(x));

            var idUser = _userIdentityService.Id;

            var requestDTO = new SawsdlModelReferenceRequestRemoveDTO
            {
                IdServiceDescription = id,
                IdServiceDescriptionElement = idWsdlFault,
                IdOntologyTerms = idOntologyTerms,
                IdOwnerUser = idUser
            };

            try
            {
                _sawsdlService.RemoveModelReferenceFromWsdlFault(requestDTO);
            }
            catch (WsdlOperationNotFoundException ex1)
            {
                return Content(System.Net.HttpStatusCode.NotFound, ex1);
            }
            catch (OntologyTermNotFoundException ex2)
            {
                return Content(System.Net.HttpStatusCode.NotFound, ex2);
            }
            catch (SawsdlModelReferenceAlreadyAddedException ex3)
            {
                return Content(System.Net.HttpStatusCode.BadRequest, ex3);
            }

            return Ok("SAWSDL Model Reference removed successfully from the Wsdl Fault");
        }

        // DELETE: api/service-descriptions/5/interfaces/1/sawsdl/model-reference
        /// <summary>
        ///
        /// </summary>
        /// <param name="id">todo: describe id parameter on AddModelReference</param>
        /// <param name="idWsdlInterface">todo: describe idOutput parameter on RemoveModelReferenceFromWsdlOutput</param>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("interfaces/{idWsdlInterface:int}/sawsdl/model-reference")]
        [ResponseType(typeof(ServiceDescription_ApiResponseViewModel))]
        public IHttpActionResult RemoveModelReferenceFromWsdlInterface(int id, int idWsdlInterface, SawsdlModelReference_ApiRequestRemoveModel requestModel)
        {
            if (string.IsNullOrEmpty(requestModel.IdOntologyTerms))
            {
                return Content(System.Net.HttpStatusCode.BadRequest, new OntologyTermListEmptyToRemoveModelReferenceException());
            }

            var idOntologyTerms = requestModel.IdOntologyTerms.Split(',').Select(x => Convert.ToInt32(x));

            var idUser = _userIdentityService.Id;

            var requestDTO = new SawsdlModelReferenceRequestRemoveDTO
            {
                IdServiceDescription = id,
                IdServiceDescriptionElement = idWsdlInterface,
                IdOntologyTerms = idOntologyTerms,
                IdOwnerUser = idUser
            };

            try
            {
                var transferredQuantity = _serviceDescriptionService.TransferNodePositionsFromModelReferencesToOntologyTerms(idOwnerUser: idUser, idServiceDescription: id, idOntologyTerms: idOntologyTerms);

                _sawsdlService.RemoveModelReferenceFromWsdlInterface(requestDTO);

                var serviceDescription = _serviceDescriptionService.Get(id);

                var graphDataObject = JsonConvert.DeserializeObject<CytoscapeObject>(serviceDescription.GraphJson);

                var wsdlInterface = _serviceDescriptionService.GetWsdlInterfaceWithSemanticAnnotations(idWsdlInterface);

                graphDataObject = RemoveIntermediateOntologyTermsFromGraph(graphDataObject);

                graphDataObject = RemoveModelReferencesNodesFromWsdlInterface(idOntologyTerms, graphDataObject, wsdlInterface);

                graphDataObject = AddIntermediateOntologyTermsToGraph(graphDataObject);

                RemoveUnusedGraphNodePositions(graphDataObject);

                serviceDescription.GraphJson = JsonConvert.SerializeObject(graphDataObject);

                //TODO: This can be asynchronous with an event handler
                RemoveOntologyLinkFromServiceDescription(serviceDescription);

                _serviceDescriptionService.Update(serviceDescription);

                var serviceDescriptionResponseModel = _mapper.Map<ServiceDescription_ApiResponseViewModel>(serviceDescription);

                return Ok(serviceDescriptionResponseModel);
            }
            catch (WsdlInterfaceNotFoundException ex1)
            {
                return Content(System.Net.HttpStatusCode.NotFound, ex1);
            }
            catch (OntologyTermNotFoundException ex2)
            {
                return Content(System.Net.HttpStatusCode.NotFound, ex2);
            }
            catch (SawsdlModelReferenceAlreadyAddedException ex3)
            {
                return Content(System.Net.HttpStatusCode.BadRequest, ex3);
            }
        }

        // DELETE: api/service-descriptions/5/operations/1/sawsdl/model-reference
        /// <summary>
        ///
        /// </summary>
        /// <param name="id">todo: describe id parameter on AddModelReference</param>
        /// <param name="idWsdlOperation">todo: describe idInput parameter on RemoveModelReferenceFromWsdlInput</param>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("operations/{idWsdlOperation:int}/sawsdl/model-reference")]
        [ResponseType(typeof(string))]
        public IHttpActionResult RemoveModelReferenceFromWsdlOperation(int id, int idWsdlOperation, SawsdlModelReference_ApiRequestRemoveModel requestModel)
        {
            if (string.IsNullOrEmpty(requestModel.IdOntologyTerms))
            {
                return Content(System.Net.HttpStatusCode.BadRequest, new OntologyTermListEmptyToRemoveModelReferenceException());
            }

            var idOntologyTerms = requestModel.IdOntologyTerms.Split(',').Select(x => Convert.ToInt32(x));

            var idUser = _userIdentityService.Id;

            var requestDTO = new SawsdlModelReferenceRequestRemoveDTO
            {
                IdServiceDescription = id,
                IdServiceDescriptionElement = idWsdlOperation,
                IdOntologyTerms = idOntologyTerms,
                IdOwnerUser = idUser
            };

            try
            {
                var transferredQuantity = _serviceDescriptionService.TransferNodePositionsFromModelReferencesToOntologyTerms(idOwnerUser: idUser, idServiceDescription: id, idOntologyTerms: idOntologyTerms);

                _sawsdlService.RemoveModelReferenceFromWsdlOperation(requestDTO);

                var serviceDescription = _serviceDescriptionService.Get(id);

                var graphDataObject = JsonConvert.DeserializeObject<CytoscapeObject>(serviceDescription.GraphJson);

                var wsdlOperation = _serviceDescriptionService.GetWsdlOperationWithSemanticAnnotations(idWsdlOperation);

                graphDataObject = RemoveIntermediateOntologyTermsFromGraph(graphDataObject);

                graphDataObject = RemoveModelReferencesNodesFromWsdlOperation(idOntologyTerms, graphDataObject, wsdlOperation);

                graphDataObject = AddIntermediateOntologyTermsToGraph(graphDataObject);

                RemoveUnusedGraphNodePositions(graphDataObject);

                serviceDescription.GraphJson = JsonConvert.SerializeObject(graphDataObject);

                //TODO: This can be asynchronous with an event handler
                RemoveOntologyLinkFromServiceDescription(serviceDescription);

                _serviceDescriptionService.Update(serviceDescription);

                var serviceDescriptionResponseModel = _mapper.Map<ServiceDescription_ApiResponseViewModel>(serviceDescription);

                return Ok(serviceDescriptionResponseModel);
            }
            catch (WsdlOperationNotFoundException ex1)
            {
                return Content(System.Net.HttpStatusCode.NotFound, ex1);
            }
            catch (OntologyTermNotFoundException ex2)
            {
                return Content(System.Net.HttpStatusCode.NotFound, ex2);
            }
            catch (SawsdlModelReferenceAlreadyAddedException ex3)
            {
                return Content(System.Net.HttpStatusCode.BadRequest, ex3);
            }
        }

        // DELETE: api/service-descriptions/5/complex-types/1/sawsdl/model-reference
        /// <summary>
        ///
        /// </summary>
        /// <param name="id">todo: describe id parameter on AddModelReference</param>
        /// <param name="idXsdComplexType"></param>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("complex-types/{idXsdComplexType:int}/sawsdl/model-reference")]
        [ResponseType(typeof(string))]
        public IHttpActionResult RemoveModelReferenceFromXsdComplexType(int id, int idXsdComplexType, SawsdlModelReference_ApiRequestRemoveModel requestModel)
        {
            if (string.IsNullOrEmpty(requestModel.IdOntologyTerms))
            {
                return Content(System.Net.HttpStatusCode.BadRequest, new OntologyTermListEmptyToRemoveModelReferenceException());
            }

            var idOntologyTerms = requestModel.IdOntologyTerms.Split(',').Select(x => Convert.ToInt32(x));

            var idUser = _userIdentityService.Id;

            var requestDTO = new SawsdlModelReferenceRequestRemoveDTO
            {
                IdServiceDescription = id,
                IdServiceDescriptionElement = idXsdComplexType,
                IdOntologyTerms = idOntologyTerms,
                IdOwnerUser = idUser
            };

            try
            {
                var transferredQuantity = _serviceDescriptionService.TransferNodePositionsFromModelReferencesToOntologyTerms(idOwnerUser: idUser, idServiceDescription: id, idOntologyTerms: idOntologyTerms);

                _sawsdlService.RemoveModelReferenceFromXsdComplexType(requestDTO);

                var serviceDescription = _serviceDescriptionService.Get(id);

                var graphDataObject = JsonConvert.DeserializeObject<CytoscapeObject>(serviceDescription.GraphJson);

                var xsdComplexType = _serviceDescriptionService.GetXsdComplexTypeWithSemanticAnnotations(idXsdComplexType);

                graphDataObject = RemoveIntermediateOntologyTermsFromGraph(graphDataObject);

                graphDataObject = RemoveModelReferencesNodesFromXsdComplexType(idOntologyTerms, graphDataObject, xsdComplexType);

                graphDataObject = AddIntermediateOntologyTermsToGraph(graphDataObject);

                RemoveUnusedGraphNodePositions(graphDataObject);

                serviceDescription.GraphJson = JsonConvert.SerializeObject(graphDataObject);

                //TODO: This can be asynchronous with an event handler
                RemoveOntologyLinkFromServiceDescription(serviceDescription);

                _serviceDescriptionService.Update(serviceDescription);

                var serviceDescriptionResponseModel = _mapper.Map<ServiceDescription_ApiResponseViewModel>(serviceDescription);

                return Ok(serviceDescriptionResponseModel);
            }
            catch (XsdComplexTypeNotFoundException ex1)
            {
                return Content(System.Net.HttpStatusCode.NotFound, ex1);
            }
            catch (OntologyTermNotFoundException ex2)
            {
                return Content(System.Net.HttpStatusCode.NotFound, ex2);
            }
            catch (SawsdlModelReferenceAlreadyAddedException ex3)
            {
                return Content(System.Net.HttpStatusCode.BadRequest, ex3);
            }
        }

        // DELETE: api/service-descriptions/5/simple-types/1/sawsdl/model-reference
        /// <summary>
        ///
        /// </summary>
        /// <param name="id">todo: describe id parameter on AddModelReference</param>
        /// <param name="idXsdSimpleType">todo: describe idOutput parameter on RemoveModelReferenceFromWsdlOutput</param>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("simple-types/{idXsdSimpleType:int}/sawsdl/model-reference")]
        [ResponseType(typeof(string))]
        public IHttpActionResult RemoveModelReferenceFromxsdSimpleType(int id, int idXsdSimpleType, SawsdlModelReference_ApiRequestRemoveModel requestModel)
        {
            if (string.IsNullOrEmpty(requestModel.IdOntologyTerms))
            {
                return Content(System.Net.HttpStatusCode.BadRequest, new OntologyTermListEmptyToRemoveModelReferenceException());
            }

            var idOntologyTerms = requestModel.IdOntologyTerms.Split(',').Select(x => Convert.ToInt32(x));

            var idUser = _userIdentityService.Id;

            var requestDTO = new SawsdlModelReferenceRequestRemoveDTO
            {
                IdServiceDescription = id,
                IdServiceDescriptionElement = idXsdSimpleType,
                IdOntologyTerms = idOntologyTerms,
                IdOwnerUser = idUser
            };

            try
            {
                var transferredQuantity = _serviceDescriptionService.TransferNodePositionsFromModelReferencesToOntologyTerms(idOwnerUser: idUser, idServiceDescription: id, idOntologyTerms: idOntologyTerms);

                _sawsdlService.RemoveModelReferenceFromXsdSimpleType(requestDTO);

                var serviceDescription = _serviceDescriptionService.Get(id);

                var graphDataObject = JsonConvert.DeserializeObject<CytoscapeObject>(serviceDescription.GraphJson);

                var xsdSimpleType = _serviceDescriptionService.GetXsdSimpleTypeWithSemanticAnnotations(idXsdSimpleType);

                graphDataObject = RemoveIntermediateOntologyTermsFromGraph(graphDataObject);

                graphDataObject = RemoveModelReferencesNodesFromxsdSimpleType(idOntologyTerms, graphDataObject, xsdSimpleType);

                graphDataObject = AddIntermediateOntologyTermsToGraph(graphDataObject);

                RemoveUnusedGraphNodePositions(graphDataObject);

                serviceDescription.GraphJson = JsonConvert.SerializeObject(graphDataObject);

                //TODO: This can be asynchronous with an event handler
                RemoveOntologyLinkFromServiceDescription(serviceDescription);

                _serviceDescriptionService.Update(serviceDescription);

                var serviceDescriptionResponseModel = _mapper.Map<ServiceDescription_ApiResponseViewModel>(serviceDescription);

                return Ok(serviceDescriptionResponseModel);
            }
            catch (XsdSimpleTypeNotFoundException ex1)
            {
                return Content(System.Net.HttpStatusCode.NotFound, ex1);
            }
            catch (OntologyTermNotFoundException ex2)
            {
                return Content(System.Net.HttpStatusCode.NotFound, ex2);
            }
            catch (SawsdlModelReferenceAlreadyAddedException ex3)
            {
                return Content(System.Net.HttpStatusCode.BadRequest, ex3);
            }
        }

        #endregion Remove Model Reference

        #endregion Model Reference

        #endregion Actions

        #region Private methods

        /// <summary>
        ///
        /// </summary>
        /// <param name="graphDataObject"></param>
        /// <returns></returns>
        private CytoscapeObject AddIntermediateOntologyTermsToGraph(CytoscapeObject graphDataObject)
        {
            var modelReferenceNodes = graphDataObject.Nodes.Where(x => x.Data.NodeTypeEnum == Domain.Enums.GraphNodeTypeEnum.ModelReference);

            if (modelReferenceNodes.Count() > 1)
            {
                var ontologyTerms = new List<OntologyTerm>();

                foreach (var modelReferenceNodeSource in modelReferenceNodes)
                {
                    var ontologyTerm = _ontologyService.FindTerm(modelReferenceNodeSource.Data.IdOntology, modelReferenceNodeSource.Data.IdOntologyTerm);

                    ontologyTerms.Add(ontologyTerm);
                }

                foreach (var ontologyTerm1 in ontologyTerms)
                {
                    foreach (var ontologyTerm2 in ontologyTerms)
                    {
                        if (ontologyTerm1.Id != ontologyTerm2.Id)
                        {
                            var path = _ontologyService.FindPathBetweenTerms(ontologyTerm1, ontologyTerm2);

                            if (path != null)
                            {
                                var pathArray = path.ToArray();

                                for (int i = 0; i < pathArray.Length; i++)
                                {
                                    var currentOntologyTermFromPath = pathArray[i];

                                    var currentOntologyTermAlreadyInGraph = graphDataObject.Nodes.FirstOrDefault(x => x.Data.IdOntologyTerm == currentOntologyTermFromPath.Id);

                                    if (currentOntologyTermAlreadyInGraph == null)
                                    {
                                        var previousOntologyTermFromPath = pathArray[i - 1];

                                        _graphService.AddIntermediateNodesForOntologyTerms(graphDataObject, previousOntologyTermFromPath, currentOntologyTermFromPath);

                                        var nextOntologyTermFromPath = pathArray[i + 1];

                                        var nextOntologyTermAlreadyInGraph = graphDataObject.Nodes.FirstOrDefault(x => x.Data.IdOntologyTerm == nextOntologyTermFromPath.Id);

                                        if (nextOntologyTermAlreadyInGraph != null)
                                        {
                                            _graphService.AddEdgeToIntermediateNodesForOntologyTerms(graphDataObject, nextOntologyTermFromPath, currentOntologyTermFromPath);
                                        }
                                    }
                                    else if (i > 0)
                                    {
                                        var previousOntologyTermFromPath = pathArray[i - 1];

                                        var isEdgeAlreadyBetweenOntologyTerms = _graphService.IsEdgeAlreadyBetweenOntologyTerms(graphDataObject, currentOntologyTermFromPath, previousOntologyTermFromPath);

                                        if (!isEdgeAlreadyBetweenOntologyTerms)
                                        {
                                            _graphService.AddEdgeToIntermediateNodesForOntologyTerms(graphDataObject, currentOntologyTermFromPath, previousOntologyTermFromPath);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return graphDataObject;
        }

        /// <summary>
        /// Add the ontology linked to the service description
        /// </summary>
        /// <param name="id"></param>
        /// <param name="idOntology"></param>
        /// <param name="serviceDescription"></param>
        private void AddOntologyLinkToServiceDescription(int idOntology, ServiceDescription serviceDescription)
        {
            var serviceDescription_Ontologies = _serviceDescription_OntologyService.GetByServiceDescriptionId(serviceDescription.Id);

            var ontologyAlreadyLinkedToServiceDescription = serviceDescription_Ontologies.Any(x => x.IdOntology == idOntology);

            if (!ontologyAlreadyLinkedToServiceDescription)
            {
                serviceDescription.ServiceDescription_Ontologies = new List<ServiceDescription_Ontology>
                {
                    new ServiceDescription_Ontology { IdOntology = idOntology, IdServiceDescription = serviceDescription.Id }
                };
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="idOntology"></param>
        /// <param name="serviceDescription"></param>
        private void AddOntologyToSharedUsers(int idOntology, ServiceDescription serviceDescription)
        {
            var sharedUsers = _serviceDescription_UserService.GetAllByServiceDescription(serviceDescription.Id);

            var idSharedUsers = sharedUsers.Select(x => x.IdSharedUser);

            //TODO : Verify why the ASYNC was not working because of the Postgres connection and the EF
            _ontologyService.AddOntologyToSharedUsersSync(idOntology, serviceDescription.Id);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="graphDataObject"></param>
        /// <returns></returns>
        private CytoscapeObject RemoveIntermediateOntologyTermsFromGraph(CytoscapeObject graphDataObject)
        {
            return _graphService.RemoveIntermediateOntologyTermsFromGraph(graphDataObject) as CytoscapeObject;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="idOntologyTerms"></param>
        /// <param name="graphDataObject"></param>
        /// <param name="wsdlInterface"></param>
        /// <returns></returns>
        private CytoscapeObject RemoveModelReferencesNodesFromWsdlInterface(IEnumerable<int> idOntologyTerms, CytoscapeObject graphDataObject, WsdlInterface wsdlInterface)
        {
            return _graphService.RemoveModelReferencesNodesFromWsdlInterface(graphDataObject, wsdlInterface, idOntologyTerms) as CytoscapeObject;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="idOntologyTerms"></param>
        /// <param name="graphDataObject"></param>
        /// <param name="wsdlOperation"></param>
        /// <returns></returns>
        private CytoscapeObject RemoveModelReferencesNodesFromWsdlOperation(IEnumerable<int> idOntologyTerms, CytoscapeObject graphDataObject, WsdlOperation wsdlOperation)
        {
            return _graphService.RemoveModelReferencesNodesFromWsdlOperation(graphDataObject, wsdlOperation, idOntologyTerms) as CytoscapeObject;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="idOntologyTerms"></param>
        /// <param name="graphDataObject"></param>
        /// <param name="xsdComplexType"></param>
        /// <returns></returns>
        private CytoscapeObject RemoveModelReferencesNodesFromXsdComplexType(IEnumerable<int> idOntologyTerms, CytoscapeObject graphDataObject, XsdComplexType xsdComplexType)
        {
            return _graphService.RemoveModelReferencesNodesFromXsdComplexType(graphDataObject, xsdComplexType, idOntologyTerms) as CytoscapeObject;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="idOntologyTerms"></param>
        /// <param name="graphDataObject"></param>
        /// <param name="xsdSimpleType"></param>
        /// <returns></returns>
        private CytoscapeObject RemoveModelReferencesNodesFromxsdSimpleType(IEnumerable<int> idOntologyTerms, CytoscapeObject graphDataObject, XsdSimpleType xsdSimpleType)
        {
            return _graphService.RemoveModelReferencesNodesFromXsdSimpleType(graphDataObject, xsdSimpleType, idOntologyTerms) as CytoscapeObject;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="serviceDescription"></param>
        private void RemoveOntologyLinkFromServiceDescription(ServiceDescription serviceDescription)
        {
            var serviceDescription_Ontologies = _serviceDescription_OntologyService.GetByServiceDescriptionId(serviceDescription.Id, false);

            var modelReferencesFromTheServiceDescription = _sawsdlService.GetModelReferencesFromServiceDescription(serviceDescription.Id);

            var idOntologyTermsFromModelReferencesInTheServiceDescription = modelReferencesFromTheServiceDescription.Select(x => x.IdOntologyTerm);

            var idOntologiesFromModelReferencesInTheServiceDescription = _ontologyService.GetAllOntologiesFromOntologyTerms(idOntologyTermsFromModelReferencesInTheServiceDescription).Select(x => x.Id);

            //Get all idOntologies from the serviceDescription_Ontologies list which are not in the idOntologiesFromModelReferencesInTheServiceDescription list
            var serviceDescription_OntologiesToBeRemoved = serviceDescription_Ontologies.Where(x => !idOntologiesFromModelReferencesInTheServiceDescription.Contains(x.IdOntology));

            //Remove all the serviceDescription_Ontologies that are not being used as a Model Reference in the Service Description
            var count = _serviceDescription_OntologyService.RemoveRange(serviceDescription_OntologiesToBeRemoved);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="graphDataObject"></param>
        private void RemoveUnusedGraphNodePositions(CytoscapeObject graphDataObject)
        {
            //_serviceDescriptionService.RemoveUnusedGraphNodePositisonsByUser(idOwnerUser, graphDataObject);
            _serviceDescriptionService.RemoveUnusedGraphNodePositisons(graphDataObject);
        }

        #endregion Private methods
    }
}