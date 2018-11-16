using Controllers.Models;
using System;
using Newtonsoft.Json;
using AzureFunctions.Autofac;
using MediatR;
using Core.Commands;
using AutoMapper;

namespace Controllers
{
    public class AddPlantController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        private string _plantMessage;

        public AddPlantController(string plantMessage, IMediator mediator, IMapper mapper)
        {
            _plantMessage = plantMessage;
            _mediator = mediator;
            _mapper = mapper;
        }

        public async void Execute()
        {
            var plant = TryDeserializePlant();

            var addPlantQuery = new AddPlant
            {
                Plant = _mapper.Map<Core.Models.Plant>(plant)
            };

            await _mediator.Send(addPlantQuery);
        }

        private Plant TryDeserializePlant()
        {
            return JsonConvert.DeserializeObject<Plant>(_plantMessage, new JsonSerializerSettings());
        }
    }
}
