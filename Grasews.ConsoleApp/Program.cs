using Grasews.Domain.Entities;
using Grasews.Infra.CrossCutting.Helpers;
using Grasews.Infra.Data.EF.Postgres.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Grasews.ConsoleApp
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //GetAllUsers();

            var pwd = CryptHelper.Decrypt("GmtSE8hwDaeBSKbmv2kacA==");
            pwd = CryptHelper.Encrypt("1qaz@WSX3edc");

            //var serviceDescription = CreateNewServiceDescription();

            //using (var serviceDescriptionRepository = new ServiceDescriptionEntityRepository())
            //{
            //    serviceDescriptionRepository.Create(serviceDescription);
            //    serviceDescriptionRepository.SaveChanges();
            //}
        }

        private static ServiceDescription CreateNewServiceDescription()
        {
            var serviceDescription = new ServiceDescription();

            serviceDescription.IdOwnerUser = 1;
            serviceDescription.ServiceName = "Test Service";
            serviceDescription.RegistrationDateTime = DateTime.Now;
            serviceDescription.Xml = "test";

            serviceDescription.XsdDocument = new XsdDocument
            {
                RegistrationDateTime = DateTime.Now,
                ServiceDescription = serviceDescription,
                XsdComplexTypes = new List<XsdComplexType>(),
                XsdSimpleTypes = new List<XsdSimpleType>()
            };

            var complexType1 = new XsdComplexType
            {
                XsdComplexTypeName = "Complex Type 1",
                RegistrationDateTime = DateTime.Now
            };

            var complexType2 = new XsdComplexType
            {
                XsdComplexTypeName = "Complex Type 2",
                RegistrationDateTime = DateTime.Now
            };

            var complexType3 = new XsdComplexType
            {
                XsdComplexTypeName = "Complex Type 3",
                RegistrationDateTime = DateTime.Now
            };

            var complexType4 = new XsdComplexType
            {
                XsdComplexTypeName = "Complex Type 4",
                RegistrationDateTime = DateTime.Now
            };

            complexType1.ChildrenXsdComplexTypes = new List<XsdComplexType> { complexType2 };
            //complexType2.ParentsXsdComplexType = new List<XsdComplexType> { complexType1 };

            complexType2.ChildrenXsdComplexTypes = new List<XsdComplexType> { complexType3, complexType4 };
            //complexType3.ParentsXsdComplexType = new List<XsdComplexType> { complexType2 };
            //complexType4.ParentsXsdComplexType = new List<XsdComplexType> { complexType2 };

            var simpleType1 = new XsdSimpleType
            {
                XsdSimpleTypeName = "Simple Type 1",
                RegistrationDateTime = DateTime.Now
            };

            var simpleType2 = new XsdSimpleType
            {
                XsdSimpleTypeName = "Simple Type 2",
                RegistrationDateTime = DateTime.Now
            };

            var simpleType3 = new XsdSimpleType
            {
                XsdSimpleTypeName = "Simple Type 3",
                RegistrationDateTime = DateTime.Now
            };

            complexType2.XsdSimpleTypes = new List<XsdSimpleType> { simpleType1 };
            complexType3.XsdSimpleTypes = new List<XsdSimpleType> { simpleType2 };
            complexType4.XsdSimpleTypes = new List<XsdSimpleType> { simpleType3 };

            serviceDescription.XsdDocument.XsdComplexTypes.Add(complexType1);
            serviceDescription.XsdDocument.XsdComplexTypes.Add(complexType2);
            serviceDescription.XsdDocument.XsdComplexTypes.Add(complexType3);
            serviceDescription.XsdDocument.XsdComplexTypes.Add(complexType4);

            var wsdlInterface = new WsdlInterface { WsdlInterfaceName = "Interface 1" };

            serviceDescription.WsdlInterfaces = new List<WsdlInterface> { wsdlInterface };

            return serviceDescription;
        }

        private static void GetAllUsers()
        {
            using (var userRepository = new UserEntityRepository())
            {
                var users = userRepository.GetAll().ToList();
            }
        }
    }
}