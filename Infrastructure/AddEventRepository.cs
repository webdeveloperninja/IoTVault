namespace Infrastructure
{
    using Core;
    using Core.Interfaces;
    using Microsoft.Azure.Documents.Client;
    using System.Threading.Tasks;

    public class AddEventRepository : IRepository
    {
        private readonly DocumentClient _documentClient;

        private readonly string _databaseName;

        private readonly string _collectionName;

        public AddEventRepository(DocumentClient documentClient, ISettings settings)
        {
            _documentClient = documentClient;
            _databaseName = settings.DatabaseName;
            _collectionName = settings.EventCollectionName;
        }

        public Task Add<IoTEvent>(IoTEvent ioTEvent)
        {
            return _documentClient.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri(_databaseName, _collectionName), ioTEvent);
        }
    }
}
