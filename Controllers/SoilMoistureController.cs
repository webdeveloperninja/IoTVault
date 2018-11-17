namespace Controllers
{
    using AutoMapper;
    using Controllers.Models;
    using Controllers.Resolvers;
    using Core.Commands;
    using Core.Interfaces;
    using Core.Models;
    using MediatR;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;
    using System;

    public class SoilMoistureController
    {
        private readonly string _message;
        private readonly ILogger _log;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly IEventsRepository _repository;

        public SoilMoistureController(string message, ILogger log, IMapper mapper, IMediator mediator, IEventsRepository repository)
        {
            _message = message;
            _log = log;
            _mapper = mapper;
            _mediator = mediator;
            _repository = repository;
        }

        public void Execute()
        {
            var ioTEvent = GetEvent();

            var addIoTEventQuery = new AddEvent
            {
                IoTEvent = ioTEvent,
                Repository = _repository
            };

            _mediator.Send(addIoTEventQuery);
        }

        private IoTEvent GetEvent()
        {
            var iotDeviceEvent = JsonConvert.DeserializeObject<IoTEventDTO>(_message, new JsonSerializerSettings()
            {
                ContractResolver = new UnderscorePropertyNames()
            });

            int voltage;
            Int32.TryParse(iotDeviceEvent.Data, out voltage);

            return new IoTEvent
            {
                MoistureVoltage = voltage,
                DeviceId = iotDeviceEvent.DeviceId,
                Event = iotDeviceEvent.Event,
                PublishedAt = _mapper.Map<DateTime>(iotDeviceEvent.PublishedAt)
            };
        }
    }
}
