namespace Functions.Configs
{
    using Autofac;
    using AutoMapper;
    using AzureFunctions.Autofac.Configuration;
    using Controllers.Profiles;
    using Core.Commands;
    using MediatR;
    using System.Collections.Generic;
    using System.Reflection;

    public class DIConfig
    {
        public DIConfig(string functionName)
        {
            DependencyInjection.Initialize(builder =>
            {

                RegisterMediatR(builder);
                RegisterAutoMapper(builder);

            }, functionName);
        }

        private void RegisterMediatR(ContainerBuilder builder)
        {
            builder
              .RegisterType<Mediator>()
              .As<IMediator>()
              .InstancePerLifetimeScope();

            builder.Register<ServiceFactory>(context =>
            {
                var c = context.Resolve<IComponentContext>();
                return t => c.Resolve(t);
            });

            builder.RegisterAssemblyTypes(typeof(AddPlantHandler).GetTypeInfo().Assembly).AsImplementedInterfaces();
        }

        private void RegisterAutoMapper(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes().AssignableTo(typeof(PlantProfile));

            builder.Register(c => new MapperConfiguration(cfg =>
            {
                foreach (var profile in c.Resolve<IEnumerable<Profile>>())
                {
                    cfg.AddProfile(profile);
                }
            })).AsSelf().SingleInstance();

            builder.Register(c => c.Resolve<MapperConfiguration>().CreateMapper(c.Resolve)).As<IMapper>().InstancePerLifetimeScope();
        }
    }
}
