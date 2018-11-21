namespace Core
{
    public interface ISettings
    {
        string DatabaseName { get; }

        string PlantCollectionName { get; }

        string EventCollectionName { get; }

        string DatabaseLink { get; }
    }
}
