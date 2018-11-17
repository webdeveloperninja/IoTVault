namespace Infrastructure
{
    using Core;
    using Core.Interfaces;
    using Microsoft.Azure.Documents.Client;
    using System.Threading.Tasks;

    public class AddPlantRepository : IRepository
    {
        private readonly DocumentClient _documentClient;

        private readonly string _databaseName;

        private readonly string _collectionName;

        public AddPlantRepository(DocumentClient documentClient, ISettings settings)
        {
            _documentClient = documentClient;
            _databaseName = settings.DatabaseName;
            _collectionName = settings.PlantCollectionName;
        }

        public Task Add<Plant>(Plant plant)
        {
            return _documentClient.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri(_databaseName, _collectionName), plant);
        }
    }
}
