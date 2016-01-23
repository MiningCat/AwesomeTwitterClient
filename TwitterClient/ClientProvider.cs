using System;
using Castle.Facilities.TypedFactory;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using TwitterClient.Core.Facade;
using TwitterClient.Facade;

namespace TwitterClient
{
    public class ClientProvider : IClientProvider
    {
        private readonly WindsorContainer _container;

        public ClientProvider(ITwitterCredentials credentials)
        {
            if (credentials == null) throw new ArgumentNullException(nameof(credentials));

            _container = new WindsorContainer();
            _container.AddFacility<TypedFactoryFacility>();

            _container.Register(Component.For<ITwitterCredentials>().Instance(credentials));
            _container.Register(Classes.FromAssemblyInThisApplication().Pick().WithServiceAllInterfaces().LifestyleTransient());
        }

        public IClient GetClient()
        {
            return _container.Resolve<IClient>();
        }
    }
}
