namespace Functions
{
    using AutoMapper;
    using AzureFunctions.Autofac;
    using Controllers;
    using Core;
    using Functions.Configs;
    using Infrastructure;
    using MediatR;
    using Microsoft.Azure.Documents.Client;
    using Microsoft.Azure.WebJobs;
    using Microsoft.Extensions.Logging;
    using IoTHubTrigger = Microsoft.Azure.WebJobs.EventHubTriggerAttribute;

    [DependencyInjectionConfig(typeof(DIConfig))]
    public static class SoilMoistureProcessor
    {
        [FunctionName("SoilMoistureProcessor")]
        public static void Run([IoTHubTrigger("messages/events", Connection = "IoTHubConnection")]string message, ILogger log, 
            [Inject] IMapper mapper, [Inject] IMediator mediator, [Inject] DocumentClient documentClient, [Inject] ISettings settings)
        {
            var repository = new AddEventRepository(documentClient, settings);

            var controller = new SoilMoistureController(message, log, mapper, mediator, repository);

            controller.Execute();
        }
    }
}