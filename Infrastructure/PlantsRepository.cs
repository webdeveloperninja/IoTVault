namespace Infrastructure
{
    using Core;
    using Core.Interfaces;
    using Core.Models;
    using Microsoft.Azure.Documents;
    using Microsoft.Azure.Documents.Client;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading.Tasks;

    public class PlantsRepository : IPlantsRepository
    {
        private readonly DocumentClient _documentClient;

        private readonly string _databaseName;

        private readonly string _collectionName;

        public PlantsRepository(DocumentClient documentClient, ISettings settings)
        {
            _documentClient = documentClient;
            _databaseName = settings.DatabaseName;
            _collectionName = settings.PlantCollectionName;
        }

        public Task Add(Plant plant)
        {
            return _documentClient.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri(_databaseName, _collectionName), plant);
        }

        public Plant SelectByDeviceId(string deviceId)
        {
            var collectionUri = UriFactory.CreateDocumentCollectionUri(_databaseName, _collectionName);

            var query = new SqlQuerySpec(
                "SELECT * FROM Plants plant WHERE plant.DeviceId = @deviceId",
                new SqlParameterCollection(new SqlParameter[] { new SqlParameter { Name = "@deviceId", Value = deviceId } }));

            return _documentClient.CreateDocumentQuery<Plant>(collectionUri, query).AsEnumerable().FirstOrDefault();
        }
    }
}
