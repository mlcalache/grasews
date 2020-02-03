using Grasews.Infra.CrossCutting.IoC;
using Grasews.Web.App_Start;
using Grasews.Web.AutoMapper;
using Microsoft.Owin;
using Microsoft.Practices.ServiceLocation;
using SimpleInjector;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.Web.Mvc;
using System.Reflection;
using System.Web.Mvc;

[assembly: OwinStartup(typeof(Startup))]

namespace Grasews.Web.App_Start
{
    public class SimpleInjectorInitializer
    {
        public static void Initialize()
        {
            var container = new Container();

            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();

            SimpleInjectorBootstrap.RegistersDependencies(container);

            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());

            ServiceLocator.SetLocatorProvider(() => new SimpleInjectorServiceLocator(container));

            var mapper = AutoMapperConfig.CreateMapperConfiguration();

            container.RegisterInstance(mapper);

            container.Register(() => mapper.CreateMapper(container.GetInstance), Lifestyle.Scoped);

            container.Verify();

            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }
    }
}