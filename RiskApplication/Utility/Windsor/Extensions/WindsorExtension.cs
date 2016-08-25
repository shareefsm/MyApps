using System;
using System.Reflection;
using Castle.Core;
using Castle.MicroKernel;

namespace RiskApplication.Utility.Windsor.Extensions
{
    public static class WindsorExtension
    {
        public static void InjectProperties(this IKernel kernel, object target)
        {
            var type = target.GetType();
            foreach (var property in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                if (property.PropertyType == typeof(IKernel))
                {
                    property.SetValue(target, kernel, null);
                    continue;
                }

                if (property.CanWrite && kernel.HasComponent(property.PropertyType))
                {
                    var value = kernel.Resolve(property.PropertyType);
                    try
                    {
                        property.SetValue(target, value, null);
                    }
                    catch (Exception ex)
                    {
                        var message = string.Format("Error setting property {0} on type {1}, See inner exception for more information.", property.Name, type.FullName);
                        throw new ComponentRegistrationException(message, ex);
                    }
                }
            }
        }

        public static void Kernel_ComponentModelCreated(ComponentModel model)
        {

            if (model.LifestyleType == LifestyleType.Undefined && model.Name.EndsWith("Controller"))
                model.LifestyleType = LifestyleType.Transient;
            else if (model.LifestyleType == LifestyleType.Undefined)
                model.LifestyleType = LifestyleType.PerWebRequest;

        }

    }
}