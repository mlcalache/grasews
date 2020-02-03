using Grasews.Domain.Entities;
using Grasews.Domain.Exceptions;
using Grasews.Domain.Interfaces.DTOs;
using Grasews.Domain.Interfaces.Repositories;
using Grasews.Domain.Interfaces.Services;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Grasews.Application.Services
{
    public class SawsdlService : BaseService, ISawsdlService
    {
        #region Private consts

        private const string LIFTING_SCHEMA_MAPPING_ATTRIBUTE = "liftingSchemaMapping";
        private const string LOWERING_SCHEMA_MAPPING_ATTRIBUTE = "loweringSchemaMapping";
        private const string MODEL_REFERENCE_ATTRIBUTE = "modelReference";
        private const string SAWSDL_NAMESPACE = "http://www.w3.org/ns/sawsdl";
        private const string WSDL_NAMESPACE = "http://www.w3.org/ns/wsdl";
        private const string XSD_NAMESPACE = "http://www.w3.org/2001/XMLSchema";

        #endregion Private consts

        #region Private vars

        private readonly IOntologyTermEntityRepository _ontologyTermRepository;
        private readonly ISawsdlModelReferenceEntityRepository _sawsdlModelReferenceRepository;
        private readonly IServiceDescriptionEntityRepository _serviceDescriptionRepository;

        private readonly XNamespace sawsdlNamespace = XNamespace.Get(SAWSDL_NAMESPACE);
        private readonly XNamespace wsdlNamespace = XNamespace.Get(WSDL_NAMESPACE);
        private readonly XNamespace xsdNamespace = XNamespace.Get(XSD_NAMESPACE);

        #endregion Private vars

        #region Ctors

        public SawsdlService(IServiceDescriptionEntityRepository serviceDescriptionRepository,
            IOntologyTermEntityRepository ontologyTermRepository,
            ISawsdlModelReferenceEntityRepository sawsdlModelReferenceRepository)
        {
            _serviceDescriptionRepository = serviceDescriptionRepository;
            _ontologyTermRepository = ontologyTermRepository;
            _sawsdlModelReferenceRepository = sawsdlModelReferenceRepository;
        }

        #endregion Ctors

        #region Public methods

        #region Schema Mapping

        #region Get Schema Mapping

        #region Get Lifting Schema Mapping

        /// <summary>
        /// Get the Lifting Schema Mapping from a  Xsd Complex Element in the WSDL document
        /// </summary>
        /// <param name="requestDTO"></param>
        /// <returns></returns>
        public string GetLiftingSchemaMappingFromXsdComplexType(ISawsdlSchemaMappingRequestViewDTO requestDTO)
        {
            var xsdComplexElement = _serviceDescriptionRepository.GetXsdComplexType(idXsdComplexType: requestDTO.IdServiceDescriptionElement, @readonly: false);

            if (xsdComplexElement == null)
            {
                throw new XsdComplexTypeNotFoundException();
            }

            if (!string.IsNullOrEmpty(xsdComplexElement.LiftingSchemaMapping))
            {
                return xsdComplexElement.LiftingSchemaMapping;
            }

            throw new SawsdlLiftingSchemaMappingDoesNotExistException();
        }

        /// <summary>
        /// Get the Lifting Schema Mapping from a Xsd Simple Element in the WSDL document
        /// </summary>
        /// <param name="requestDTO"></param>
        /// <returns></returns>
        public string GetLiftingSchemaMappingFromXsdSimpleType(ISawsdlSchemaMappingRequestViewDTO requestDTO)
        {
            var xsdSimpleType = _serviceDescriptionRepository.GetXsdSimpleType(idXsdSimpleType: requestDTO.IdServiceDescriptionElement, @readonly: false);

            if (xsdSimpleType == null)
            {
                throw new XsdSimpleTypeNotFoundException();
            }

            if (!string.IsNullOrEmpty(xsdSimpleType.LiftingSchemaMapping))
            {
                return xsdSimpleType.LiftingSchemaMapping;
            }

            throw new SawsdlLiftingSchemaMappingDoesNotExistException();
        }

        #endregion Get Lifting Schema Mapping

        #region Get Lowering Schema Mapping

        /// <summary>
        /// Get the Lowering Schema Mapping from a Xsd Complex Element in the WSDL document
        /// </summary>
        /// <param name="requestDTO"></param>
        /// <returns></returns>
        public string GetLoweringSchemaMappingFromXsdComplexType(ISawsdlSchemaMappingRequestViewDTO requestDTO)
        {
            var xsdComplexElement = _serviceDescriptionRepository.GetXsdComplexType(idXsdComplexType: requestDTO.IdServiceDescriptionElement, @readonly: false);

            if (xsdComplexElement == null)
            {
                throw new XsdComplexTypeNotFoundException();
            }

            if (!string.IsNullOrEmpty(xsdComplexElement.LoweringSchemaMapping))
            {
                return xsdComplexElement.LoweringSchemaMapping;
            }

            throw new SawsdlLoweringSchemaMappingDoesNotExistException();
        }

        /// <summary>
        /// Get the Lowering Schema Mapping from a Xsd Simple Element in the WSDL document
        /// </summary>
        /// <param name="requestDTO"></param>
        /// <returns></returns>
        public string GetLoweringSchemaMappingFromXsdSimpleType(ISawsdlSchemaMappingRequestViewDTO requestDTO)
        {
            var xsdSimpleType = _serviceDescriptionRepository.GetXsdSimpleType(idXsdSimpleType: requestDTO.IdServiceDescriptionElement, @readonly: false);

            if (xsdSimpleType == null)
            {
                throw new XsdSimpleTypeNotFoundException();
            }

            if (!string.IsNullOrEmpty(xsdSimpleType.LoweringSchemaMapping))
            {
                return xsdSimpleType.LoweringSchemaMapping;
            }

            throw new SawsdlLoweringSchemaMappingDoesNotExistException();
        }

        #endregion Get Lowering Schema Mapping

        #endregion Get Schema Mapping

        #region Add Schema Mapping

        #region Add Lifting Schema Mapping

        /// <summary>
        /// Add a SAWSDL Lifting Schema Mapping to a Xsd Complex Element in the WSDL document
        /// </summary>
        /// <param name="requestDTO"></param>
        /// <returns></returns>
        public int AddLiftingSchemaMappingToXsdComplexType(ISawsdlLiftingSchemaMappingRequestCreateDTO requestDTO)
        {
            var xsdComplexElement = _serviceDescriptionRepository.GetXsdComplexType(idXsdComplexType: requestDTO.IdServiceDescriptionElement, @readonly: false);

            if (xsdComplexElement == null)
            {
                throw new XsdComplexTypeNotFoundException();
            }

            if (string.IsNullOrEmpty(xsdComplexElement.LiftingSchemaMapping))
            {
                xsdComplexElement.LiftingSchemaMapping = requestDTO.LiftingSchemaMappingUrl;

                var serviceDescription = xsdComplexElement.XsdDocument.ServiceDescription;

                AddLiftingSchemaMappingXmlAttributeToXsdComplexElement(xsdComplexElement, serviceDescription, requestDTO.LiftingSchemaMappingUrl);

                _serviceDescriptionRepository.Update(serviceDescription);

                return _serviceDescriptionRepository.SaveChanges();
            }

            throw new SawsdlLiftingSchemaMappingAlreadyAddedException();
        }

        /// <summary>
        /// Add a SAWSDL Lifting Schema Mapping to a Xsd Simple Element in the WSDL document
        /// </summary>
        /// <param name="requestDTO"></param>
        /// <returns></returns>
        public int AddLiftingSchemaMappingToXsdSimpleType(ISawsdlLiftingSchemaMappingRequestCreateDTO requestDTO)
        {
            var xsdSimpleElement = _serviceDescriptionRepository.GetXsdSimpleType(idXsdSimpleType: requestDTO.IdServiceDescriptionElement, @readonly: false);

            if (xsdSimpleElement == null)
            {
                throw new XsdSimpleTypeNotFoundException();
            }

            if (string.IsNullOrEmpty(xsdSimpleElement.LiftingSchemaMapping))
            {
                xsdSimpleElement.LiftingSchemaMapping = requestDTO.LiftingSchemaMappingUrl;

                var serviceDescription = xsdSimpleElement.XsdDocument.ServiceDescription;

                AddLiftingSchemaMappingXmlAttributeToXsdSimpleType(xsdSimpleElement, serviceDescription, requestDTO.LiftingSchemaMappingUrl);

                return _serviceDescriptionRepository.SaveChanges();
            }

            throw new SawsdlLiftingSchemaMappingAlreadyAddedException();
        }

        #endregion Add Lifting Schema Mapping

        #region Add Lowering Schema Mapping

        /// <summary>
        /// Add a SAWSDL Lowering Schema Mapping to a Xsd Complex Element in the WSDL document
        /// </summary>
        /// <param name="requestDTO"></param>
        /// <returns></returns>
        public int AddLoweringSchemaMappingToXsdComplexType(ISawsdlLoweringSchemaMappingRequestCreateDTO requestDTO)
        {
            var xsdComplexElement = _serviceDescriptionRepository.GetXsdComplexType(idXsdComplexType: requestDTO.IdServiceDescriptionElement, @readonly: false);

            if (xsdComplexElement == null)
            {
                throw new XsdComplexTypeNotFoundException();
            }

            if (string.IsNullOrEmpty(xsdComplexElement.LoweringSchemaMapping))
            {
                xsdComplexElement.LoweringSchemaMapping = requestDTO.LoweringSchemaMappingUrl;

                var serviceDescription = xsdComplexElement.XsdDocument.ServiceDescription;

                AddLoweringSchemaMappingXmlAttributeToXsdComplexType(xsdComplexElement, serviceDescription, requestDTO.LoweringSchemaMappingUrl);

                _serviceDescriptionRepository.Update(serviceDescription);

                return _serviceDescriptionRepository.SaveChanges();
            }

            throw new SawsdlLoweringSchemaMappingAlreadyAddedException();
        }

        /// <summary>
        /// Add a SAWSDL Lowering Schema Mapping to a Xsd Simple Element in the WSDL document
        /// </summary>
        /// <param name="requestDTO"></param>
        /// <returns></returns>
        public int AddLoweringSchemaMappingToXsdSimpleType(ISawsdlLoweringSchemaMappingRequestCreateDTO requestDTO)
        {
            var xsdSimpleElement = _serviceDescriptionRepository.GetXsdSimpleType(idXsdSimpleType: requestDTO.IdServiceDescriptionElement, @readonly: false);

            if (xsdSimpleElement == null)
            {
                throw new XsdSimpleTypeNotFoundException();
            }

            if (string.IsNullOrEmpty(xsdSimpleElement.LoweringSchemaMapping))
            {
                xsdSimpleElement.LoweringSchemaMapping = requestDTO.LoweringSchemaMappingUrl;

                var serviceDescription = xsdSimpleElement.XsdDocument.ServiceDescription;

                AddLoweringSchemaMappingXmlAttributeToXsdSimpleType(xsdSimpleElement, serviceDescription, requestDTO.LoweringSchemaMappingUrl);

                return _serviceDescriptionRepository.SaveChanges();
            }

            throw new SawsdlLoweringSchemaMappingAlreadyAddedException();
        }

        #endregion Add Lowering Schema Mapping

        #endregion Add Schema Mapping

        #region Remove Schema Mapping

        #region Remove Lifting Schema Mapping

        /// <summary>
        /// Remove a SAWSDL Lifting Schema Mapping from a Xsd Complex Element in the WSDL document
        /// </summary>
        /// <param name="requestDTO"></param>
        /// <returns></returns>
        public int RemoveLiftingSchemaMappingFromXsdComplexType(ISawsdlLiftingSchemaMappingRequestRemoveDTO requestDTO)
        {
            var xsdComplexElement = _serviceDescriptionRepository.GetXsdComplexType(idXsdComplexType: requestDTO.IdServiceDescriptionElement, @readonly: false);

            if (xsdComplexElement == null)
            {
                throw new XsdComplexTypeNotFoundException();
            }

            xsdComplexElement.LiftingSchemaMapping = null;

            var serviceDescription = xsdComplexElement.XsdDocument.ServiceDescription;

            RemoveLiftingSchemaMappingXmlAttributeFromXsdComplexType(xsdComplexElement, serviceDescription);

            _serviceDescriptionRepository.Update(serviceDescription);

            return _serviceDescriptionRepository.SaveChanges();
        }

        /// <summary>
        /// Remove a SAWSDL Lifting Schema Mapping from a Xsd Simple Element in the WSDL document
        /// </summary>
        /// <param name="requestDTO"></param>
        /// <returns></returns>
        public int RemoveLiftingSchemaMappingFromXsdSimpleType(ISawsdlLiftingSchemaMappingRequestRemoveDTO requestDTO)
        {
            var xsdSimpleElement = _serviceDescriptionRepository.GetXsdSimpleType(idXsdSimpleType: requestDTO.IdServiceDescriptionElement, @readonly: false);

            xsdSimpleElement.LiftingSchemaMapping = null;

            var serviceDescription = xsdSimpleElement.XsdDocument.ServiceDescription;

            RemoveLiftingSchemaMappingXmlAttributeFromXsdSimpleType(xsdSimpleElement, serviceDescription);

            _serviceDescriptionRepository.Update(serviceDescription);

            return _serviceDescriptionRepository.SaveChanges();
        }

        #endregion Remove Lifting Schema Mapping

        #region Remove Lowering Schema Mapping

        /// <summary>
        /// Remove a SAWSDL Lowering Schema Mapping from a Xsd Complex Element in the WSDL document
        /// </summary>
        /// <param name="requestDTO"></param>
        /// <returns></returns>
        public int RemoveLoweringSchemaMappingFromXsdComplexType(ISawsdlLoweringSchemaMappingRequestRemoveDTO requestDTO)
        {
            var xsdComplexElement = _serviceDescriptionRepository.GetXsdComplexType(idXsdComplexType: requestDTO.IdServiceDescriptionElement, @readonly: false);

            if (xsdComplexElement == null)
            {
                throw new XsdComplexTypeNotFoundException();
            }

            xsdComplexElement.LoweringSchemaMapping = null;

            var serviceDescription = xsdComplexElement.XsdDocument.ServiceDescription;

            RemoveLoweringSchemaMappingXmlAttributeFromXsdComplexType(xsdComplexElement, serviceDescription);

            _serviceDescriptionRepository.Update(serviceDescription);

            return _serviceDescriptionRepository.SaveChanges();
        }

        /// <summary>
        /// Remove a SAWSDL Lowering Schema Mapping from a Xsd Simple Element in the WSDL document
        /// </summary>
        /// <param name="requestDTO"></param>
        /// <returns></returns>
        public int RemoveLoweringSchemaMappingFromXsdSimpleType(ISawsdlLoweringSchemaMappingRequestRemoveDTO requestDTO)
        {
            var xsdSimpleElement = _serviceDescriptionRepository.GetXsdSimpleType(idXsdSimpleType: requestDTO.IdServiceDescriptionElement, @readonly: false);

            xsdSimpleElement.LoweringSchemaMapping = null;

            var serviceDescription = xsdSimpleElement.XsdDocument.ServiceDescription;

            RemoveLoweringSchemaMappingXmlAttributeFromXsdSimpleType(xsdSimpleElement, serviceDescription);

            _serviceDescriptionRepository.Update(serviceDescription);

            return _serviceDescriptionRepository.SaveChanges();
        }

        #endregion Remove Lowering Schema Mapping

        #endregion Remove Schema Mapping

        #region Update Schema Mapping

        #region Update Lifting Schema Mapping

        /// <summary>
        /// Update a SAWSDL Lifting Schema Mapping from a Xsd Complex Element in the WSDL document
        /// </summary>
        /// <param name="requestDTO"></param>
        /// <returns></returns>
        public int UpdateLiftingSchemaMappingInXsdComplexType(ISawsdlLiftingSchemaMappingRequestUpdateDTO requestDTO)
        {
            var xsdComplexElement = _serviceDescriptionRepository.GetXsdComplexType(idXsdComplexType: requestDTO.IdServiceDescriptionElement, @readonly: false);

            if (string.IsNullOrEmpty(xsdComplexElement.LiftingSchemaMapping))
            {
                xsdComplexElement.LiftingSchemaMapping = requestDTO.LiftingSchemaMappingUrl;

                return _serviceDescriptionRepository.SaveChanges();
            }

            throw new SawsdlLiftingSchemaMappingDoesNotExistException();
        }

        /// <summary>
        /// Update a SAWSDL Lifting Schema Mapping from a Xsd Simple Element in the WSDL document
        /// </summary>
        /// <param name="requestDTO"></param>
        /// <returns></returns>
        public int UpdateLiftingSchemaMappingInXsdSimpleType(ISawsdlLiftingSchemaMappingRequestUpdateDTO requestDTO)
        {
            var xsdSimpleElement = _serviceDescriptionRepository.GetXsdSimpleType(idXsdSimpleType: requestDTO.IdServiceDescriptionElement, @readonly: false);

            if (string.IsNullOrEmpty(xsdSimpleElement.LiftingSchemaMapping))
            {
                xsdSimpleElement.LiftingSchemaMapping = requestDTO.LiftingSchemaMappingUrl;

                return _serviceDescriptionRepository.SaveChanges();
            }

            throw new SawsdlLiftingSchemaMappingDoesNotExistException();
        }

        #endregion Update Lifting Schema Mapping

        #region Update Lowering Schema Mapping

        /// <summary>
        /// Update a SAWSDL Lowering Schema Mapping from a Xsd Complex Element in the WSDL document
        /// </summary>
        /// <param name="requestDTO"></param>
        /// <returns></returns>
        public int UpdateLoweringSchemaMappingInXsdComplexType(ISawsdlLoweringSchemaMappingRequestUpdateDTO requestDTO)
        {
            var xsdComplexElement = _serviceDescriptionRepository.GetXsdComplexType(idXsdComplexType: requestDTO.IdServiceDescriptionElement, @readonly: false);

            if (string.IsNullOrEmpty(xsdComplexElement.LoweringSchemaMapping))
            {
                xsdComplexElement.LoweringSchemaMapping = requestDTO.LoweringSchemaMappingUrl;

                return _serviceDescriptionRepository.SaveChanges();
            }

            throw new SawsdlLiftingSchemaMappingDoesNotExistException();
        }

        /// <summary>
        /// Update a SAWSDL Lowering Schema Mapping from a Xsd Simple Element in the WSDL document
        /// </summary>
        /// <param name="requestDTO"></param>
        /// <returns></returns>
        public int UpdateLoweringSchemaMappingInXsdSimpleType(ISawsdlLoweringSchemaMappingRequestUpdateDTO requestDTO)
        {
            var xsdSimpleElement = _serviceDescriptionRepository.GetXsdSimpleType(idXsdSimpleType: requestDTO.IdServiceDescriptionElement, @readonly: false);

            if (string.IsNullOrEmpty(xsdSimpleElement.LoweringSchemaMapping))
            {
                xsdSimpleElement.LoweringSchemaMapping = requestDTO.LoweringSchemaMappingUrl;

                return _serviceDescriptionRepository.SaveChanges();
            }

            throw new SawsdlLiftingSchemaMappingDoesNotExistException();
        }

        #endregion Update Lowering Schema Mapping

        #endregion Update Schema Mapping

        #endregion Schema Mapping

        #region Model Reference

        #region Get Model References

        public IEnumerable<SawsdlModelReference> GetModelReferencesFromServiceDescription(int id)
        {
            var temp1 = _sawsdlModelReferenceRepository.GetAll().ToList();

            return _sawsdlModelReferenceRepository.GetAll().Where(x => x.IdServiceDescription == id).ToList();
        }

        public IEnumerable<SawsdlModelReference> GetModelReferencesFromWsdlFault(ISawsdlModelReferenceRequestViewDTO requestDTO)
        {
            var wsdlFault = _serviceDescriptionRepository.GetWsdlFault(requestDTO.IdServiceDescriptionElement, false);

            if (wsdlFault == null)
            {
                throw new WsdlFaultNotFoundException();
            }

            var sawsdlModelReferences = _sawsdlModelReferenceRepository.GetAllWithWsdlFault().Where(x => x.WsdlFault.Id == wsdlFault.Id);

            return sawsdlModelReferences.ToList();
        }

        public IEnumerable<SawsdlModelReference> GetModelReferencesFromWsdlInterface(ISawsdlModelReferenceRequestViewDTO requestDTO)
        {
            var wsdlInterface = _serviceDescriptionRepository.GetWsdlInterface(requestDTO.IdServiceDescriptionElement, false);

            if (wsdlInterface == null)
            {
                throw new WsdlInterfaceNotFoundException();
            }

            var sawsdlModelReferences = _sawsdlModelReferenceRepository.GetAllWithWsdlInterface().Where(x => x.WsdlInterface.Id == wsdlInterface.Id);

            return sawsdlModelReferences.ToList();
        }

        public IEnumerable<SawsdlModelReference> GetModelReferencesFromWsdlOperation(ISawsdlModelReferenceRequestViewDTO requestDTO)
        {
            var wsdlOperation = _serviceDescriptionRepository.GetWsdlOperation(requestDTO.IdServiceDescriptionElement, false);

            if (wsdlOperation == null)
            {
                throw new WsdlOperationNotFoundException();
            }

            var sawsdlModelReferences = _sawsdlModelReferenceRepository.GetAllWithWsdlOperation().Where(x => x.WsdlOperation.Id == wsdlOperation.Id);

            return sawsdlModelReferences.ToList();
        }

        public IEnumerable<SawsdlModelReference> GetModelReferencesFromXsdComplexType(ISawsdlModelReferenceRequestViewDTO requestDTO)
        {
            var xsdComplexElement = _serviceDescriptionRepository.GetXsdComplexType(requestDTO.IdServiceDescriptionElement, false);

            if (xsdComplexElement == null)
            {
                throw new XsdComplexTypeNotFoundException();
            }

            var sawsdlModelReferences = _sawsdlModelReferenceRepository.GetAllWithXsdComplexType().Where(x => x.XsdComplexType.Id == xsdComplexElement.Id);

            return sawsdlModelReferences.ToList();
        }

        public IEnumerable<SawsdlModelReference> GetModelReferencesFromXsdSimpleType(ISawsdlModelReferenceRequestViewDTO requestDTO)
        {
            var xsdSimpleType = _serviceDescriptionRepository.GetXsdSimpleType(requestDTO.IdServiceDescriptionElement, false);

            if (xsdSimpleType == null)
            {
                throw new XsdSimpleTypeNotFoundException();
            }

            var sawsdlModelReferences = _sawsdlModelReferenceRepository.GetAllWithXsdSimpleType().Where(x => x.XsdSimpleType.Id == xsdSimpleType.Id);

            return sawsdlModelReferences.ToList();
        }

        #endregion Get Model References

        #region Add Model Reference

        /// <summary>
        ///
        /// </summary>
        /// <param name="requestDTO"></param>
        /// <returns></returns>
        public int AddModelReferenceToWsdlFault(ISawsdlModelReferenceRequestCreateDTO requestDTO)
        {
            var wsdlFault = _serviceDescriptionRepository.GetWsdlFault(requestDTO.IdServiceDescriptionElement, false);

            if (wsdlFault == null)
            {
                throw new WsdlFaultNotFoundException();
            }

            var ontologyTerm = _ontologyTermRepository.Get(requestDTO.IdOntologyTerm, false);

            if (ontologyTerm == null)
            {
                throw new OntologyTermNotFoundException();
            }

            var modelReferenceAlreadyAdded = _sawsdlModelReferenceRepository.GetAllWithWsdlFault().Any(x => x.WsdlFault.Id == wsdlFault.Id && x.IdOntologyTerm == ontologyTerm.Id);

            if (modelReferenceAlreadyAdded)
            {
                throw new SawsdlModelReferenceAlreadyAddedException();
            }

            if (wsdlFault.SawsdlModelReferences == null)
            {
                wsdlFault.SawsdlModelReferences = new List<SawsdlModelReference>();
            }

            var serviceDescription = wsdlFault.WsdlInterface.ServiceDescription;

            wsdlFault.SawsdlModelReferences.Add(new SawsdlModelReference
            {
                ModelReference = ontologyTerm.TermUri,
                IdOntologyTerm = ontologyTerm.Id,
                IdOwnerUser = requestDTO.IdOwnerUser,
                IdServiceDescription = serviceDescription.Id
            });

            _serviceDescriptionRepository.Update(serviceDescription);

            return _serviceDescriptionRepository.SaveChanges();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="requestDTO"></param>
        /// <returns></returns>
        public int AddModelReferenceToWsdlInterface(ISawsdlModelReferenceRequestCreateDTO requestDTO)
        {
            var wsdlInterface = _serviceDescriptionRepository.GetWsdlInterface(requestDTO.IdServiceDescriptionElement, false);

            if (wsdlInterface == null)
            {
                throw new WsdlInterfaceNotFoundException();
            }

            var ontologyTerm = _ontologyTermRepository.Get(requestDTO.IdOntologyTerm, false);

            if (ontologyTerm == null)
            {
                throw new OntologyTermNotFoundException();
            }

            var modelReferenceAlreadyAdded = _sawsdlModelReferenceRepository.GetAllWithWsdlInterface().Any(x => x.WsdlInterface.Id == wsdlInterface.Id && x.IdOntologyTerm == ontologyTerm.Id);

            if (modelReferenceAlreadyAdded)
            {
                throw new SawsdlModelReferenceAlreadyAddedException();
            }

            if (wsdlInterface.SawsdlModelReferences == null)
            {
                wsdlInterface.SawsdlModelReferences = new List<SawsdlModelReference>();
            }

            var serviceDescription = wsdlInterface.ServiceDescription;

            wsdlInterface.SawsdlModelReferences.Add(new SawsdlModelReference
            {
                ModelReference = ontologyTerm.TermUri,
                IdOntologyTerm = ontologyTerm.Id,
                IdOwnerUser = requestDTO.IdOwnerUser,
                IdServiceDescription = serviceDescription.Id
            });

            AddModelReferenceXmlAttributeToWsdlInterface(wsdlInterface, ontologyTerm, serviceDescription);

            _serviceDescriptionRepository.Update(serviceDescription);

            return _serviceDescriptionRepository.SaveChanges();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="requestDTO"></param>
        /// <returns></returns>
        public int AddModelReferenceToWsdlOperation(ISawsdlModelReferenceRequestCreateDTO requestDTO)
        {
            var wsdlOperation = _serviceDescriptionRepository.GetWsdlOperation(requestDTO.IdServiceDescriptionElement, false);

            if (wsdlOperation == null)
            {
                throw new WsdlOperationNotFoundException();
            }

            var ontologyTerm = _ontologyTermRepository.Get(requestDTO.IdOntologyTerm, false);

            if (ontologyTerm == null)
            {
                throw new OntologyTermNotFoundException();
            }

            var modelReferenceAlreadyAdded = _sawsdlModelReferenceRepository.GetAllWithWsdlOperation().Any(x => x.WsdlOperation.Id == wsdlOperation.Id && x.IdOntologyTerm == ontologyTerm.Id);

            if (modelReferenceAlreadyAdded)
            {
                throw new SawsdlModelReferenceAlreadyAddedException();
            }

            if (wsdlOperation.SawsdlModelReferences == null)
            {
                wsdlOperation.SawsdlModelReferences = new List<SawsdlModelReference>();
            }

            var serviceDescription = wsdlOperation.WsdlInterface.ServiceDescription;

            wsdlOperation.SawsdlModelReferences.Add(new SawsdlModelReference
            {
                ModelReference = ontologyTerm.TermUri,
                IdOntologyTerm = ontologyTerm.Id,
                IdOwnerUser = requestDTO.IdOwnerUser,
                IdServiceDescription = serviceDescription.Id
            });

            AddModelReferenceXmlAttributeToWsdlOperation(wsdlOperation, ontologyTerm, serviceDescription);

            _serviceDescriptionRepository.Update(serviceDescription);

            return _serviceDescriptionRepository.SaveChanges();
        }

        ///// <summary>
        /////
        ///// </summary>
        ///// <param name="requestDTO"></param>
        ///// <returns></returns>
        //public int AddModelReferenceToWsdlOutfault(ISawsdlModelReferenceRequestCreateDTO requestDTO)
        //{
        //    var wsdlOutfault = _serviceDescriptionRepository.GetWsdlOutfault(requestDTO.IdServiceDescriptionElement, false);

        //    if (wsdlOutfault == null)
        //    {
        //        throw new WsdlOutfaultNotFoundException();
        //    }

        //    var ontologyTerm = _ontologyTermRepository.Get(requestDTO.IdOntologyTerm, false);

        //    if (ontologyTerm == null)
        //    {
        //        throw new OntologyTermNotFoundException();
        //    }

        //    var modelReferenceAlreadyAdded = _sawsdlModelReferenceRepository.GetAllWithXsdComplexType().Any(x => x.WsdlOutfault.Id == wsdlOutfault.Id && x.IdOntologyTerm == ontologyTerm.Id);

        //    if (modelReferenceAlreadyAdded)
        //    {
        //        throw new SawsdlModelReferenceAlreadyAddedException();
        //    }

        //    if (wsdlOutfault.SawsdlModelReferences == null)
        //    {
        //        wsdlOutfault.SawsdlModelReferences = new List<SawsdlModelReference>();
        //    }

        //    var serviceDescription = wsdlOutfault.WsdlOperation.WsdlInterface.ServiceDescription;

        //    wsdlOutfault.SawsdlModelReferences.Add(new SawsdlModelReference
        //    {
        //        ModelReference = ontologyTerm.TermUri,
        //        IdOntologyTerm = ontologyTerm.Id,
        //        IdOwnerUser = requestDTO.IdOwnerUser,
        //        IdServiceDescription = serviceDescription.Id
        //    });

        //    _serviceDescriptionRepository.Update(serviceDescription);

        //    return _serviceDescriptionRepository.SaveChanges();
        //}

        /// <summary>
        ///
        /// </summary>
        /// <param name="requestDTO"></param>
        /// <returns></returns>
        public int AddModelReferenceToXsdComplexType(ISawsdlModelReferenceRequestCreateDTO requestDTO)
        {
            var xsdComplexElement = _serviceDescriptionRepository.GetXsdComplexType(requestDTO.IdServiceDescriptionElement, false);

            if (xsdComplexElement == null)
            {
                throw new XsdComplexTypeNotFoundException();
            }

            var ontologyTerm = _ontologyTermRepository.Get(requestDTO.IdOntologyTerm, false);

            if (ontologyTerm == null)
            {
                throw new OntologyTermNotFoundException();
            }

            var modelReferenceAlreadyAdded = _sawsdlModelReferenceRepository.GetAllWithXsdComplexType().Any(x => x.XsdComplexType.Id == xsdComplexElement.Id && x.IdOntologyTerm == ontologyTerm.Id);

            if (modelReferenceAlreadyAdded)
            {
                throw new SawsdlModelReferenceAlreadyAddedException();
            }

            if (xsdComplexElement.SawsdlModelReferences == null)
            {
                xsdComplexElement.SawsdlModelReferences = new List<SawsdlModelReference>();
            }

            var serviceDescription = xsdComplexElement.XsdDocument.ServiceDescription;

            xsdComplexElement.SawsdlModelReferences.Add(new SawsdlModelReference
            {
                ModelReference = ontologyTerm.TermUri,
                IdOntologyTerm = ontologyTerm.Id,
                IdOwnerUser = requestDTO.IdOwnerUser,
                IdServiceDescription = serviceDescription.Id
            });

            AddModelReferenceXmlAttributeToXsdComplexElement(xsdComplexElement, ontologyTerm, serviceDescription);

            _serviceDescriptionRepository.Update(serviceDescription);

            return _serviceDescriptionRepository.SaveChanges();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="requestDTO"></param>
        /// <returns></returns>
        public int AddModelReferenceToXsdSimpleType(ISawsdlModelReferenceRequestCreateDTO requestDTO)
        {
            var xsdSimpleType = _serviceDescriptionRepository.GetXsdSimpleType(requestDTO.IdServiceDescriptionElement, false);

            if (xsdSimpleType == null)
            {
                throw new XsdSimpleTypeNotFoundException();
            }

            var ontologyTerm = _ontologyTermRepository.Get(requestDTO.IdOntologyTerm, false);

            if (ontologyTerm == null)
            {
                throw new OntologyTermNotFoundException();
            }

            var modelReferenceAlreadyAdded = _sawsdlModelReferenceRepository.GetAllWithXsdSimpleType().Any(x => x.XsdSimpleType.Id == xsdSimpleType.Id && x.IdOntologyTerm == ontologyTerm.Id);

            if (modelReferenceAlreadyAdded)
            {
                throw new SawsdlModelReferenceAlreadyAddedException();
            }

            if (xsdSimpleType.SawsdlModelReferences == null)
            {
                xsdSimpleType.SawsdlModelReferences = new List<SawsdlModelReference>();
            }

            var serviceDescription = xsdSimpleType.XsdDocument.ServiceDescription;

            xsdSimpleType.SawsdlModelReferences.Add(new SawsdlModelReference
            {
                ModelReference = ontologyTerm.TermUri,
                IdOntologyTerm = ontologyTerm.Id,
                IdOwnerUser = requestDTO.IdOwnerUser,
                IdServiceDescription = serviceDescription.Id
            });

            AddModelReferenceXmlAttributeToXsdSimpleType(xsdSimpleType, ontologyTerm, serviceDescription);

            _serviceDescriptionRepository.Update(serviceDescription);

            return _serviceDescriptionRepository.SaveChanges();
        }

        #endregion Add Model Reference

        #region Remove Model Reference

        /// <summary>
        ///
        /// </summary>
        /// <param name="requestDTO"></param>
        /// <returns></returns>
        public int RemoveModelReferenceFromWsdlFault(ISawsdlModelReferenceRequestRemoveDTO requestDTO)
        {
            var wsdlFault = _serviceDescriptionRepository.GetWsdlFault(requestDTO.IdServiceDescriptionElement, false);

            if (wsdlFault == null)
            {
                throw new WsdlFaultNotFoundException();
            }

            foreach (var idOntologyTerm in requestDTO.IdOntologyTerms)
            {
                var ontologyTerm = _ontologyTermRepository.Get(idOntologyTerm, false);

                if (ontologyTerm == null)
                {
                    throw new OntologyTermNotFoundException();
                }

                var modelReferenceExists = _sawsdlModelReferenceRepository.GetAllWithWsdlFault().Any(x => x.WsdlFault.Id == wsdlFault.Id && x.IdOntologyTerm == ontologyTerm.Id);

                if (!modelReferenceExists)
                {
                    throw new SawsdlModelReferenceDoesNotExistException();
                }

                var modelReference = wsdlFault.SawsdlModelReferences.FirstOrDefault(x => x.IdOntologyTerm == ontologyTerm.Id);

                wsdlFault.SawsdlModelReferences.Remove(modelReference);

                _serviceDescriptionRepository.Update(wsdlFault.WsdlInterface.ServiceDescription);

                var idSawsdlModelReference = _sawsdlModelReferenceRepository.GetAll().FirstOrDefault(x => x.WsdlFault.Id == wsdlFault.Id && x.IdOntologyTerm == ontologyTerm.Id).Id;

                _sawsdlModelReferenceRepository.Remove(idSawsdlModelReference);
            }

            var serviceDescriptionRepositoryChanges = _serviceDescriptionRepository.SaveChanges();

            var sawsdlModelReferenceRepositoryChanges = _sawsdlModelReferenceRepository.SaveChanges();

            return serviceDescriptionRepositoryChanges + sawsdlModelReferenceRepositoryChanges;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="requestDTO"></param>
        /// <returns></returns>
        public int RemoveModelReferenceFromWsdlInterface(ISawsdlModelReferenceRequestRemoveDTO requestDTO)
        {
            var wsdlInterface = _serviceDescriptionRepository.GetWsdlInterface(requestDTO.IdServiceDescriptionElement, false);

            var modelReferenceUrisToRemoveFromXml = new List<string>();

            if (wsdlInterface == null)
            {
                throw new WsdlInterfaceNotFoundException();
            }

            foreach (var idOntologyTerm in requestDTO.IdOntologyTerms)
            {
                var ontologyTerm = _ontologyTermRepository.Get(idOntologyTerm);

                if (ontologyTerm == null)
                {
                    throw new OntologyTermNotFoundException();
                }

                var modelReferenceExists = _sawsdlModelReferenceRepository.GetAllWithWsdlInterface().Any(x => x.WsdlInterface.Id == wsdlInterface.Id && x.IdOntologyTerm == idOntologyTerm);

                if (!modelReferenceExists)
                {
                    throw new SawsdlModelReferenceDoesNotExistException();
                }

                var idSawsdlModelReference = _sawsdlModelReferenceRepository.GetAll().FirstOrDefault(x => x.WsdlInterface.Id == wsdlInterface.Id && x.IdOntologyTerm == idOntologyTerm).Id;

                _sawsdlModelReferenceRepository.Remove(idSawsdlModelReference);

                var modelReference = wsdlInterface.SawsdlModelReferences.FirstOrDefault(x => x.IdOntologyTerm == idOntologyTerm);

                modelReferenceUrisToRemoveFromXml.Add(modelReference.ModelReference);
            }

            var sawsdlModelReferenceRepositoryChanges = _sawsdlModelReferenceRepository.SaveChanges();

            RemoveModelReferenceXmlAttributeFromWsdlInterface(wsdlInterface, modelReferenceUrisToRemoveFromXml, wsdlInterface.ServiceDescription);

            _serviceDescriptionRepository.Update(wsdlInterface.ServiceDescription);

            var serviceDescriptionRepositoryChanges = _serviceDescriptionRepository.SaveChanges();

            return serviceDescriptionRepositoryChanges + sawsdlModelReferenceRepositoryChanges;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="requestDTO"></param>
        /// <returns></returns>
        public int RemoveModelReferenceFromWsdlOperation(ISawsdlModelReferenceRequestRemoveDTO requestDTO)
        {
            var wsdlOperation = _serviceDescriptionRepository.GetWsdlOperation(requestDTO.IdServiceDescriptionElement, false);

            var modelReferenceUrisToRemoveFromXml = new List<string>();

            if (wsdlOperation == null)
            {
                throw new WsdlOperationNotFoundException();
            }

            foreach (var idOntologyTerm in requestDTO.IdOntologyTerms)
            {
                var ontologyTerm = _ontologyTermRepository.Get(idOntologyTerm);

                if (ontologyTerm == null)
                {
                    throw new OntologyTermNotFoundException();
                }

                var modelReferenceExists = _sawsdlModelReferenceRepository.GetAllWithWsdlOperation().Any(x => x.WsdlOperation.Id == wsdlOperation.Id && x.IdOntologyTerm == idOntologyTerm);

                if (!modelReferenceExists)
                {
                    throw new SawsdlModelReferenceDoesNotExistException();
                }

                var idSawsdlModelReference = _sawsdlModelReferenceRepository.GetAll().FirstOrDefault(x => x.WsdlOperation.Id == wsdlOperation.Id && x.IdOntologyTerm == idOntologyTerm).Id;

                _sawsdlModelReferenceRepository.Remove(idSawsdlModelReference);

                var modelReference = wsdlOperation.SawsdlModelReferences.FirstOrDefault(x => x.IdOntologyTerm == idOntologyTerm);

                modelReferenceUrisToRemoveFromXml.Add(modelReference.ModelReference);
            }

            var sawsdlModelReferenceRepositoryChanges = _sawsdlModelReferenceRepository.SaveChanges();

            RemoveModelReferenceXmlAttributeFromWsdlOperation(wsdlOperation, modelReferenceUrisToRemoveFromXml, wsdlOperation.WsdlInterface.ServiceDescription);

            _serviceDescriptionRepository.Update(wsdlOperation.WsdlInterface.ServiceDescription);

            var serviceDescriptionRepositoryChanges = _serviceDescriptionRepository.SaveChanges();

            return serviceDescriptionRepositoryChanges + sawsdlModelReferenceRepositoryChanges;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="requestDTO"></param>
        /// <returns></returns>
        public int RemoveModelReferenceFromXsdComplexType(ISawsdlModelReferenceRequestRemoveDTO requestDTO)
        {
            var xsdComplexElement = _serviceDescriptionRepository.GetXsdComplexType(requestDTO.IdServiceDescriptionElement, false);

            var modelReferenceUrisToRemoveFromXml = new List<string>();

            if (xsdComplexElement == null)
            {
                throw new XsdComplexTypeNotFoundException();
            }

            foreach (var idOntologyTerm in requestDTO.IdOntologyTerms)
            {
                var ontologyTerm = _ontologyTermRepository.Get(idOntologyTerm);

                if (ontologyTerm == null)
                {
                    throw new OntologyTermNotFoundException();
                }

                var modelReferenceExists = _sawsdlModelReferenceRepository.GetAllWithXsdComplexType().Any(x => x.XsdComplexType.Id == xsdComplexElement.Id && x.IdOntologyTerm == idOntologyTerm);

                if (!modelReferenceExists)
                {
                    throw new SawsdlModelReferenceDoesNotExistException();
                }

                var idSawsdlModelReference = _sawsdlModelReferenceRepository.GetAll().FirstOrDefault(x => x.XsdComplexType.Id == xsdComplexElement.Id && x.IdOntologyTerm == idOntologyTerm).Id;

                _sawsdlModelReferenceRepository.Remove(idSawsdlModelReference);

                var modelReference = xsdComplexElement.SawsdlModelReferences.FirstOrDefault(x => x.IdOntologyTerm == idOntologyTerm);

                modelReferenceUrisToRemoveFromXml.Add(modelReference.ModelReference);
            }

            var sawsdlModelReferenceRepositoryChanges = _sawsdlModelReferenceRepository.SaveChanges();

            RemoveModelReferenceXmlAttributeFromXsdComplexElement(xsdComplexElement, modelReferenceUrisToRemoveFromXml, xsdComplexElement.XsdDocument.ServiceDescription);

            _serviceDescriptionRepository.Update(xsdComplexElement.XsdDocument.ServiceDescription);

            var serviceDescriptionRepositoryChanges = _serviceDescriptionRepository.SaveChanges();

            return serviceDescriptionRepositoryChanges + sawsdlModelReferenceRepositoryChanges;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="requestDTO"></param>
        /// <returns></returns>
        public int RemoveModelReferenceFromXsdSimpleType(ISawsdlModelReferenceRequestRemoveDTO requestDTO)
        {
            var xsdSimpleType = _serviceDescriptionRepository.GetXsdSimpleType(requestDTO.IdServiceDescriptionElement, false);

            var modelReferenceUrisToRemoveFromXml = new List<string>();

            if (xsdSimpleType == null)
            {
                throw new XsdSimpleTypeNotFoundException();
            }

            foreach (var idOntologyTerm in requestDTO.IdOntologyTerms)
            {
                var ontologyTerm = _ontologyTermRepository.Get(idOntologyTerm);

                if (ontologyTerm == null)
                {
                    throw new OntologyTermNotFoundException();
                }

                var modelReferenceExists = _sawsdlModelReferenceRepository.GetAllWithXsdComplexType().Any(x => x.XsdSimpleType.Id == xsdSimpleType.Id && x.IdOntologyTerm == idOntologyTerm);

                if (!modelReferenceExists)
                {
                    throw new SawsdlModelReferenceDoesNotExistException();
                }

                var idSawsdlModelReference = _sawsdlModelReferenceRepository.GetAll().FirstOrDefault(x => x.XsdSimpleType.Id == xsdSimpleType.Id && x.IdOntologyTerm == idOntologyTerm).Id;

                _sawsdlModelReferenceRepository.Remove(idSawsdlModelReference);

                var modelReference = xsdSimpleType.SawsdlModelReferences.FirstOrDefault(x => x.IdOntologyTerm == idOntologyTerm);

                modelReferenceUrisToRemoveFromXml.Add(modelReference.ModelReference);
            }

            var sawsdlModelReferenceRepositoryChanges = _sawsdlModelReferenceRepository.SaveChanges();

            RemoveModelReferenceXmlAttributeFromXsdSimpleType(xsdSimpleType, modelReferenceUrisToRemoveFromXml, xsdSimpleType.XsdDocument.ServiceDescription);

            _serviceDescriptionRepository.Update(xsdSimpleType.XsdDocument.ServiceDescription);

            var serviceDescriptionRepositoryChanges = _serviceDescriptionRepository.SaveChanges();

            return serviceDescriptionRepositoryChanges + sawsdlModelReferenceRepositoryChanges;
        }

        #endregion Remove Model Reference

        #endregion Model Reference

        #endregion Public methods

        #region Private methods

        #region Model Reference

        #region Add Model Reference Attribute in Wsdl (Xml)

        /// <summary>
        ///
        /// </summary>
        /// <param name="modelReference"></param>
        /// <param name="xElement"></param>
        private void AddModelReferenceAsAttributeToXElement(string modelReference, XElement xElement)
        {
            if (xElement.Attribute(sawsdlNamespace + MODEL_REFERENCE_ATTRIBUTE) == null)
            {
                var modelReferenceAttribute = new XAttribute(sawsdlNamespace + MODEL_REFERENCE_ATTRIBUTE, modelReference);

                xElement.Add(modelReferenceAttribute);
            }
            else
            {
                xElement.Attribute(sawsdlNamespace + MODEL_REFERENCE_ATTRIBUTE).SetValue($"{xElement.Attribute(sawsdlNamespace + MODEL_REFERENCE_ATTRIBUTE).Value} {modelReference}");
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="wsdlInterface"></param>
        /// <param name="ontologyTerm"></param>
        /// <param name="serviceDescription"></param>
        private void AddModelReferenceXmlAttributeToWsdlInterface(WsdlInterface wsdlInterface, OntologyTerm ontologyTerm, ServiceDescription serviceDescription)
        {
            var wsdl = XDocument.Parse(serviceDescription.Xml);

            var descriptionXElement = wsdl.Descendants(wsdlNamespace + "description").FirstOrDefault();

            AddSawsdlNamespaceToRootXElement(descriptionXElement);

            var xElement = wsdl.Descendants(wsdlNamespace + "interface").FirstOrDefault(x => (string)x.Attribute("name") == wsdlInterface.WsdlInterfaceName);

            AddModelReferenceAsAttributeToXElement(ontologyTerm.TermUri, xElement);

            serviceDescription.Xml = wsdl.ToString();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="wsdlOperation"></param>
        /// <param name="ontologyTerm"></param>
        /// <param name="serviceDescription"></param>
        private void AddModelReferenceXmlAttributeToWsdlOperation(WsdlOperation wsdlOperation, OntologyTerm ontologyTerm, ServiceDescription serviceDescription)
        {
            var wsdl = XDocument.Parse(serviceDescription.Xml);

            var descriptionXElement = wsdl.Descendants(wsdlNamespace + "description").FirstOrDefault();

            AddSawsdlNamespaceToRootXElement(descriptionXElement);

            var xElement = wsdl.Descendants(wsdlNamespace + "operation").FirstOrDefault(x => (string)x.Attribute("name") == wsdlOperation.WsdlOperationName);

            AddModelReferenceAsAttributeToXElement(ontologyTerm.TermUri, xElement);

            serviceDescription.Xml = wsdl.ToString();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="xsdComplexElement"></param>
        /// <param name="ontologyTerm"></param>
        /// <param name="serviceDescription"></param>
        private void AddModelReferenceXmlAttributeToXsdComplexElement(XsdComplexType xsdComplexElement, OntologyTerm ontologyTerm, ServiceDescription serviceDescription)
        {
            var wsdl = XDocument.Parse(serviceDescription.Xml);

            var descriptionXElement = wsdl.Descendants(wsdlNamespace + "description").FirstOrDefault();

            AddSawsdlNamespaceToRootXElement(descriptionXElement);

            var xElement = FindXElementForXsdComplexType(xsdComplexElement, wsdl);

            if (xElement != null)
            {
                AddModelReferenceAsAttributeToXElement(ontologyTerm.TermUri, xElement);
            }

            serviceDescription.Xml = wsdl.ToString();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="xsdSimpleType"></param>
        /// <param name="ontologyTerm"></param>
        /// <param name="serviceDescription"></param>
        private void AddModelReferenceXmlAttributeToXsdSimpleType(XsdSimpleType xsdSimpleType, OntologyTerm ontologyTerm, ServiceDescription serviceDescription)
        {
            var wsdl = XDocument.Parse(serviceDescription.Xml);

            var descriptionXElement = wsdl.Descendants(wsdlNamespace + "description").FirstOrDefault();

            AddSawsdlNamespaceToRootXElement(descriptionXElement);

            var xElement = FindXElementForXsdSimpleType(xsdSimpleType, wsdl, serviceDescription.XsdDocument.XsdComplexTypes.ToList());

            AddModelReferenceAsAttributeToXElement(ontologyTerm.TermUri, xElement);

            serviceDescription.Xml = wsdl.ToString();
        }

        #endregion Add Model Reference Attribute in Wsdl (Xml)

        #region Remove Model Reference Attribute in Wsdl (Xml)

        /// <summary>
        ///
        /// </summary>
        /// <param name="modelReference"></param>
        /// <param name="xElement"></param>
        private void RemoveModelReferenceAsAttributeFromElement(string modelReference, XElement xElement)
        {
            var newValue = xElement.Attribute(sawsdlNamespace + MODEL_REFERENCE_ATTRIBUTE).Value.Replace(modelReference, string.Empty).Trim();

            if (string.IsNullOrEmpty(newValue))
            {
                xElement.Attribute(sawsdlNamespace + MODEL_REFERENCE_ATTRIBUTE).Remove();
            }
            else
            {
                xElement.Attribute(sawsdlNamespace + MODEL_REFERENCE_ATTRIBUTE).SetValue(newValue);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="wsdlInterface"></param>
        /// <param name="serviceDescription"></param>
        /// <param name="modelReferenceUrisToRemoveFromXml">todo: describe modelReferenceUrisToRemoveFromXml parameter on RemoveModelReferenceXmlAttributeToWsdlInterface</param>
        private void RemoveModelReferenceXmlAttributeFromWsdlInterface(WsdlInterface wsdlInterface, List<string> modelReferenceUrisToRemoveFromXml, ServiceDescription serviceDescription)
        {
            var wsdl = XDocument.Parse(serviceDescription.Xml);

            var xElement = wsdl.Descendants(wsdlNamespace + "interface").FirstOrDefault(x => (string)x.Attribute("name") == wsdlInterface.WsdlInterfaceName);

            foreach (var uri in modelReferenceUrisToRemoveFromXml)
            {
                RemoveModelReferenceAsAttributeFromElement(uri, xElement);
            }

            var exists = ExistsSawsdlAttributes(xElement);

            if (!exists)
            {
                RemoveSawsdlNamespaceFromXElement(xElement);
            }

            serviceDescription.Xml = wsdl.ToString();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="wsdlOperation"></param>
        /// <param name="serviceDescription"></param>
        /// <param name="modelReferenceUrisToRemoveFromXml">todo: describe modelReferenceUrisToRemoveFromXml parameter on RemoveModelReferenceXmlAttributeToWsdlInterface</param>
        private void RemoveModelReferenceXmlAttributeFromWsdlOperation(WsdlOperation wsdlOperation, List<string> modelReferenceUrisToRemoveFromXml, ServiceDescription serviceDescription)
        {
            var wsdl = XDocument.Parse(serviceDescription.Xml);

            var xElement = wsdl.Descendants(wsdlNamespace + "operation").FirstOrDefault(x => (string)x.Attribute("name") == wsdlOperation.WsdlOperationName);

            foreach (var uri in modelReferenceUrisToRemoveFromXml)
            {
                RemoveModelReferenceAsAttributeFromElement(uri, xElement);
            }

            var exists = ExistsSawsdlAttributes(xElement);

            if (!exists)
            {
                RemoveSawsdlNamespaceFromXElement(xElement);
            }

            serviceDescription.Xml = wsdl.ToString();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="xsdComplexElement"></param>
        /// <param name="serviceDescription"></param>
        /// <param name="modelReferenceUrisToRemoveFromXml">todo: describe modelReferenceUrisToRemoveFromXml parameter on RemoveModelReferenceXmlAttributeToWsdlInterface</param>
        private void RemoveModelReferenceXmlAttributeFromXsdComplexElement(XsdComplexType xsdComplexElement, List<string> modelReferenceUrisToRemoveFromXml, ServiceDescription serviceDescription)
        {
            var wsdl = XDocument.Parse(serviceDescription.Xml);

            var xElement = FindXElementForXsdComplexType(xsdComplexElement, wsdl);

            if (xElement != null)
            {
                foreach (var uri in modelReferenceUrisToRemoveFromXml)
                {
                    RemoveModelReferenceAsAttributeFromElement(uri, xElement);
                }

                var exists = ExistsSawsdlAttributes(xElement);

                if (!exists)
                {
                    RemoveSawsdlNamespaceFromXElement(xElement);
                }
            }

            serviceDescription.Xml = wsdl.ToString();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="xsdSimpleType"></param>
        /// <param name="serviceDescription"></param>
        /// <param name="modelReferenceUrisToRemoveFromXml">todo: describe modelReferenceUrisToRemoveFromXml parameter on RemoveModelReferenceXmlAttributeToWsdlInterface</param>
        private void RemoveModelReferenceXmlAttributeFromXsdSimpleType(XsdSimpleType xsdSimpleType, List<string> modelReferenceUrisToRemoveFromXml, ServiceDescription serviceDescription)
        {
            var wsdl = XDocument.Parse(serviceDescription.Xml);

            var xElement = FindXElementForXsdSimpleType(xsdSimpleType, wsdl, serviceDescription.XsdDocument.XsdComplexTypes.ToList());

            foreach (var uri in modelReferenceUrisToRemoveFromXml)
            {
                RemoveModelReferenceAsAttributeFromElement(uri, xElement);

                var exists = ExistsSawsdlAttributes(xElement);

                if (!exists)
                {
                    RemoveSawsdlNamespaceFromXElement(xElement);
                }
            }

            serviceDescription.Xml = wsdl.ToString();
        }

        #endregion Remove Model Reference Attribute in Wsdl (Xml)

        #endregion Model Reference

        #region Schema Mapping

        #region Add Schema Mapping Attribute in Wsdl (Xml)

        /// <summary>
        ///
        /// </summary>
        /// <param name="xElement"></param>
        /// <param name="liftingSchemaMapping"></param>
        private void AddLiftingSchemaMappingAsAttributeToXElement(string liftingSchemaMapping, XElement xElement)
        {
            if (xElement.Attribute(sawsdlNamespace + LIFTING_SCHEMA_MAPPING_ATTRIBUTE) == null)
            {
                var modelReferenceAttribute = new XAttribute(sawsdlNamespace + LIFTING_SCHEMA_MAPPING_ATTRIBUTE, liftingSchemaMapping);
                xElement.Add(modelReferenceAttribute);
            }
            else
            {
                xElement.Attribute(sawsdlNamespace + LIFTING_SCHEMA_MAPPING_ATTRIBUTE).SetValue(liftingSchemaMapping);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="xsdComplexElement"></param>
        /// <param name="serviceDescription"></param>
        /// <param name="liftingSchemaMapping"></param>
        private void AddLiftingSchemaMappingXmlAttributeToXsdComplexElement(XsdComplexType xsdComplexElement, ServiceDescription serviceDescription, string liftingSchemaMapping)
        {
            var wsdl = XDocument.Parse(serviceDescription.Xml);

            var descriptionXElement = wsdl.Descendants(wsdlNamespace + "description").FirstOrDefault();

            AddSawsdlNamespaceToRootXElement(descriptionXElement);

            var xElement = FindXElementForXsdComplexType(xsdComplexElement, wsdl);

            if (xElement != null)
            {
                AddLiftingSchemaMappingAsAttributeToXElement(liftingSchemaMapping, xElement);
            }

            serviceDescription.Xml = wsdl.ToString();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="xsdSimpleType"></param>
        /// <param name="serviceDescription"></param>
        /// <param name="liftingSchemaMapping"></param>
        private void AddLiftingSchemaMappingXmlAttributeToXsdSimpleType(XsdSimpleType xsdSimpleType, ServiceDescription serviceDescription, string liftingSchemaMapping)
        {
            var wsdl = XDocument.Parse(serviceDescription.Xml);

            var descriptionXElement = wsdl.Descendants(wsdlNamespace + "description").FirstOrDefault();

            AddSawsdlNamespaceToRootXElement(descriptionXElement);

            var xElement = FindXElementForXsdSimpleType(xsdSimpleType, wsdl, serviceDescription.XsdDocument.XsdComplexTypes.ToList());

            AddLiftingSchemaMappingAsAttributeToXElement(liftingSchemaMapping, xElement);

            serviceDescription.Xml = wsdl.ToString();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="xElement"></param>
        /// <param name="loweringSchemaMapping"></param>
        private void AddLoweringSchemaMappingAsAttributeToXElement(string loweringSchemaMapping, XElement xElement)
        {
            if (xElement.Attribute(sawsdlNamespace + LOWERING_SCHEMA_MAPPING_ATTRIBUTE) == null)
            {
                var modelReferenceAttribute = new XAttribute(sawsdlNamespace + LOWERING_SCHEMA_MAPPING_ATTRIBUTE, loweringSchemaMapping);
                xElement.Add(modelReferenceAttribute);
            }
            else
            {
                xElement.Attribute(sawsdlNamespace + LOWERING_SCHEMA_MAPPING_ATTRIBUTE).SetValue(loweringSchemaMapping);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="xsdComplexType"></param>
        /// <param name="serviceDescription"></param>
        /// <param name="loweringSchemaMapping"></param>
        private void AddLoweringSchemaMappingXmlAttributeToXsdComplexType(XsdComplexType xsdComplexType, ServiceDescription serviceDescription, string loweringSchemaMapping)
        {
            var wsdl = XDocument.Parse(serviceDescription.Xml);

            var descriptionXElement = wsdl.Descendants(wsdlNamespace + "description").FirstOrDefault();

            AddSawsdlNamespaceToRootXElement(descriptionXElement);

            var xElement = FindXElementForXsdComplexType(xsdComplexType, wsdl);

            if (xElement != null)
            {
                AddLoweringSchemaMappingAsAttributeToXElement(loweringSchemaMapping, xElement);
            }

            serviceDescription.Xml = wsdl.ToString();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="xsdSimpleType"></param>
        /// <param name="serviceDescription"></param>
        /// <param name="loweringSchemaMapping"></param>
        private void AddLoweringSchemaMappingXmlAttributeToXsdSimpleType(XsdSimpleType xsdSimpleType, ServiceDescription serviceDescription, string loweringSchemaMapping)
        {
            var wsdl = XDocument.Parse(serviceDescription.Xml);

            var descriptionXElement = wsdl.Descendants(wsdlNamespace + "description").FirstOrDefault();

            AddSawsdlNamespaceToRootXElement(descriptionXElement);

            var xElement = FindXElementForXsdSimpleType(xsdSimpleType, wsdl, serviceDescription.XsdDocument.XsdComplexTypes.ToList());

            AddLoweringSchemaMappingAsAttributeToXElement(loweringSchemaMapping, xElement);

            serviceDescription.Xml = wsdl.ToString();
        }

        #endregion Add Schema Mapping Attribute in Wsdl (Xml)

        #region Remove Schema Mapping Attribute in Wsdl (Xml)

        /// <summary>
        ///
        /// </summary>
        /// <param name="xElement"></param>
        private void RemoveLiftingSchemaMappingAsAttributeFromXElement(XElement xElement)
        {
            if (xElement.Attribute(sawsdlNamespace + LIFTING_SCHEMA_MAPPING_ATTRIBUTE) != null)
            {
                xElement.Attribute(sawsdlNamespace + LIFTING_SCHEMA_MAPPING_ATTRIBUTE).Remove();
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="xsdComplexType"></param>
        /// <param name="serviceDescription"></param>
        private void RemoveLiftingSchemaMappingXmlAttributeFromXsdComplexType(XsdComplexType xsdComplexType, ServiceDescription serviceDescription)
        {
            var wsdl = XDocument.Parse(serviceDescription.Xml);

            var xElement = FindXElementForXsdComplexType(xsdComplexType, wsdl);

            if (xElement != null)
            {
                RemoveLiftingSchemaMappingAsAttributeFromXElement(xElement);

                var exists = ExistsSawsdlAttributes(xElement);

                if (!exists)
                {
                    RemoveSawsdlNamespaceFromXElement(xElement);
                }
            }

            serviceDescription.Xml = wsdl.ToString();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="xsdSimpleType"></param>
        /// <param name="serviceDescription"></param>
        private void RemoveLiftingSchemaMappingXmlAttributeFromXsdSimpleType(XsdSimpleType xsdSimpleType, ServiceDescription serviceDescription)
        {
            var wsdl = XDocument.Parse(serviceDescription.Xml);

            var xElement = FindXElementForXsdSimpleType(xsdSimpleType, wsdl, serviceDescription.XsdDocument.XsdComplexTypes.ToList());

            if (xElement != null)
            {
                RemoveLiftingSchemaMappingAsAttributeFromXElement(xElement);

                var exists = ExistsSawsdlAttributes(xElement);

                if (!exists)
                {
                    RemoveSawsdlNamespaceFromXElement(xElement);
                }
            }

            serviceDescription.Xml = wsdl.ToString();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="xElement"></param>
        private void RemoveLoweringSchemaMappingAsAttributeFromXElement(XElement xElement)
        {
            if (xElement.Attribute(sawsdlNamespace + LOWERING_SCHEMA_MAPPING_ATTRIBUTE) != null)
            {
                xElement.Attribute(sawsdlNamespace + LOWERING_SCHEMA_MAPPING_ATTRIBUTE).Remove();
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="xsdComplexType"></param>
        /// <param name="serviceDescription"></param>
        private void RemoveLoweringSchemaMappingXmlAttributeFromXsdComplexType(XsdComplexType xsdComplexType, ServiceDescription serviceDescription)
        {
            var wsdl = XDocument.Parse(serviceDescription.Xml);

            var xElement = FindXElementForXsdComplexType(xsdComplexType, wsdl);

            if (xElement != null)
            {
                RemoveLoweringSchemaMappingAsAttributeFromXElement(xElement);

                var exists = ExistsSawsdlAttributes(xElement);

                if (!exists)
                {
                    RemoveSawsdlNamespaceFromXElement(xElement);
                }
            }

            serviceDescription.Xml = wsdl.ToString();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="xsdSimpleType"></param>
        /// <param name="serviceDescription"></param>
        private void RemoveLoweringSchemaMappingXmlAttributeFromXsdSimpleType(XsdSimpleType xsdSimpleType, ServiceDescription serviceDescription)
        {
            var wsdl = XDocument.Parse(serviceDescription.Xml);

            var xElement = FindXElementForXsdSimpleType(xsdSimpleType, wsdl, serviceDescription.XsdDocument.XsdComplexTypes.ToList());

            if (xElement != null)
            {
                RemoveLoweringSchemaMappingAsAttributeFromXElement(xElement);

                var exists = ExistsSawsdlAttributes(xElement);

                if (!exists)
                {
                    RemoveSawsdlNamespaceFromXElement(xElement);
                }
            }

            serviceDescription.Xml = wsdl.ToString();
        }

        #endregion Remove Schema Mapping Attribute in Wsdl (Xml)

        #endregion Schema Mapping

        /// <summary>
        ///
        /// </summary>
        /// <param name="descriptionXElement"></param>
        private void AddSawsdlNamespaceToRootXElement(XElement descriptionXElement)
        {
            var ns = new XAttribute(XNamespace.Xmlns + "sawsdl", SAWSDL_NAMESPACE);

            if (!descriptionXElement.Attributes().Any(x => x.IsNamespaceDeclaration && x.Value == SAWSDL_NAMESPACE))
            {
                descriptionXElement.Add(ns);
            }
        }

        /// <summary>
        /// Check if exist any SAWSDL attribute in the XElement. If exist returns true, if don't returns false
        /// </summary>
        /// <param name="xElement"></param>
        /// <returns></returns>
        private bool ExistsSawsdlAttributes(XElement xElement)
        {
            if (xElement.Attribute(sawsdlNamespace + MODEL_REFERENCE_ATTRIBUTE) != null)
            {
                return true;
            }

            if (xElement.Attribute(sawsdlNamespace + LIFTING_SCHEMA_MAPPING_ATTRIBUTE) != null)
            {
                return true;
            }

            if (xElement.Attribute(sawsdlNamespace + LOWERING_SCHEMA_MAPPING_ATTRIBUTE) != null)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Find the XElement inside the XDocument that matches the XsdComplexElement from the DB
        /// </summary>
        /// <param name="xsdComplexType"></param>
        /// <param name="wsdl"></param>
        /// <returns></returns>
        private XElement FindXElementForXsdComplexType(XsdComplexType xsdComplexType, XDocument wsdl)
        {
            var xElementsComplexType = wsdl.Descendants(xsdNamespace + "complexType")
                .Where(x => x.Parent.Name == xsdNamespace + "schema")
                .Distinct()
                .ToList();

            foreach (var xElement in xElementsComplexType)
            {
                if (xElement.Attributes().FirstOrDefault(x => x.Name.ToString().ToUpper() == "NAME").Value == xsdComplexType.XsdComplexTypeName)
                {
                    return xElement;
                }
            }

            xElementsComplexType = wsdl.Descendants(xsdNamespace + "complexType")
                .Where(x => x.Parent.Name == xsdNamespace + "element")
                .Distinct()
                .ToList();

            foreach (var xElement in xElementsComplexType)
            {
                if (xElement.Parent.Attributes().FirstOrDefault(x => x.Name.ToString().ToUpper() == "NAME").Value == xsdComplexType.XsdComplexTypeName)
                {
                    return xElement.Parent;
                }
            }

            return null;
        }

        private List<XElement> GetXsdSimpleTypesFromComplexTypes(XDocument xDocument, ICollection<XsdComplexType> xsdComplexTypes)
        {
            var xsdSimpleTypes1 = xDocument.Descendants(xsdNamespace + "complexType")
                .Where(x => x.Parent.Name == xsdNamespace + "schema")
                .Descendants(xsdNamespace + "element")
                .Where(x => x.Attribute("type").Value.StartsWith($"{x.GetPrefixOfNamespace(xsdNamespace)}:"))
                .Distinct()
                .ToList();

            var xsdSimpleTypes2 = xDocument.Descendants(xsdNamespace + "complexType")
                .Where(x => x.Parent.Name == xsdNamespace + "element")
                .Descendants(xsdNamespace + "element")
                .Distinct()
                .ToList();

            var r = new List<XElement>();
            r.AddRange(xsdSimpleTypes1);
            r.AddRange(xsdSimpleTypes2);

            return r;
        }

        private List<XElement> GetXsdSimpleTypesFromRoot(XDocument xDocument)
        {
            var xsdSimpleTypes = xDocument.Descendants(xsdNamespace + "simpleType")
                                    .Distinct()
                                    .ToList();

            return xsdSimpleTypes;
        }

        private XsdComplexType FindParentComplexType(XElement xElement, ICollection<XsdComplexType> xsdComplexTypes)
        {
            if (xElement.Parent.Name == xsdNamespace + "complexType")
            {
                return xsdComplexTypes.FirstOrDefault(ct => ct.XsdComplexTypeName == (string)xElement.Parent.Attribute("name"));
            }
            else
            {
                return FindParentComplexType(xElement.Parent, xsdComplexTypes);
            }
        }

        /// <summary>
        /// Find the XElement inside the XDocument that matches the XsdSimpleElement from the DB
        /// </summary>
        /// <param name="xsdSimpleElement"></param>
        /// <param name="wsdl"></param>
        /// <returns></returns>
        private XElement FindXElementForXsdSimpleType(XsdSimpleType xsdSimpleElement, XDocument wsdl, List<XsdComplexType> xsdComplexTypes)
        {
            var xsdSimpleTypes = GetXsdSimpleTypesFromRoot(wsdl);

            var xsdSimpleTypesInsideComplexTypes = GetXsdSimpleTypesFromComplexTypes(wsdl, xsdComplexTypes);

            var xElement = xsdSimpleTypes
                .FirstOrDefault(x => (string)x.Attribute("name") == xsdSimpleElement.XsdSimpleTypeName
                    || (string)x.Attribute("name") == xsdSimpleElement.XsdSimpleTypeElementType);

            if (xElement == null)
            {
                var eles = xsdSimpleTypesInsideComplexTypes
                    .Where(x => (string)x.Attribute("name") == xsdSimpleElement.XsdSimpleTypeName
                        || (string)x.Attribute("type") == xsdSimpleElement.XsdSimpleTypeElementType);

                foreach (var e in eles)
                {
                    var parentOK = CheckSimpleTypeByComplexTypeName(e, xsdSimpleElement.XsdComplexType.XsdComplexTypeName);

                    if (parentOK)
                    {
                        xElement = e;
                        break;
                    }
                }
            }

            return xElement;
        }

        private bool CheckSimpleTypeByComplexTypeName(XElement e, string xsdComplexTypeName)
        {
            if (e.Parent == null)
            {
                return false;
            }

            var parentFound = (string)e.Parent.Attribute("name") == xsdComplexTypeName;

            if (parentFound)
            {
                return parentFound;
            }
            else
            {
                return CheckSimpleTypeByComplexTypeName(e.Parent, xsdComplexTypeName);
            }
        }

        /// <summary>
        /// Remove the SAWSDL Namespace from the XElement
        /// </summary>
        /// <param name="xElement"></param>
        private void RemoveSawsdlNamespaceFromXElement(XElement xElement)
        {
            var namespaces = xElement.Attributes().Where(x => x.IsNamespaceDeclaration);

            foreach (var ns in namespaces)
            {
                if (ns.Value == SAWSDL_NAMESPACE)
                {
                    ns.Remove();
                }
            }
        }

        #endregion Private methods
    }
}