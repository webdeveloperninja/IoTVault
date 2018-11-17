namespace Functions
{
    using Core;
    using System;

    public class Settings : ISettings
    {
        public string DatabaseName { get => Environment.GetEnvironmentVariable("DatabaseName"); }

        public string PlantCollectionName { get => Environment.GetEnvironmentVariable("PlantsCollectionName"); }

        public string EventCollectionName { get => Environment.GetEnvironmentVariable("EventsCollectionName"); }
    }
}
