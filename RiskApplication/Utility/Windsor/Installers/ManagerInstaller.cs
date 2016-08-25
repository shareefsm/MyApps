using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using RiskApplication.Managers.Implementations;
using RiskApplication.Managers.Interfaces;

namespace RiskApplication.Utility.Windsor.Installers
{
    public class WebMvcManagerInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Classes.FromAssemblyContaining<RiskManager>()
                    .BasedOn<IRiskManager>()
                    .WithService.DefaultInterfaces()
                    .Configure(x => x.Named(x.Implementation.Name)));

        }
    }
}
