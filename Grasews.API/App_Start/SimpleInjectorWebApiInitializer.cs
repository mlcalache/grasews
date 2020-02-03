namespace Grasews.API
{
    using Grasews.API.AutoMapper;
    using Grasews.Infra.CrossCutting.IoC;
    using Microsoft.Practices.ServiceLocation;
    using Owin;
    using SimpleInjector;
    using SimpleInjector.Integration.WebApi;
    using SimpleInjector.Lifestyles;
    using System.Web.Http;

    /// <summary>
    ///
    /// </summary>
    public static class SimpleInjectorWebApiInitializer
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="app"></param>
        public static void Initialize(IAppBuilder app)
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

            SimpleInjectorBootstrap.RegistersDependencies(container);

            container.RegisterWebApiControllers(GlobalConfiguration.Configuration);

            ServiceLocator.SetLocatorProvider(() => new SimpleInjectorServiceLocator(container));

            var mapper = AutoMapperConfig.CreateMapperConfiguration();

            container.RegisterInstance(mapper);

            container.Register(() => mapper.CreateMapper(container.GetInstance), Lifestyle.Scoped);

            container.Verify();

            GlobalConfiguration.Configuration.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);

            app.Use(async (context, next) =>
            {
                using (AsyncScopedLifestyle.BeginScope(container))
                {
                    await next?.Invoke();
                }
            });
        }
    }
}