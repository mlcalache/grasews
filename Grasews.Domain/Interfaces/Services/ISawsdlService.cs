using Grasews.Domain.Entities;
using Grasews.Domain.Interfaces.DTOs;
using System.Collections.Generic;

namespace Grasews.Domain.Interfaces.Services
{
    public interface ISawsdlService : IBaseService
    {
        #region Schema Mapping

        #region Get Schema Mapping

        #region Get Lifting Schema Mapping

        /// <summary>
        /// Get the Lifting Schema Mapping from a Xsd Complext Type in the WSDL document
        /// </summary>
        /// <param name="requestDTO"></param>
        /// <returns></returns>
        string GetLiftingSchemaMappingFromXsdComplexType(ISawsdlSchemaMappingRequestViewDTO requestDTO);

        /// <summary>
        /// Get the Lifting Schema Mapping from a Xsd Simple Type in the WSDL document
        /// </summary>
        /// <param name="requestDTO"></param>
        /// <returns></returns>
        string GetLiftingSchemaMappingFromXsdSimpleType(ISawsdlSchemaMappingRequestViewDTO requestDTO);

        #endregion Get Lifting Schema Mapping

        #region Get Lowering Schema Mapping

        /// <summary>
        /// Get the Lowering Schema Mapping from a Xsd Complext Type in the WSDL document
        /// </summary>
        /// <param name="requestDTO"></param>
        /// <returns></returns>
        string GetLoweringSchemaMappingFromXsdComplexType(ISawsdlSchemaMappingRequestViewDTO requestDTO);

        /// <summary>
        /// Get the Lowering Schema Mapping from a Xsd Simple Type in the WSDL document
        /// </summary>
        /// <param name="requestDTO"></param>
        /// <returns></returns>
        string GetLoweringSchemaMappingFromXsdSimpleType(ISawsdlSchemaMappingRequestViewDTO requestDTO);

        #endregion Get Lowering Schema Mapping

        #endregion Get Schema Mapping

        #region Add Schema Mapping

        #region Add Lifting Schema Mapping

        /// <summary>
        /// Add a SAWSDL Lifting Schema Mapping to a Xsd Complext Type  in the WSDL document
        /// </summary>
        /// <param name="requestDTO"></param>
        /// <returns></returns>
        int AddLiftingSchemaMappingToXsdComplexType(ISawsdlLiftingSchemaMappingRequestCreateDTO requestDTO);

        /// <summary>
        /// Add a SAWSDL Lifting Schema Mapping to a Xsd Simple Type in the WSDL document
        /// </summary>
        /// <param name="requestDTO"></param>
        /// <returns></returns>
        int AddLiftingSchemaMappingToXsdSimpleType(ISawsdlLiftingSchemaMappingRequestCreateDTO requestDTO);

        #endregion Add Lifting Schema Mapping

        #region Add Lowering Schema Mapping

        /// <summary>
        /// Add a SAWSDL Lowering Schema Mapping to a Xsd Complext Type in the WSDL document
        /// </summary>
        /// <param name="requestDTO"></param>
        /// <returns></returns>
        int AddLoweringSchemaMappingToXsdComplexType(ISawsdlLoweringSchemaMappingRequestCreateDTO requestDTO);

        /// <summary>
        /// Add a SAWSDL Lowering Schema Mapping to a Xsd Simple Type in the WSDL document
        /// </summary>
        /// <param name="requestDTO"></param>
        /// <returns></returns>
        int AddLoweringSchemaMappingToXsdSimpleType(ISawsdlLoweringSchemaMappingRequestCreateDTO requestDTO);

        #endregion Add Lowering Schema Mapping

        #endregion Add Schema Mapping

        #region Remove Schema Mapping

        #region Remove Lifting Schema Mapping

        /// <summary>
        /// Remove a SAWSDL Lifting Schema Mapping from a Xsd Complext Type in the WSDL document
        /// </summary>
        /// <param name="requestDTO"></param>
        /// <returns></returns>
        int RemoveLiftingSchemaMappingFromXsdComplexType(ISawsdlLiftingSchemaMappingRequestRemoveDTO requestDTO);

        /// <summary>
        /// Remove a SAWSDL Lifting Schema Mapping from a Xsd Simple Type in the WSDL document
        /// </summary>
        /// <param name="requestDTO"></param>
        /// <returns></returns>
        int RemoveLiftingSchemaMappingFromXsdSimpleType(ISawsdlLiftingSchemaMappingRequestRemoveDTO requestDTO);

        #endregion Remove Lifting Schema Mapping

        #region Remove Lowering Schema Mapping

        /// <summary>
        /// Remove a SAWSDL Lowering Schema Mapping from a Xsd Complext Type in the WSDL document
        /// </summary>
        /// <param name="requestDTO"></param>
        /// <returns></returns>
        int RemoveLoweringSchemaMappingFromXsdComplexType(ISawsdlLoweringSchemaMappingRequestRemoveDTO requestDTO);

        /// <summary>
        /// Remove a SAWSDL Lowering Schema Mapping from a Xsd Simple Type in the WSDL document
        /// </summary>
        /// <param name="requestDTO"></param>
        /// <returns></returns>
        int RemoveLoweringSchemaMappingFromXsdSimpleType(ISawsdlLoweringSchemaMappingRequestRemoveDTO requestDTO);

