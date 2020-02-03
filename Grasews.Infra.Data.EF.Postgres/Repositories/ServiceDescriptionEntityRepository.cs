using Grasews.Domain.Entities;
using Grasews.Domain.Interfaces.Repositories;
using Npgsql;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace Grasews.Infra.Data.EF.Postgres.Repositories
{
    public class ServiceDescriptionEntityRepository : BaseEntityRepository<ServiceDescription, int>, IServiceDescriptionEntityRepository
    {
        #region IServiceDescriptionEntityRepository methods

        public ServiceDescription GetComplete(int id, bool withSawsdlModelReferences = false, bool @readonly = true)
        {
            var serviceDescription = base.Get(id, @readonly);

            //var query = base.GetAll(@readonly)
            //    .Include(svc => svc.WsdlInterfaces)
            //    .Include(svc => svc.WsdlInterfaces.Select(i => i.WsdlOperations))
            //    .Include(svc => svc.WsdlInterfaces.Select(i => i.WsdlOperations.Select(op => op.WsdlInputs)))
            //    .Include(svc => svc.WsdlInterfaces.Select(i => i.WsdlOperations.Select(op => op.WsdlOutputs)));

            //query = query
            //    .Include(svc => svc.WsdlInterfaces.Select(i => i.WsdlOperations.Select(op => op.WsdlInputs.Select(input => input.XsdComplexType))))
            //    .Include(svc => svc.WsdlInterfaces.Select(i => i.WsdlOperations.Select(op => op.WsdlInputs.Select(input => input.XsdComplexType.ChildrenXsdComplexTypes))))
            //    .Include(svc => svc.WsdlInterfaces.Select(i => i.WsdlOperations.Select(op => op.WsdlInputs.Select(input => input.XsdComplexType.ChildrenXsdComplexTypes.Select(ct => ct.ChildrenXsdComplexTypes)))))
            //    .Include(svc => svc.WsdlInterfaces.Select(i => i.WsdlOperations.Select(op => op.WsdlOutputs.Select(output => output.XsdComplexType))))
            //    .Include(svc => svc.WsdlInterfaces.Select(i => i.WsdlOperations.Select(op => op.WsdlOutputs.Select(output => output.XsdComplexType.ChildrenXsdComplexTypes))))
            //    .Include(svc => svc.WsdlInterfaces.Select(i => i.WsdlOperations.Select(op => op.WsdlOutputs.Select(output => output.XsdComplexType.ChildrenXsdComplexTypes.Select(ct => ct.ChildrenXsdComplexTypes)))));

            //query = query
            //    .Include(svc => svc.XsdDocument)
            //    .Include(svc => svc.XsdDocument.XsdComplexTypes)
            //    .Include(svc => svc.XsdDocument.XsdComplexTypes.Select(ct => ct.ChildrenXsdComplexTypes))
            //    .Include(svc => svc.XsdDocument.XsdSimpleTypes);

            //if (withSawsdlModelReferences)
            //{
            //    query = query.Include(svc => svc.WsdlInterfaces.Select(i => i.SawsdlModelReferences))
            //        .Include(svc => svc.WsdlInterfaces.Select(i => i.WsdlOperations.Select(op => op.SawsdlModelReferences)))
            //        .Include(svc => svc.XsdDocument.XsdComplexTypes.Select(ct => ct.SawsdlModelReferences))
            //        .Include(svc => svc.XsdDocument.XsdSimpleTypes.Select(st => st.SawsdlModelReferences));
            //}

            //var serviceDescription = query.FirstOrDefault(x => x.Id == id);

            return serviceDescription;
        }

        public ServiceDescription GetWithOntologies(int id, bool @readonly = true)
        {
            var query = base.GetAll(@readonly)
                 .Include(nameof(ServiceDescription.ServiceDescription_Ontologies));

            var serviceDescription = query.FirstOrDefault(x => x.Id == id);

            return serviceDescription;
        }

        public WsdlFault GetWsdlFault(int idWsdlFault, bool @readonly = true)
        {
            var baseQuery = @readonly ? _context.WsdlFaults.AsNoTracking() : _context.WsdlFaults;

            var query = baseQuery
                .Include(nameof(WsdlFault.SawsdlModelReferences))
                .Include(nameof(WsdlFault.WsdlInterface))
                .FirstOrDefault(x => x.Id == idWsdlFault);

            return query;
        }

        public WsdlInfault GetWsdlInfault(int idWsdlInfault, bool @readonly = true)
        {
            var baseQuery = @readonly ? _context.WsdlInfaults.AsNoTracking() : _context.WsdlInfaults;

            var query = baseQuery
                .Include(nameof(WsdlInfault.WsdlOperation))
                .Include($"{nameof(WsdlInfault.WsdlOperation)}{nameof(WsdlInfault.WsdlOperation.WsdlInterface)}")
                .FirstOrDefault(x => x.Id == idWsdlInfault);

            return query;

            //return @readonly
            //    ? _context.WsdlInfaults.AsNoTracking()
            //        .Include(nameof(WsdlInfault.SawsdlModelReferences))
            //        .Include(nameof(WsdlInfault.WsdlOperation))
            //        .Include($"{nameof(WsdlInfault.WsdlOperation)}{nameof(WsdlInfault.WsdlOperation.WsdlInterface)}")
            //        .Include(nameof(WsdlInfault.Issues))
            //        .FirstOrDefault(x => x.Id == idWsdlInfault)
            //    : _context.WsdlInfaults
            //        .Include(nameof(WsdlInfault.SawsdlModelReferences))
            //        .Include(nameof(WsdlInfault.WsdlOperation))
            //        .Include($"{nameof(WsdlInfault.WsdlOperation)}{nameof(WsdlInfault.WsdlOperation.WsdlInterface)}")
            //        .Include(nameof(WsdlInfault.Issues))
            //        .FirstOrDefault(x => x.Id == idWsdlInfault);
        }

        public WsdlInput GetWsdlInput(int idWsdlInput, bool @readonly = true)
        {
            var baseQuery = @readonly ? _context.WsdlInputs.AsNoTracking() : _context.WsdlInputs;

            var query = baseQuery
                .Include(nameof(WsdlInput.WsdlOperation))
                .Include($"{nameof(WsdlInput.WsdlOperation)}.{nameof(WsdlInput.WsdlOperation.WsdlInterface)}")
                .Include($"{nameof(WsdlInput.WsdlOperation)}.{nameof(WsdlInput.WsdlOperation.WsdlInterface)}.{nameof(WsdlInput.WsdlOperation.WsdlInterface.ServiceDescription)}")
                .FirstOrDefault(x => x.Id == idWsdlInput);

            return query;

            //return @readonly
            //    ? _context.WsdlInputs.AsNoTracking()
            //        .Include(nameof(WsdlInput.WsdlOperation))
            //        .Include($"{nameof(WsdlInput.WsdlOperation)}.{nameof(WsdlInput.WsdlOperation.WsdlInterface)}")
            //        .Include($"{nameof(WsdlInput.WsdlOperation)}.{nameof(WsdlInput.WsdlOperation.WsdlInterface)}.{nameof(WsdlInput.WsdlOperation.WsdlInterface.ServiceDescription)}")
            //        .Include(nameof(WsdlInput.Issues))
            //        .FirstOrDefault(x => x.Id == idWsdlInput)
            //    : _context.WsdlInputs
            //        .Include(nameof(WsdlInput.WsdlOperation))
            //        .Include($"{nameof(WsdlInput.WsdlOperation)}.{nameof(WsdlInput.WsdlOperation.WsdlInterface)}")
            //        .Include($"{nameof(WsdlInput.WsdlOperation)}.{nameof(WsdlInput.WsdlOperation.WsdlInterface)}.{nameof(WsdlInput.WsdlOperation.WsdlInterface.ServiceDescription)}")
            //        .Include(nameof(WsdlInput.Issues))
            //        .FirstOrDefault(x => x.Id == idWsdlInput);
        }

        public WsdlInterface GetWsdlInterface(int idWsdlInterface, bool @readonly = true)
        {
            var baseQuery = @readonly ? _context.WsdlInterfaces.AsNoTracking() : _context.WsdlInterfaces;

            var query = baseQuery
                .Include(nameof(WsdlInterface.SawsdlModelReferences))
                .Include(nameof(WsdlInterface.ServiceDescription))
                .Include(nameof(WsdlInterface.Issues))
                .FirstOrDefault(x => x.Id == idWsdlInterface);

            return query;

            //return @readonly
            //    ? _context.WsdlInterfaces.AsNoTracking()
            //        .Include(nameof(WsdlInterface.SawsdlModelReferences))
            //        .Include(nameof(WsdlInterface.ServiceDescription))
            //        .Include(nameof(WsdlInterface.Issues))
            //        .FirstOrDefault(x => x.Id == idWsdlInterface)
            //    : _context.WsdlInterfaces
            //        .Include(nameof(WsdlInterface.SawsdlModelReferences))
            //        .Include(nameof(WsdlInterface.ServiceDescription))
            //        .Include(nameof(WsdlInterface.Issues))
            //        .FirstOrDefault(x => x.Id == idWsdlInterface);
        }

        public WsdlOperation GetWsdlOperation(int idWsdlOperation, bool @readonly = true)
        {
            var baseQuery = @readonly ? _context.WsdlOperations.AsNoTracking() : _context.WsdlOperations;

            var query = baseQuery
                .Include(nameof(WsdlOperation.SawsdlModelReferences))
                .Include(nameof(WsdlOperation.WsdlInterface))
                .Include($"{nameof(WsdlOperation.WsdlInterface)}.{nameof(WsdlOperation.WsdlInterface.ServiceDescription)}")
                .Include(nameof(WsdlOperation.Issues))
                .FirstOrDefault(x => x.Id == idWsdlOperation);

            return query;

            //return @readonly
            //    ? _context.WsdlOperations.AsNoTracking()
            //        .Include(nameof(WsdlOperation.SawsdlModelReferences))
            //        .Include(nameof(WsdlOperation.WsdlInterface))
            //        .Include($"{nameof(WsdlOperation.WsdlInterface)}.{nameof(WsdlOperation.WsdlInterface.ServiceDescription)}")
            //        .Include(nameof(WsdlOperation.Issues))
            //        .FirstOrDefault(x => x.Id == idWsdlOperation)
            //    : _context.WsdlOperations
            //        .Include(nameof(WsdlOperation.SawsdlModelReferences))
            //        .Include(nameof(WsdlOperation.WsdlInterface))
            //        .Include($"{nameof(WsdlOperation.WsdlInterface)}.{nameof(WsdlOperation.WsdlInterface.ServiceDescription)}")
            //        .Include(nameof(WsdlOperation.Issues))
            //        .FirstOrDefault(x => x.Id == idWsdlOperation);
        }

        public WsdlOutfault GetWsdlOutfault(int idWsdlOutfault, bool @readonly = true)
        {
            var baseQuery = @readonly ? _context.WsdlOutfaults.AsNoTracking() : _context.WsdlOutfaults;

            var query = baseQuery
                .Include(nameof(WsdlOutfault.WsdlOperation))
                .Include($"{nameof(WsdlOutfault.WsdlOperation)}.{nameof(WsdlOperation.WsdlInterface)}")
                .Include($"{nameof(WsdlOutfault.WsdlOperation)}.{nameof(WsdlOperation.WsdlInterface)}.{nameof(WsdlInterface.ServiceDescription)}")
                .FirstOrDefault(x => x.Id == idWsdlOutfault);

            return query;

            //return @readonly
            //    ? _context.WsdlOutfaults.AsNoTracking()
            //        .Include($"{nameof(WsdlOutfault.SawsdlModelReferences)}")
            //        .Include($"{nameof(WsdlOutfault.WsdlOperation)}")
            //        .Include($"WsdlOperation.WsdlInterface")
            //        .Include($"WsdlOperation.WsdlInterface.ServiceDescription")
            //        .Include($"{nameof(WsdlOutfault.Issues)}")
            //        .FirstOrDefault(x => x.Id == idWsdlOutfault)
            //    : _context.WsdlOutfaults
            //        .Include("SawsdlModelReferences")
            //        .Include("WsdlOperation")
            //        .Include("WsdlOperation.WsdlInterface")
            //        .Include("WsdlOperation.WsdlInterface.ServiceDescription")
            //        .Include(nameof(WsdlOutfault.Issues))
            //        .FirstOrDefault(x => x.Id == idWsdlOutfault);
        }

        public WsdlOutput GetWsdlOutput(int idWsdlOutput, bool @readonly = true)
        {
            var baseQuery = @readonly ? _context.WsdlOutputs.AsNoTracking() : _context.WsdlOutputs;

            var query = baseQuery
                .Include(nameof(WsdlOperation))
                .Include(nameof(WsdlOperation.WsdlInterface))
                .Include(nameof(WsdlInterface.ServiceDescription))
                .FirstOrDefault(x => x.Id == idWsdlOutput);

            return query;

            //return @readonly
            //    ? _context.WsdlOutputs.AsNoTracking()
            //        .Include("WsdlOperation")
            //        .Include("WsdlOperation.WsdlInterface")
            //        .Include("WsdlOperation.WsdlInterface.ServiceDescription")
            //        .Include(nameof(WsdlOutput.Issues))
            //        .FirstOrDefault(x => x.Id == idWsdlOutput)
            //    : _context.WsdlOutputs
            //        .Include("WsdlOperation")
            //        .Include("WsdlOperation.WsdlInterface")
            //        .Include("WsdlOperation.WsdlInterface.ServiceDescription")
            //        .Include(nameof(WsdlOutput.Issues))
            //        .FirstOrDefault(x => x.Id == idWsdlOutput);
        }

        public XsdComplexType GetXsdComplexType(int idXsdComplexElement, bool @readonly = true)
        {
            var baseQuery = @readonly ? _context.XsdComplexTypes.AsNoTracking() : _context.XsdComplexTypes;

            var query = baseQuery
                .Include(nameof(XsdComplexType.SawsdlModelReferences))
                .Include(nameof(XsdDocument))
                .Include($"{nameof(XsdDocument)}.{nameof(XsdDocument.ServiceDescription)}")
                .Include(nameof(XsdComplexType.Issues))
                .FirstOrDefault(x => x.Id == idXsdComplexElement);

            return query;

            //return @readonly
            //    ? _context.XsdComplexTypes.AsNoTracking()
            //        .Include("SawsdlModelReferences")
            //        .Include("XsdDocument")
            //        .Include("XsdDocument.ServiceDescription")
            //        .Include(nameof(XsdComplexType.Issues))
            //        .FirstOrDefault(x => x.Id == idXsdComplexElement)
            //    : _context.XsdComplexTypes
            //        .Include("SawsdlModelReferences")
            //        .Include("XsdDocument")
            //        .Include("XsdDocument.ServiceDescription")
            //        .Include(nameof(XsdComplexType.Issues))
            //        .FirstOrDefault(x => x.Id == idXsdComplexElement);
        }

        public XsdSimpleType GetXsdSimpleType(int idXsdSimpleElement, bool @readonly = true)
        {
            var baseQuery = @readonly ? _context.XsdSimpleTypes.AsNoTracking() : _context.XsdSimpleTypes;

            var query = baseQuery
                .Include(nameof(XsdSimpleType.SawsdlModelReferences))
                .Include(nameof(XsdSimpleType.XsdComplexType))
                .Include(nameof(XsdSimpleType.XsdDocument))
                .Include($"{nameof(XsdSimpleType.XsdDocument)}.{nameof(XsdDocument.ServiceDescription)}")
                .Include(nameof(XsdSimpleType.Issues))
                .FirstOrDefault(x => x.Id == idXsdSimpleElement);

            return query;

            //return @readonly
            //    ? _context.XsdSimpleTypes.AsNoTracking()
            //        .Include("SawsdlModelReferences")
            //        .Include("XsdDocument")
            //        .Include("XsdDocument.ServiceDescription")
            //        .Include(nameof(XsdSimpleType.Issues))
            //        .FirstOrDefault(x => x.Id == idXsdSimpleElement)
            //    : _context.XsdSimpleTypes
            //        .Include("SawsdlModelReferences")
            //        .Include("XsdDocument")
            //        .Include("XsdDocument.ServiceDescription")
            //        .Include(nameof(XsdSimpleType.Issues))
            //        .FirstOrDefault(x => x.Id == idXsdSimpleElement);
        }

        public void RemoveXsdComplexTypeSelfReferences(int id)
        {
            var sqlDeleteStatement = new StringBuilder();

            sqlDeleteStatement.AppendLine(" DELETE ");
            sqlDeleteStatement.AppendLine(" FROM public.\"XsdComplexComplex\" ctct ");
            sqlDeleteStatement.AppendLine(" USING public.\"XsdComplexType\" ct, public.\"XsdDocument\" d, public.\"ServiceDescription\" sd ");
            sqlDeleteStatement.AppendLine(" WHERE ct.\"Id\" = ctct.\"IdParent\" ");
            sqlDeleteStatement.AppendLine(" AND d.\"Id\" = ct.\"IdXsdDocument\" ");
            sqlDeleteStatement.AppendLine(" AND sd.\"Id\" = d.\"Id\" ");
            sqlDeleteStatement.AppendLine(" AND sd.\"Id\" = :IdServiceDescription ");

            //var parameterList = new List<NpgsqlParameter>
            //{
            //    new NpgsqlParameter(":IdServiceDescription", id)
            //};

            _context.Database.ExecuteSqlCommand(sqlDeleteStatement.ToString(), new NpgsqlParameter(":IdServiceDescription", id));
        }

        #endregion IServiceDescriptionEntityRepository methods

        #region Overrides

        //public override void Remove(int id)
        //{
        //    var serviceDescription = GetComplete(id, withSawsdlModelReferences: false, @readonly: false);

        //    ////_context.WsdlInfaults.RemoveRange(serviceDescription.WsdlInterfaces.SelectMany(x => x.WsdlOperations).SelectMany(x => x.WsdlInfaults));
        //    //_context.WsdlInputs.RemoveRange(serviceDescription.WsdlInterfaces.SelectMany(x => x.WsdlOperations).SelectMany(x => x.WsdlInputs));
        //    ////_context.WsdlOutfaults.RemoveRange(serviceDescription.WsdlInterfaces.SelectMany(x => x.WsdlOperations).SelectMany(x => x.WsdlOutfaults));
        //    //_context.WsdlOutputs.RemoveRange(serviceDescription.WsdlInterfaces.SelectMany(x => x.WsdlOperations).SelectMany(x => x.WsdlOutputs));
        //    //_context.WsdlOperations.RemoveRange(serviceDescription.WsdlInterfaces.SelectMany(x => x.WsdlOperations));
        //    //_context.WsdlInterfaces.RemoveRange(serviceDescription.WsdlInterfaces);

        //    //_context.XsdComplexTypes.RemoveRange(serviceDescription.XsdDocument.XsdComplexTypes);
        //    //_context.XsdSimpleTypes.RemoveRange(serviceDescription.XsdDocument.XsdSimpleTypes);
        //    //_context.XsdDocuments.Remove(serviceDescription.XsdDocument);

        //    _context.ServiceDescriptions.Remove(serviceDescription);
        //}

        #endregion Overrides
    }
}