using Autofac;
using ChatSignalR.Services;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace ChatSignalR
{
    public class Bootstrapper
    {
        public static void Init()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<ChatService>().As<IChatService>().SingleInstance();

            var currentAssembly = Assembly.GetExecutingAssembly();

            builder.RegisterAssemblyTypes(currentAssembly).Where(x => x.Name.EndsWith("View", StringComparison.Ordinal));

            builder.RegisterAssemblyTypes(currentAssembly).Where(x => x.Name.EndsWith("ViewModel", StringComparison.Ordinal));

            var container = builder.Build();

            Resolver.Initialize(container);
        }
    }
}