        #endregion Remove Lowering Schema Mapping

        #endregion Remove Schema Mapping

        #region Update Schema Mapping

        #region Update Lifting Schema Mapping

        /// <summary>
        /// Update a SAWSDL Lifting Schema Mapping from a Xsd Complext Type in the WSDL document
        /// </summary>
        /// <param name="requestDTO"></param>
        /// <returns></returns>
        int UpdateLiftingSchemaMappingInXsdComplexType(ISawsdlLiftingSchemaMappingRequestUpdateDTO requestDTO);

        /// <summary>
        /// Update a SAWSDL Lifting Schema Mapping from a Xsd Simple Type in the WSDL document
        /// </summary>
        /// <param name="requestDTO"></param>
        /// <returns></returns>
        int UpdateLiftingSchemaMappingInXsdSimpleType(ISawsdlLiftingSchemaMappingRequestUpdateDTO requestDTO);

        #endregion Update Lifting Schema Mapping

        #region Update Lowering Schema Mapping

        /// <summary>
        /// Update a SAWSDL Lowering Schema Mapping from a Xsd Element Complext Type in the WSDL document
        /// </summary>
        /// <param name="requestDTO"></param>
        /// <returns></returns>
        int UpdateLoweringSchemaMappingInXsdComplexType(ISawsdlLoweringSchemaMappingRequestUpdateDTO requestDTO);

        /// <summary>
        /// Update a SAWSDL Lowering Schema Mapping from a Xsd Simple Type in the WSDL document
        /// </summary>
        /// <param name="requestDTO"></param>
        /// <returns></returns>
        int UpdateLoweringSchemaMappingInXsdSimpleType(ISawsdlLoweringSchemaMappingRequestUpdateDTO requestDTO);

        #endregion Update Lowering Schema Mapping

        #endregion Update Schema Mapping

        #endregion Schema Mapping

        #region Model Reference

        #region Get Model References

        IEnumerable<SawsdlModelReference> GetModelReferencesFromServiceDescription(int id);

        //IEnumerable<SawsdlModelReference> GetModelReferencesFromWsdlInfault(ISawsdlModelReferenceRequestViewDTO requestDTO);
        IEnumerable<SawsdlModelReference> GetModelReferencesFromWsdlFault(ISawsdlModelReferenceRequestViewDTO requestDTO);

        IEnumerable<SawsdlModelReference> GetModelReferencesFromWsdlInterface(ISawsdlModelReferenceRequestViewDTO requestDTO);

        IEnumerable<SawsdlModelReference> GetModelReferencesFromWsdlOperation(ISawsdlModelReferenceRequestViewDTO requestDTO);

        //IEnumerable<SawsdlModelReference> GetModelReferencesFromWsdlOutfault(ISawsdlModelReferenceRequestViewDTO requestDTO);

        IEnumerable<SawsdlModelReference> GetModelReferencesFromXsdComplexType(ISawsdlModelReferenceRequestViewDTO requestDTO);

        IEnumerable<SawsdlModelReference> GetModelReferencesFromXsdSimpleType(ISawsdlModelReferenceRequestViewDTO requestDTO);

        #endregion Get Model References

        #region Add Model Reference

        //int AddModelReferenceToWsdlInfault(ISawsdlModelReferenceRequestCreateDTO requestDTO);
        int AddModelReferenceToWsdlFault(ISawsdlModelReferenceRequestCreateDTO requestDTO);

        int AddModelReferenceToWsdlInterface(ISawsdlModelReferenceRequestCreateDTO requestDTO);

        int AddModelReferenceToWsdlOperation(ISawsdlModelReferenceRequestCreateDTO requestDTO);

        //int AddModelReferenceToWsdlOutfault(ISawsdlModelReferenceRequestCreateDTO requestDTO);

        int AddModelReferenceToXsdComplexType(ISawsdlModelReferenceRequestCreateDTO requestDTO);

        int AddModelReferenceToXsdSimpleType(ISawsdlModelReferenceRequestCreateDTO requestDTO);

        #endregion Add Model Reference

        #region Remove Model Reference

        //int RemoveModelReferenceFromWsdlInfault(ISawsdlModelReferenceRequestRemoveDTO requestDTO);
        int RemoveModelReferenceFromWsdlFault(ISawsdlModelReferenceRequestRemoveDTO requestDTO);

        int RemoveModelReferenceFromWsdlInterface(ISawsdlModelReferenceRequestRemoveDTO requestDTO);

        int RemoveModelReferenceFromWsdlOperation(ISawsdlModelReferenceRequestRemoveDTO requestDTO);

        //int RemoveModelReferenceFromWsdlOutfault(ISawsdlModelReferenceRequestRemoveDTO requestDTO);

        int RemoveModelReferenceFromXsdComplexType(ISawsdlModelReferenceRequestRemoveDTO requestDTO);

        int RemoveModelReferenceFromXsdSimpleType(ISawsdlModelReferenceRequestRemoveDTO requestDTO);

        #endregion Remove Model Reference

        #endregion Model Reference
    }
}