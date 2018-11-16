namespace Infrastructure
{
    using Core.Interfaces;
    using Core.Models;
    using Microsoft.Azure.WebJobs;
    using System.Threading.Tasks;

    public class AddPlantRepository : IRepository
    {
        private readonly IAsyncCollector<Plant> _plants;

        public AddPlantRepository(IAsyncCollector<Plant> plants)
        {
            _plants = plants;
        }

        public Task Add(Plant plant)
        {
            return _plants.AddAsync(plant);
        }
    }
}
