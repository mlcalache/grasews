using Grasews.Domain.Entities;
using Grasews.Domain.Interfaces.Repositories;
using System.Data.Entity;
using System.Linq;

namespace Grasews.Infra.Data.EF.SqlServer.Repositories
{
    public class ServiceDescriptionEntityRepository : BaseEntityRepository<ServiceDescription, int>, IServiceDescriptionEntityRepository
    {
        public ServiceDescription GetComplete(int id, bool withSawsdlModelReferences = false, bool @readonly = true)
        {
            var query = base.GetAll(@readonly)
                 .Include(nameof(ServiceDescription.WsdlInterfaces))
                 .Include("WsdlInterfaces.WsdlOperations")
                 .Include("WsdlInterfaces.WsdlOperations.WsdlInputs")
                 .Include("WsdlInterfaces.WsdlOperations.WsdlInputs.XsdComplexElement")
                 .Include("WsdlInterfaces.WsdlOperations.WsdlInputs.XsdComplexElement.XsdElements")
                 .Include("WsdlInterfaces.WsdlOperations.WsdlInputs.XsdSimpleElement")
                 .Include("WsdlInterfaces.WsdlOperations.WsdlOutputs")
                 .Include("WsdlInterfaces.WsdlOperations.WsdlOutputs.XsdComplexElement")
                 .Include("WsdlInterfaces.WsdlOperations.WsdlOutputs.XsdComplexElement.XsdElements")
                 .Include("WsdlInterfaces.WsdlOperations.WsdlOutputs.XsdSimpleElement")

                 //.Include("WsdlInterfaces.WsdlOperations.WsdlInFaults")
                 //.Include("WsdlInterfaces.WsdlOperations.WsdlInFaults.XsdComplexElement")
                 //.Include("WsdlInterfaces.WsdlOperations.WsdlInFaults.XsdComplexElement.XsdElements")
                 //.Include("WsdlInterfaces.WsdlOperations.WsdlInFaults.XsdSimpleElement")
                 //.Include("WsdlInterfaces.WsdlOperations.WsdlOutFaults")
                 //.Include("WsdlInterfaces.WsdlOperations.WsdlOutFaults.XsdComplexElement")
                 //.Include("WsdlInterfaces.WsdlOperations.WsdlOutFaults.XsdComplexElement.XsdElements")
                 //.Include("WsdlInterfaces.WsdlOperations.WsdlOutFaults.XsdSimpleElement")

                 .Include("XsdDocument")
                 .Include("XsdDocument.XsdComplexElements")
                 .Include("XsdDocument.XsdComplexElements.XsdElements")
                 .Include("XsdDocument.XsdSimpleElements");

            if (withSawsdlModelReferences)
            {
                query = query.Include("SawsdlModelReferences")
                    .Include("WsdlInterfaces.SawsdlModelReferences")
                    .Include("WsdlInterfaces.WsdlOperations.SawsdlModelReferences")
                    .Include("WsdlInterfaces.WsdlOperations.WsdlInputs.XsdComplexElement.SawsdlModelReferences")
                    .Include("WsdlInterfaces.WsdlOperations.WsdlInputs.XsdComplexElement.XsdElements.SawsdlModelReferences")
                    .Include("WsdlInterfaces.WsdlOperations.WsdlInputs.XsdSimpleElement.SawsdlModelReferences")
                    .Include("WsdlInterfaces.WsdlOperations.WsdlOutputs.XsdComplexElement.SawsdlModelReferences")
                    .Include("WsdlInterfaces.WsdlOperations.WsdlOutputs.XsdComplexElement.XsdElements.SawsdlModelReferences")
                    .Include("WsdlInterfaces.WsdlOperations.WsdlOutputs.XsdSimpleElement.SawsdlModelReferences")

                    //.Include("WsdlInterfaces.WsdlOperations.WsdlInFaults.SawsdlModelReferences")
                    //.Include("WsdlInterfaces.WsdlOperations.WsdlInFaults.XsdComplexElement.SawsdlModelReferences")
                    //.Include("WsdlInterfaces.WsdlOperations.WsdlInFaults.XsdComplexElement.XsdElements.SawsdlModelReferences")
                    //.Include("WsdlInterfaces.WsdlOperations.WsdlInFaults.XsdSimpleElement.SawsdlModelReferences")
                    //.Include("WsdlInterfaces.WsdlOperations.WsdlOutFaults.SawsdlModelReferences")
                    //.Include("WsdlInterfaces.WsdlOperations.WsdlOutFaults.XsdComplexElement.SawsdlModelReferences")
                    //.Include("WsdlInterfaces.WsdlOperations.WsdlOutFaults.XsdComplexElement.XsdElements.SawsdlModelReferences")
                    //.Include("WsdlInterfaces.WsdlOperations.WsdlOutFaults.XsdSimpleElement.SawsdlModelReferences")
                    ;
            }

            //query = query.Include("WsdlInterfaces.Issues")
            //     .Include("WsdlInterfaces.WsdlOperations.Issues")
            //     .Include("WsdlInterfaces.WsdlOperations.WsdlInputs.Issues")
            //     .Include("WsdlInterfaces.WsdlOperations.WsdlInputs.XsdComplexElement.Issues")
            //     .Include("WsdlInterfaces.WsdlOperations.WsdlInputs.XsdComplexElement.XsdElements.Issues")
            //     .Include("WsdlInterfaces.WsdlOperations.WsdlInputs.XsdSimpleElement.Issues")
            //     .Include("WsdlInterfaces.WsdlOperations.WsdlOutputs.Issues")
            //     .Include("WsdlInterfaces.WsdlOperations.WsdlOutputs.XsdComplexElement.Issues")
            //     .Include("WsdlInterfaces.WsdlOperations.WsdlOutputs.XsdComplexElement.XsdElements.Issues")
            //     .Include("WsdlInterfaces.WsdlOperations.WsdlOutputs.XsdSimpleElement.Issues")

            //     //.Include("WsdlInterfaces.WsdlOperations.WsdlInFaults.Issues")
            //     //.Include("WsdlInterfaces.WsdlOperations.WsdlInFaults.XsdComplexElement")
            //     //.Include("WsdlInterfaces.WsdlOperations.WsdlInFaults.XsdComplexElement.XsdElements.Issues")
            //     //.Include("WsdlInterfaces.WsdlOperations.WsdlInFaults.XsdSimpleElement.Issues")
            //     //.Include("WsdlInterfaces.WsdlOperations.WsdlOutFaults.Issues")
            //     //.Include("WsdlInterfaces.WsdlOperations.WsdlOutFaults.XsdComplexElement.Issues")
            //     //.Include("WsdlInterfaces.WsdlOperations.WsdlOutFaults.XsdComplexElement.XsdElements.Issues")
            //     //.Include("WsdlInterfaces.WsdlOperations.WsdlOutFaults.XsdSimpleElement.Issues")
            //     ;

            var serviceDescription = query.FirstOrDefault(x => x.Id == id);

            return serviceDescription;
        }

