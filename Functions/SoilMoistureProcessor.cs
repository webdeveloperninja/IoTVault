namespace Functions
{
    using AutoMapper;
    using AzureFunctions.Autofac;
    using Controllers;
    using Functions.Configs;
    using Microsoft.Azure.WebJobs;
    using Microsoft.Extensions.Logging;
    using IoTHubTrigger = Microsoft.Azure.WebJobs.EventHubTriggerAttribute;

    [DependencyInjectionConfig(typeof(DIConfig))]
    public static class SoilMoistureProcessor
    {
        [FunctionName("SoilMoistureProcessor")]
        public static void Run([IoTHubTrigger("messages/events", Connection = "IoTHubConnection")]string message, ILogger log, [Inject] IMapper mapper)
        {
            var controller = new SoilMoistureController(message, log, mapper);

            controller.Execute();
        }
    }
}