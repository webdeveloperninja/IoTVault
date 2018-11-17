namespace Functions
{
    using AutoMapper;
    using AzureFunctions.Autofac;
    using Controllers;
    using Core;
    using Core.Models;
    using Functions.Configs;
    using Infrastructure;
    using MediatR;
    using Microsoft.Azure.Documents.Client;
    using Microsoft.Azure.WebJobs;
    using Microsoft.Extensions.Logging;
    using System;

    [DependencyInjectionConfig(typeof(DIConfig))]
    public static class AddPlantProcessor
    {
        [FunctionName("AddPlant")]
        public static async void Run([EventHubTrigger("plant", Connection = "EventHubPlantDetailsConnection")]string newPlantMessage, ILogger log, [Inject] IMediator mediator, [Inject] IMapper mapper, [Inject] DocumentClient documentClient, [Inject] ISettings settings)
        {
            var repository = new AddPlantRepository(documentClient, settings);

            var controller = new AddPlantController(newPlantMessage, mediator, mapper, repository);

            controller.Execute();
        }
    }
}