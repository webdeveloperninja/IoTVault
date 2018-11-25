namespace Functions
{
    using AzureFunctions.Autofac;
    using Core;
    using Functions.Configs;
    using Functions.Models;
    using Microsoft.Azure.Documents;
    using Microsoft.Azure.Documents.Client;
    using Microsoft.Azure.WebJobs;
    using Microsoft.Azure.WebJobs.Extensions.Http;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;

    [DependencyInjectionConfig(typeof(DIConfig))]
    public static class GetSoilMoisture
    {
        [FunctionName("GetSoilMoisture")]
        public static async Task<HttpResponseMessage> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)]HttpRequestMessage req, [Inject] DocumentClient documentClient, [Inject] ISettings settings)
        {
            string plantId = req.GetQueryNameValuePairs()
                .FirstOrDefault(q => string.Compare(q.Key, "plantId", true) == 0).Value;

            var databaseLink = UriFactory.CreateDatabaseUri("IoT");

            Database database = documentClient.CreateDatabaseQuery("SELECT * FROM d WHERE d.id = \"IoT\"").AsEnumerable().First();

            List<DocumentCollection> collections = documentClient.CreateDocumentCollectionQuery((String)database.SelfLink).ToList();

            var eventsCollection = collections.Where(c => c.Id == "Events").FirstOrDefault();

            var events = documentClient.CreateDocumentQuery<Event>(eventsCollection.SelfLink).Where(b => b.Plant._id == plantId).ToList();

            var json = JsonConvert.SerializeObject(events, Formatting.Indented);

            return new HttpResponseMessage(HttpStatusCode.OK)
            {
               Content = new StringContent(json, Encoding.UTF8, "application/json")
            };
        }
    }
}
