namespace Functions.Configs
{
    using Autofac;
    using AutoMapper;
    using AzureFunctions.Autofac.Configuration;
    using Controllers.Profiles;
    using Core;
    using Core.Commands;
    using MediatR;
    using Microsoft.Azure.Documents.Client;
    using System;
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

                RegisterCosmosDb(builder);

                RegisterSettings(builder);

            }, functionName);
        }

        private void RegisterSettings(ContainerBuilder builder)
        {
            var settings = new Settings();
     
            builder.RegisterInstance<ISettings>(settings);
        }

        private void RegisterCosmosDb(ContainerBuilder builder)
        {
            var cosmosEndpoint = Environment.GetEnvironmentVariable("CosmosEndpoint");
            var cosmosAuthorizationKey = Environment.GetEnvironmentVariable("CosmosAuthorizationKey");
            var documentClient = new DocumentClient(new Uri(cosmosEndpoint), cosmosAuthorizationKey, new ConnectionPolicy()
            {
                RequestTimeout = new TimeSpan(0, 0, 30),
                RetryOptions = new RetryOptions()
                {
                    MaxRetryAttemptsOnThrottledRequests = 3,
                    MaxRetryWaitTimeInSeconds = 60
                }
            });

            builder.RegisterInstance(documentClient);
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
            builder.RegisterAssemblyTypes().AssignableTo(typeof(DateTimeProfile));

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