        public ServiceDescription GetWithOntologies(int id, bool @readonly = true)
        {
            var query = base.GetAll(@readonly)
                 .Include(nameof(ServiceDescription.ServiceDescription_Ontologies));

            var serviceDescription = query.FirstOrDefault(x => x.Id == id);

            return serviceDescription;
        }

        public WsdlInFault GetWsdlInFault(int idWsdlInFault, bool @readonly = true)
        {
            return @readonly
                ? _context.WsdlInFaults.AsNoTracking()
                    .Include(nameof(WsdlInFault.SawsdlModelReferences))
                    .Include(nameof(WsdlInFault.WsdlOperation))
                    .Include($"{nameof(WsdlInFault.WsdlOperation)}{nameof(WsdlInFault.WsdlOperation.WsdlInterface)}")
                    .Include(nameof(WsdlInFault.Issues))
                    .FirstOrDefault(x => x.Id == idWsdlInFault)
                : _context.WsdlInFaults
                    .Include(nameof(WsdlInFault.SawsdlModelReferences))
                    .Include(nameof(WsdlInFault.WsdlOperation))
                    .Include($"{nameof(WsdlInFault.WsdlOperation)}{nameof(WsdlInFault.WsdlOperation.WsdlInterface)}")
                    .Include(nameof(WsdlInFault.Issues))
                    .FirstOrDefault(x => x.Id == idWsdlInFault);
        }

