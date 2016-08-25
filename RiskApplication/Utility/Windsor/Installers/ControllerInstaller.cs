using System.Web.Mvc;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using RiskApplication.Controllers;

namespace RiskApplication.Utility.Windsor.Installers
{
    public class ControllerInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(AllTypes.FromThisAssembly()
                                   .BasedOn<IController>()
                                   .If(Component.IsInSameNamespaceAs<RiskController>())
                                   .If(t => t.Name.EndsWith("Controller"))
                                   .Configure(c => c.IsDefault()));
        }
    }
}