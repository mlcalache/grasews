using Grasews.Application.Services;
using Grasews.Domain.Events;
using Grasews.Domain.Interfaces.Containers;
using Grasews.Domain.Interfaces.Events;
using Grasews.Domain.Interfaces.Repositories;
using Grasews.Domain.Interfaces.Services;
using Grasews.Infra.CrossCutting.Email;
using Grasews.Infra.CrossCutting.Helpers.RestClient;
using Grasews.Infra.CrossCutting.Helpers.RestClient.Interfaces;
using Grasews.Infra.CrossCutting.Security;
using Grasews.Infra.Data.EF.Postgres.Contexts;
using Grasews.Infra.Data.EF.Postgres.Repositories;
using Grasews.Infra.ExternalService.Cytoscape.Services;
using Grasews.Infra.ExternalService.Gmail;
using SimpleInjector;

namespace Grasews.Infra.CrossCutting.IoC
{
    public static class SimpleInjectorBootstrap
    {
        public static void RegistersDependencies(Container container)
        {
            #region Events

            container.Register<IDomainContainer, DomainContainer>(Lifestyle.Scoped);
            container.Register<IEventDispatcher, EventDispatcher>(Lifestyle.Scoped);
            container.Collection.Register(typeof(IEventHandler<>), typeof(IEventHandler<>).Assembly);

            #endregion Events

            #region Repositories

            container.Register<IOntologyEntityRepository, OntologyEntityRepository>(Lifestyle.Scoped);
            container.Register<IOntologyTermEntityRepository, OntologyTermEntityRepository>(Lifestyle.Scoped);
            container.Register<IServiceDescription_OntologyEntityRepository, ServiceDescription_OntologyEntityRepository>(Lifestyle.Scoped);
            container.Register<IServiceDescriptionEntityRepository, ServiceDescriptionEntityRepository>(Lifestyle.Scoped);

            container.Register<ITaskCommentEntityRepository, TaskCommentEntityRepository>(Lifestyle.Scoped);
            container.Register<ITaskEntityRepository, TaskEntityRepository>(Lifestyle.Scoped);
            container.Register<IIssueAnswerEntityRepository, IssueAnswerEntityRepository>(Lifestyle.Scoped);
            container.Register<IIssueEntityRepository, IssueEntityRepository>(Lifestyle.Scoped);

            container.Register<IUserEntityRepository, UserEntityRepository>(Lifestyle.Scoped);
            container.Register<IShareInvitationEntityRepository, ShareInvitationEntityRepository>(Lifestyle.Scoped);
            container.Register<IShareInvitation_OntologyEntityRepository, ShareInvitation_OntologyEntityRepository>(Lifestyle.Scoped);
            container.Register<IServiceDescription_UserEntityRepository, ServiceDescription_UserEntityRepository>(Lifestyle.Scoped);
            container.Register<IOntology_UserEntityRepository, Ontology_UserEntityRepository>(Lifestyle.Scoped);
            container.Register<IResetPasswordEntityRepository, ResetPasswordEntityRepository>(Lifestyle.Scoped);

            container.Register<IWsdlInterfaceEntityRepository, WsdlInterfaceRepository>(Lifestyle.Scoped);
            container.Register<IWsdlOperationEntityRepository, WsdlOperationRepository>(Lifestyle.Scoped);
            container.Register<IWsdlInputEntityRepository, WsdlInputRepository>(Lifestyle.Scoped);
            container.Register<IWsdlOutputEntityRepository, WsdlOutputRepository>(Lifestyle.Scoped);
            container.Register<IWsdlFaultEntityRepository, WsdlFaultRepository>(Lifestyle.Scoped);
            container.Register<IWsdlInfaultEntityRepository, WsdlInfaultRepository>(Lifestyle.Scoped);
            container.Register<IWsdlOutfaultEntityRepository, WsdlOutfaultRepository>(Lifestyle.Scoped);

            container.Register<IXsdComplexTypeEntityRepository, XsdComplexElementRepository>(Lifestyle.Scoped);
            container.Register<IXsdSimpleTypeEntityRepository, XsdSimpleElementRepository>(Lifestyle.Scoped);

            container.Register<ISawsdlModelReferenceEntityRepository, SawsdlModelReferenceEntityRepository>(Lifestyle.Scoped);

            container.Register<IGraphNodePosition_OntologyTermRepository, GraphNodePosition_OntologyTermRepository>(Lifestyle.Scoped);
            container.Register<IGraphNodePosition_SawsdlModelReferenceRepository, GraphNodePosition_SawsdlModelReferenceRepository>(Lifestyle.Scoped);

            #endregion Repositories

            #region Services

            container.Register<IIssueAnswerService, IssueAnswerService>(Lifestyle.Scoped);
            container.Register<IIssueService, IssueService>(Lifestyle.Scoped);
            container.Register<IOntologyService, OntologyService>(Lifestyle.Scoped);
            container.Register<IOntology_UserService, Ontology_UserService>(Lifestyle.Scoped);
            container.Register<IServiceDescription_UserService, ServiceDescription_UserService>(Lifestyle.Scoped);
            container.Register<IServiceDescription_OntologyService, ServiceDescription_OntologyService>(Lifestyle.Scoped);
            container.Register<IServiceDescriptionService, ServiceDescriptionService>(Lifestyle.Scoped);
            container.Register<IShareInvitationService, ShareInvitationService>(Lifestyle.Scoped);
            container.Register<ITaskCommentService, TaskCommentService>(Lifestyle.Scoped);
            container.Register<ITaskService, TaskService>(Lifestyle.Scoped);
            container.Register<IUserService, UserService>(Lifestyle.Scoped);
            container.Register<IUserIdentityService, UserIdentityService>(Lifestyle.Scoped);
            container.Register<ISawsdlService, SawsdlService>(Lifestyle.Scoped);
            container.Register<IEmailMessageService, EmailMessageService>(Lifestyle.Scoped);

            #endregion Services

            #region External Services

            container.Register<IGraphService, CytoscapeService>(Lifestyle.Scoped);
            container.Register<IEmailSenderService, GmailService>(Lifestyle.Scoped);

            #endregion External Services

            #region Db Contexts

            container.Register<GrasewsContext>(Lifestyle.Scoped);

            #endregion Db Contexts

            #region Helpers

            container.Register<IAPIRestClient, APIRestClient>(Lifestyle.Scoped);

            #endregion Helpers
        }
    }
}