using Controllers.Models;
namespace Controllers
{
    using AutoMapper;
    using AzureFunctions.Autofac;
    using Core.Commands;
    using Infrastructure;
    using MediatR;
    using Newtonsoft.Json;

    public class AddPlantController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly AddPlantRepository _addPlantRepository;

        private string _plantMessage;

        public AddPlantController(string plantMessage, IMediator mediator, IMapper mapper, AddPlantRepository repository)
        {
            _plantMessage = plantMessage;
            _mediator = mediator;
            _mapper = mapper;
            _addPlantRepository = repository;
        }

        public async void Execute()
        {
            var plant = TryDeserializePlant();

            var addPlantQuery = new AddPlant
            {
                Plant = _mapper.Map<Core.Models.Plant>(plant),
                Repository = _addPlantRepository
            };

            await _mediator.Send(addPlantQuery);
        }

        private Plant TryDeserializePlant()
        {
            return JsonConvert.DeserializeObject<Plant>(_plantMessage, new JsonSerializerSettings());
        }
    }
}
