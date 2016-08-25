using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Castle.MicroKernel;

namespace RiskApplication.Utility.Windsor
{
    public class WindsorControllerFactory : DefaultControllerFactory
    {
        private readonly IKernel kernel;

        public WindsorControllerFactory(IKernel kernel)
        {
            this.kernel = kernel;
        }

        public override void ReleaseController(IController controller)
        {
            kernel.ReleaseComponent(controller);
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            if (controllerType == null)
            {
                throw new HttpException(404, string.Format("The controller for path '{0}' could not be found.", requestContext.HttpContext.Request.Path));
            }

            {
                var controller = (IController)kernel.Resolve(controllerType);

                //Inject the action invoker
                if (controller != null && kernel.HasComponent(typeof(IActionInvoker)))
                {
                    ((Controller)controller).ActionInvoker = kernel.Resolve<IActionInvoker>();
                }

                return controller;
            }
        }

    }
}