using Autofac;
using C.O.S.E.C.Domain.InterfaceDrivers;
using System;
using System.Linq;
using System.Reflection;

namespace C.O.S.E.C.Api.Models
{
    public class AutofacModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var baseType = typeof(IBaseBLL);
            var assemblyName = Assembly.GetAssembly(typeof(IBaseBLL)).GetName().Name;
            var assemblies = Assembly.Load(assemblyName);
            builder.RegisterAssemblyTypes(assemblies)
                .Where(t => t.Name.EndsWith("BLL", StringComparison.CurrentCulture) && baseType.IsAssignableFrom(t) && !t.GetTypeInfo().IsAbstract)
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            var assemblies2 = Assembly.Load("C.O.S.E.C.Infrastructure.Repository");
            builder.RegisterAssemblyTypes(assemblies2)
                .Where(t => t.Name.EndsWith("BLL", StringComparison.CurrentCulture) && baseType.IsAssignableFrom(t) && !t.GetTypeInfo().IsAbstract)
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }
    }
}
