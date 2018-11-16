using Core.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Commands
{
    public class AddPlant : IRequest<Plant>
    {
        public Plant Plant;
    }

    public class AddPlantHandler : IRequestHandler<AddPlant, Plant>
    {
        public Task<Plant> Handle(AddPlant request, CancellationToken cancellationToken)
        {
            return Task.FromResult(new Plant());
        }
    }
}
