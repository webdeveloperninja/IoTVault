namespace Core.Commands
{
    using Core.Interfaces;
    using Core.Models;
    using MediatR;
    using System.Threading;
    using System.Threading.Tasks;

    public class AddPlant : IRequest<Plant>
    {
        public Plant Plant;
        public IPlantsRepository Repository;
    }

    public class AddPlantHandler : IRequestHandler<AddPlant, Plant>
    {
        public async Task<Plant> Handle(AddPlant request, CancellationToken cancellationToken)
        {

            DeleteExistingPlantsWithSameDeviceId(request.Plant.DeviceId, request.Repository);

            await request.Repository.Add(request.Plant);

            return request.Plant;
        }

        private void DeleteExistingPlantsWithSameDeviceId(string deviceId, IPlantsRepository repository)
        {
            var existingPlant = repository.SelectByDeviceId(deviceId);

            if (existingPlant != null)
            {
                repository.DeletePlantByDeviceId(deviceId);

                DeleteExistingPlantsWithSameDeviceId(deviceId, repository);
            }
        }
    }
}
