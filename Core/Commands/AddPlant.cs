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
        public IRepository Repository;
    }

    public class AddPlantHandler : IRequestHandler<AddPlant, Plant>
    {
        public async Task<Plant> Handle(AddPlant request, CancellationToken cancellationToken)
        {
            await request.Repository.Add(request.Plant);

            return request.Plant;
        }
    }
}
