namespace Infrastructure
{
    using Core;
    using Core.Interfaces;
    using Core.Models;
    using Microsoft.Azure.Documents.Client;
    using System.Threading.Tasks;

    public class EventsRepository : IEventsRepository
    {
        private readonly DocumentClient _documentClient;

        private readonly string _databaseName;

        private readonly string _collectionName;

        public EventsRepository(DocumentClient documentClient, ISettings settings)
        {
            _documentClient = documentClient;
            _databaseName = settings.DatabaseName;
            _collectionName = settings.EventCollectionName;
        }

        public Task Add(IoTEvent ioTEvent)
        {
            return _documentClient.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri(_databaseName, _collectionName), ioTEvent);
        }
    }
}
