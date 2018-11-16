namespace Functions
{
    using AutoMapper;
    using AzureFunctions.Autofac;
    using Controllers;
    using Core.Models;
    using Functions.Configs;
    using Infrastructure;
    using MediatR;
    using Microsoft.Azure.WebJobs;
    using Microsoft.Extensions.Logging;

    [DependencyInjectionConfig(typeof(DIConfig))]
    public static class AddPlantFunction
    {
        [FunctionName("AddPlant")]
        public static async void Run([EventHubTrigger("plant", Connection = "EventHubPlantDetailsConnection")]string newPlantMessage, [CosmosDB(
                databaseName: "IotVault",
                collectionName: "Plants",
                ConnectionStringSetting = "CosmosDBConnection")] IAsyncCollector<Plant> plantsOut, ILogger log, [Inject] IMediator mediator, [Inject] IMapper mapper)
        {
            var repository = new AddPlantRepository(plantsOut);

            var controller = new AddPlantController(newPlantMessage, mediator, mapper, repository);

            controller.Execute();
        }
    }
}