        public WsdlInput GetWsdlInput(int idWsdlInput, bool @readonly = true)
        {
            return @readonly
                ? _context.WsdlInputs.AsNoTracking()
                    .Include(nameof(WsdlInput.WsdlOperation))
                    .Include($"{nameof(WsdlInput.WsdlOperation)}.{nameof(WsdlInput.WsdlOperation.WsdlInterface)}")
                    .Include($"{nameof(WsdlInput.WsdlOperation)}.{nameof(WsdlInput.WsdlOperation.WsdlInterface)}.{nameof(WsdlInput.WsdlOperation.WsdlInterface.ServiceDescription)}")
                    .Include(nameof(WsdlInput.Issues))
                    .FirstOrDefault(x => x.Id == idWsdlInput)
                : _context.WsdlInputs
                    .Include(nameof(WsdlInput.WsdlOperation))
                    .Include($"{nameof(WsdlInput.WsdlOperation)}.{nameof(WsdlInput.WsdlOperation.WsdlInterface)}")
                    .Include($"{nameof(WsdlInput.WsdlOperation)}.{nameof(WsdlInput.WsdlOperation.WsdlInterface)}.{nameof(WsdlInput.WsdlOperation.WsdlInterface.ServiceDescription)}")
                    .Include(nameof(WsdlInput.Issues))
                    .FirstOrDefault(x => x.Id == idWsdlInput);
        }

        public WsdlInterface GetWsdlInterface(int idWsdlInterface, bool @readonly = true)
        {
            return @readonly
                ? _context.WsdlInterfaces.AsNoTracking()
                    .Include(nameof(WsdlInterface.SawsdlModelReferences))
                    .Include(nameof(WsdlInterface.ServiceDescription))
                    .Include(nameof(WsdlInterface.Issues))
                    .FirstOrDefault(x => x.Id == idWsdlInterface)
                : _context.WsdlInterfaces
                    .Include(nameof(WsdlInterface.SawsdlModelReferences))
                    .Include(nameof(WsdlInterface.ServiceDescription))
                    .Include(nameof(WsdlInterface.Issues))
                    .FirstOrDefault(x => x.Id == idWsdlInterface);
        }

        public WsdlOperation GetWsdlOperation(int idWsdlOperation, bool @readonly = true)
        {
            return @readonly
                ? _context.WsdlOperations.AsNoTracking()
                    .Include(nameof(WsdlOperation.SawsdlModelReferences))
                    .Include(nameof(WsdlOperation.WsdlInterface))
                    .Include($"{nameof(WsdlOperation.WsdlInterface)}.{nameof(WsdlOperation.WsdlInterface.ServiceDescription)}")
                    .Include(nameof(WsdlOperation.Issues))
                    .FirstOrDefault(x => x.Id == idWsdlOperation)
                : _context.WsdlOperations
                    .Include(nameof(WsdlOperation.SawsdlModelReferences))
                    .Include(nameof(WsdlOperation.WsdlInterface))
                    .Include($"{nameof(WsdlOperation.WsdlInterface)}.{nameof(WsdlOperation.WsdlInterface.ServiceDescription)}")
                    .Include(nameof(WsdlOperation.Issues))
                    .FirstOrDefault(x => x.Id == idWsdlOperation);
        }

        public WsdlOutFault GetWsdlOutFault(int idWsdlOutFault, bool @readonly = true)
        {
            return @readonly
                ? _context.WsdlOutFaults.AsNoTracking()
                    .Include("SawsdlModelReferences")
                    .Include("WsdlOperation")
                    .Include("WsdlOperation.WsdlInterface")
                    .Include("WsdlOperation.WsdlInterface.ServiceDescription")
                    .Include(nameof(WsdlOutFault.Issues))
                    .FirstOrDefault(x => x.Id == idWsdlOutFault)
                : _context.WsdlOutFaults
                    .Include("SawsdlModelReferences")
                    .Include("WsdlOperation")
                    .Include("WsdlOperation.WsdlInterface")
                    .Include("WsdlOperation.WsdlInterface.ServiceDescription")
                    .Include(nameof(WsdlOutFault.Issues))
                    .FirstOrDefault(x => x.Id == idWsdlOutFault);
        }

        public WsdlOutput GetWsdlOutput(int idWsdlOutput, bool @readonly = true)
        {
            return @readonly
                ? _context.WsdlOutputs.AsNoTracking()
                    .Include("WsdlOperation")
                    .Include("WsdlOperation.WsdlInterface")
                    .Include("WsdlOperation.WsdlInterface.ServiceDescription")
                    .Include(nameof(WsdlOutput.Issues))
                    .FirstOrDefault(x => x.Id == idWsdlOutput)
                : _context.WsdlOutputs
                    .Include("WsdlOperation")
                    .Include("WsdlOperation.WsdlInterface")
                    .Include("WsdlOperation.WsdlInterface.ServiceDescription")
                    .Include(nameof(WsdlOutput.Issues))
                    .FirstOrDefault(x => x.Id == idWsdlOutput);
        }

