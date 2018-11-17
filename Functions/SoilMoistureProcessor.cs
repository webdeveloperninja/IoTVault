namespace Functions
{
    using AutoMapper;
    using Controllers;
    using Microsoft.Azure.WebJobs;
    using Microsoft.Extensions.Logging;
    using Microsoft.ServiceBus.Messaging;
    using System;
    using System.Text;
    using IoTHubTrigger = Microsoft.Azure.WebJobs.EventHubTriggerAttribute;

    public static class SoilMoistureProcessor
    {
        static SoilMoistureProcessor()
        {
            Mapper.Initialize(cfg => cfg.CreateMap<string, DateTime>().ConvertUsing(Convert.ToDateTime));
        }

        [FunctionName("SoilMoistureProcessor")]
        public static void Run([IoTHubTrigger("messages/events", Connection = "IoTHubConnection")]string message, ILogger log)
        {
            var controller = new SoilMoistureController(message, log);

            controller.Execute();
        }
    }
}