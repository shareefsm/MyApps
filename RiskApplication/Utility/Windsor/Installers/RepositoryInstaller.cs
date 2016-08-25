using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Repositories.Interfaces;

namespace RiskApplication.Utility.Windsor.Installers
{
    public class RepositoryInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                 Classes
                     .FromAssemblyContaining(typeof(IRiskRepository))
                     .BasedOn<IRiskRepository>()
                     .WithService
                     .DefaultInterfaces()
                     .Configure(x => x.Named(x.Implementation.Name)));
        }
    }
}