using IoTVault.Controllers;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace IoTVault
{
    public static class AddPlantFunction
    {
        [FunctionName("AddPlant")]
        public static void Run([EventHubTrigger("plant", Connection = "EventHubPlantDetailsConnection")]string newPlantMessage, ILogger log)
        {
            var controller = new AddPlantController(newPlantMessage);

            controller.Execute();
        }
    }
}
