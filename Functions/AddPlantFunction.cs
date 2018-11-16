namespace Functions
{
    using AutoMapper;
    using AzureFunctions.Autofac;
    using Controllers;
    using Functions.Configs;
    using MediatR;
    using Microsoft.Azure.WebJobs;
    using Microsoft.Extensions.Logging;

    [DependencyInjectionConfig(typeof(DIConfig))]
    public static class AddPlantFunction
    {
        [FunctionName("AddPlant")]
        public static void Run([EventHubTrigger("plant", Connection = "EventHubPlantDetailsConnection")]string newPlantMessage, ILogger log, [Inject] IMediator mediator, [Inject] IMapper mapper)
        {
            var controller = new AddPlantController(newPlantMessage, mediator, mapper);

            controller.Execute();
        }
    }
}