        public XsdComplexElement GetXsdComplexElement(int idXsdComplexElement, bool @readonly = true)
        {
            return @readonly
                ? _context.XsdComplexElements.AsNoTracking()
                    .Include("SawsdlModelReferences")
                    .Include("XsdDocument")
                    .Include("XsdDocument.ServiceDescription")
                    .Include(nameof(XsdComplexElement.Issues))
                    .FirstOrDefault(x => x.Id == idXsdComplexElement)
                : _context.XsdComplexElements
                    .Include("SawsdlModelReferences")
                    .Include("XsdDocument")
                    .Include("XsdDocument.ServiceDescription")
                    .Include(nameof(XsdComplexElement.Issues))
                    .FirstOrDefault(x => x.Id == idXsdComplexElement);
        }

        public XsdElement GetXsdElement(int idXsdElement, bool @readonly = true)
        {
            return @readonly
                ? _context.XsdElements.AsNoTracking()
                    .Include("SawsdlModelReferences")
                    .Include("XsdComplexElement")
                    .Include("XsdComplexElement.XsdDocument")
                    .Include("XsdComplexElement.XsdDocument.ServiceDescription")
                    .Include(nameof(XsdElement.Issues))
                    .FirstOrDefault(x => x.Id == idXsdElement)
                : _context.XsdElements
                    .Include("SawsdlModelReferences")
                    .Include("XsdComplexElement")
                    .Include("XsdComplexElement.XsdDocument")
                    .Include("XsdComplexElement.XsdDocument.ServiceDescription")
                    .Include(nameof(XsdElement.Issues))
                    .FirstOrDefault(x => x.Id == idXsdElement);
        }

        public XsdSimpleElement GetXsdSimpleElement(int idXsdSimpleElement, bool @readonly = true)
        {
            return @readonly
                ? _context.XsdSimpleElements.AsNoTracking()
                    .Include("SawsdlModelReferences")
                    .Include("XsdDocument")
                    .Include("XsdDocument.ServiceDescription")
                    .Include(nameof(XsdSimpleElement.Issues))
                    .FirstOrDefault(x => x.Id == idXsdSimpleElement)
                : _context.XsdSimpleElements
                    .Include("SawsdlModelReferences")
                    .Include("XsdDocument")
                    .Include("XsdDocument.ServiceDescription")
                    .Include(nameof(XsdSimpleElement.Issues))
                    .FirstOrDefault(x => x.Id == idXsdSimpleElement);
        }

        public override void Remove(int id)
        {
            var serviceDescription = GetComplete(id, withSawsdlModelReferences: false, @readonly: false);

            //_context.WsdlInFaults.RemoveRange(serviceDescription.WsdlInterfaces.SelectMany(x => x.WsdlOperations).SelectMany(x => x.WsdlInFaults));
            _context.WsdlInputs.RemoveRange(serviceDescription.WsdlInterfaces.SelectMany(x => x.WsdlOperations).SelectMany(x => x.WsdlInputs));
            //_context.WsdlOutFaults.RemoveRange(serviceDescription.WsdlInterfaces.SelectMany(x => x.WsdlOperations).SelectMany(x => x.WsdlOutFaults));
            _context.WsdlOutputs.RemoveRange(serviceDescription.WsdlInterfaces.SelectMany(x => x.WsdlOperations).SelectMany(x => x.WsdlOutputs));
            _context.WsdlOperations.RemoveRange(serviceDescription.WsdlInterfaces.SelectMany(x => x.WsdlOperations));
            _context.WsdlInterfaces.RemoveRange(serviceDescription.WsdlInterfaces);

            _context.XsdElements.RemoveRange(serviceDescription.XsdDocument.XsdComplexElements.SelectMany(x => x.XsdElements));
            _context.XsdComplexElements.RemoveRange(serviceDescription.XsdDocument.XsdComplexElements);
            _context.XsdSimpleElements.RemoveRange(serviceDescription.XsdDocument.XsdSimpleElements);
            _context.XsdDocuments.Remove(serviceDescription.XsdDocument);

            _context.ServiceDescriptions.Remove(serviceDescription);
        }
    }
}