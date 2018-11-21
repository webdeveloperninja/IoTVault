namespace Functions
{
    using AzureFunctions.Autofac;
    using Core;
    using Functions.Configs;
    using Microsoft.Azure.Documents;
    using Microsoft.Azure.Documents.Client;
    using Microsoft.Azure.WebJobs;
    using Microsoft.Azure.WebJobs.Extensions.Http;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;

    [DependencyInjectionConfig(typeof(DIConfig))]
    public static class GetSoilMoisture
    {
        [FunctionName("GetSoilMoisture")]
        public static async Task<HttpResponseMessage> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)]HttpRequestMessage req, [Inject] DocumentClient documentClient, [Inject] ISettings settings)
        {
            string name = req.GetQueryNameValuePairs()
                .FirstOrDefault(q => string.Compare(q.Key, "name", true) == 0)
                .Value;

            var databaseLink = UriFactory.CreateDatabaseUri("IoT");

            try
            {
                DocumentCollection collection = documentClient.CreateDocumentCollectionQuery(databaseLink).Where(c => c.Id == "b8f822b7-5bb9-4499-a921-c17bc4e1376b").AsEnumerable().FirstOrDefault();

                Database database = documentClient.CreateDatabaseQuery("SELECT * FROM d WHERE d.id = \"IoT\"").AsEnumerable().First();

                List<DocumentCollection> collections = documentClient.CreateDocumentCollectionQuery((String)database.SelfLink).ToList();

                var eventsCollection = collections.Where(c => c.Id == "Events").FirstOrDefault();

                var book = documentClient.CreateDocumentQuery<Test>(eventsCollection.SelfLink).Where(b => b.Plant._id == "5bf194d74f98d70020d5bf41").ToList();


            }
            catch (Exception e)
            {
                var t = e;
            }

            return req.CreateResponse();
        }

        class Test
        {
            public Plant Plant;
        }

        class Plant
        {
            public string _id;
        }
    }
}
