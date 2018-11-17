using Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Functions
{
    public class Settings : ISettings
    {
        public string DatabaseName { get => Environment.GetEnvironmentVariable("DatabaseName"); }

        public string PlantCollectionName { get => Environment.GetEnvironmentVariable("PlantsCollectionName"); }

        public string EventCollectionName { get => Environment.GetEnvironmentVariable("EventsCollectionName"); }
    }
}
